using System.Data;

using Puzzles.Puzzle7;

internal class Puzzle7 : PuzzleBase<Dir, long>
{
    CommandHandler _commandHandler = new(new());
    List<(long TotalSize, Dir Directory)> _validDirectories = new();

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

    internal override long PartOne(Dir dataset)
    {
        static bool belowPredicate(long a, long b) { return a <= b; }
        _ = FindCandidates(dataset, 100000, belowPredicate );
        
        return _validDirectories.Aggregate((long)0, (value, item) => value + item.TotalSize);
    }

    private long FindCandidates(Dir CurrentCandidate, long maximumSize, Func<long, long, bool> predicate)
    {
        List<long> childrensSizes = new();
        foreach (var candidate in CurrentCandidate.Directories)
        {
            childrensSizes.Add(FindCandidates(candidate, maximumSize, predicate));
        }

        long fileSize = CurrentCandidate.Files.Aggregate((long)0, (current, x) => current + x.Size, result => result);
        long childSize = childrensSizes.Aggregate(fileSize, (x, y) => x + y, result => result);
        if (predicate(childSize, maximumSize))
        {
            _validDirectories.Add(new(childSize, CurrentCandidate));
        }

        return childSize;
    }

    internal override long PartTwo(Dir dataset)
    {
        _validDirectories.Clear();

        long driveMax = 70000000;
        long patchSize = 30000000;

        bool abovePredicate(long a, long b) { return a > b; }
        _ = FindCandidates(dataset, 0, abovePredicate);

        long freeSpace = driveMax - _validDirectories.MaxBy(x => x.TotalSize).TotalSize;
        long spaceNeeded = patchSize - freeSpace;

        var ordered = _validDirectories.OrderBy(x => x.TotalSize);
        var selectedDirectory = ordered.First(x => x.TotalSize >= spaceNeeded);

        return selectedDirectory.TotalSize;
    }

}
