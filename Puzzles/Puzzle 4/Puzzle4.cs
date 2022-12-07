using System.Data;

using Puzzles.Puzzle4;

internal class Puzzle4 : PuzzleBase<IEnumerable<Pair>, int>
{
    private readonly DatasetGenerator _generator;

    public Puzzle4()
    {
        _generator = new DatasetGenerator(@".\puzzle 4\input.txt");
    }

    internal override void Solve()
    {
        var dataset = GetDataset();
        var partOneResult = PartOne(dataset);
        var partTwoResult = PartTwo(dataset);

        Console.WriteLine($"4: {new { PartOne = partOneResult, PartTwo = partTwoResult }}");
    }

    internal override int PartOne(IEnumerable<Pair> dataset)
    {
        var selected = dataset.Select<Pair, (int[], int[])>(x => new(x.First.ToArray(), x.Second.ToArray()));
        var total = selected.Where(x => x.Item1.All(first => x.Item2.Contains(first)) || x.Item2.All(second => x.Item1.Contains(second)));

        return total.Count();
    }

    internal override int PartTwo(IEnumerable<Pair> dataset)
    {
        var selected = dataset.Select<Pair, (int[], int[])>(x => new(x.First.ToArray(), x.Second.ToArray()));
        var total = selected.Where(x => x.Item1.Any(first => x.Item2.Contains(first)) || x.Item2.Any(first => x.Item1.Contains(first)));

        return total.Count();
    }

    internal override IEnumerable<Pair> GetDataset()
    {
        return _generator.GetDataSet();
    }
}
