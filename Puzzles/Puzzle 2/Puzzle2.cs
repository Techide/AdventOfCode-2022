using static Puzzle2;

internal sealed class Puzzle2 : PuzzleBase<IEnumerable<Selection>, long>
{
    internal struct FirstShape
    {
        public const string ROCK = "A";
        public const string PAPER = "B";
        public const string SCISSORS = "C";
    }

    internal struct SecondShape
    {
        public const string ROCK = "X";
        public const string PAPER = "Y";
        public const string SCISSORS = "Z";
    }

    internal struct Result
    {
        public const string LOSE = "X";
        public const string DRAW = "Y";
        public const string WIN = "Z";
    }

    internal Dictionary<Tuple<string, string>, int> _matchup = new()
    {
        { new(FirstShape.ROCK, SecondShape.ROCK), 3 },
        { new(FirstShape.ROCK, SecondShape.PAPER), 6 },
        { new(FirstShape.ROCK, SecondShape.SCISSORS), 0 },
        { new(FirstShape.PAPER, SecondShape.ROCK), 0 },
        { new(FirstShape.PAPER, SecondShape.PAPER), 3 },
        { new(FirstShape.PAPER, SecondShape.SCISSORS), 6 },
        { new(FirstShape.SCISSORS, SecondShape.ROCK), 6 },
        { new(FirstShape.SCISSORS, SecondShape.PAPER), 0 },
        { new(FirstShape.SCISSORS, SecondShape.SCISSORS), 3 }
    };

    internal Dictionary<Tuple<string, string>, string> _desiredSelection = new()
    {
        { new(FirstShape.ROCK, Result.LOSE), SecondShape.SCISSORS },
        { new(FirstShape.ROCK, Result.DRAW), SecondShape.ROCK},
        { new(FirstShape.ROCK, Result.WIN), SecondShape.PAPER },
        { new(FirstShape.PAPER, Result.LOSE), SecondShape.ROCK },
        { new(FirstShape.PAPER, Result.DRAW), SecondShape.PAPER},
        { new(FirstShape.PAPER, Result.WIN), SecondShape.SCISSORS },
        { new(FirstShape.SCISSORS, Result.LOSE), SecondShape.PAPER },
        { new(FirstShape.SCISSORS, Result.DRAW), SecondShape.SCISSORS},
        { new(FirstShape.SCISSORS, Result.WIN), SecondShape.ROCK }
    };

    internal Dictionary<string, int> _selectionPoints = new() {
        { SecondShape.ROCK, 1 },
        { SecondShape.PAPER, 2 },
        { SecondShape.SCISSORS, 3 },
    };

    internal class Selection
    {
        public string Oponent { get; set; } = string.Empty;

        public string Me { get; set; } = string.Empty;
    }

    internal override void Solve()
    {

        var selections = GetDataset();
        var partOneResult = PartOne(selections);
        var partTwoResult = PartTwo(selections);

        Console.WriteLine($"2: {new { PartOne = partOneResult, PartTwo = partTwoResult }}");
    }

    internal override IEnumerable<Selection> GetDataset()
    {
        var dataset = File.ReadAllLines(@".\puzzle 2\input.txt");
        List<Selection> selection = new List<Selection>();
        foreach (string dataLine in dataset)
        {
            var selectionStringArray = dataLine.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            selection.Add(new Selection
            {
                Oponent = selectionStringArray[0],
                Me = selectionStringArray[1]
            });
        }

        return selection;
    }

    internal override long PartOne(IEnumerable<Selection> selections)
    {
        long sum = 0;
        foreach (var selection in selections)
        {
            sum += GetPoints(selection);
        }

        return sum;
    }

    internal override long PartTwo(IEnumerable<Selection> selections)
    {
        long sum = 0;
        foreach (var selection in selections)
        {
            var desiredSelection = _desiredSelection[new(selection.Oponent, selection.Me)];
            selection.Me = desiredSelection;
            sum += GetPoints(selection);
        }

        return sum;
    }

    private int GetPoints(Selection selection)
    {
        return _selectionPoints[selection.Me] + _matchup[new(selection.Oponent, selection.Me)];
    }

}
