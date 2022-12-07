using System.Data;

using Puzzles.Puzzle7;

internal class Puzzle7 : PuzzleBase<Dir, long>
{
    CommandHandler _commandHandler = new(new());
    List<Dir> _directoriesBelow = new();
    List<(long TotalSize, Dir Directory)> _directoriesAbove = new();
    long _runningTotal = 0;

    internal override Dir GetDataset()
    {
        Dir entry = _commandHandler.ExecuteGetRootCommand();
        var commands = File.ReadLines(@".\puzzle 7\input.txt");
        foreach (var command in commands)
        {
            entry = _commandHandler.InterpretCommand(command, entry);
        }

        return _commandHandler.ExecuteGetRootCommand();
    }

    internal override void Solve()
    {
        Dir dataset = GetDataset();
        long partOneResult = PartOne(dataset);
        long partTwoResult = PartTwo(dataset);

        Console.WriteLine($"7: {new { PartOne = partOneResult, PartTwo = partTwoResult }}");
    }

    internal override long PartOne(Dir dataset)
    {
        _ = FindCandidates(dataset, 100000);
        return _runningTotal;
    }

    private long FindCandidates(Dir CurrentCandidate, long maximumSize)
    {
        List<long> childrensSizes = new();
        foreach (var candidate in CurrentCandidate.Directories)
        {
            childrensSizes.Add(FindCandidates(candidate, maximumSize));
        }

        long fileSize = CurrentCandidate.Files.Aggregate((long)0, (current, x) => current + x.Size, result => result);
        long childSize = childrensSizes.Aggregate(fileSize, (x, y) => x + y, result => result);
        if (childSize <= maximumSize)
        {
            _runningTotal += childSize;
            _directoriesBelow.Add(CurrentCandidate);
        }
        else
        {
            _directoriesAbove.Add(new(childSize, CurrentCandidate));
        }

        return childSize;
    }

    internal override long PartTwo(Dir dataset)
    {
        _directoriesAbove.Clear();
        _directoriesBelow.Clear();

        long driveMax = 70000000;
        long patchSize = 30000000;

        _ = FindCandidates(dataset, 0);
        long freeSpace = driveMax - _directoriesAbove.MaxBy(x => x.TotalSize).TotalSize;
        long spaceNeeded = patchSize - freeSpace;

        var ordered = _directoriesAbove.OrderBy(x => x.TotalSize);
        var selectedDirectory = ordered.First(x => x.TotalSize >= spaceNeeded);

        return selectedDirectory.TotalSize;
    }

}
