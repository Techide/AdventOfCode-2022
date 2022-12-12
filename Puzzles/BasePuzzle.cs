internal abstract class BasePuzzle<Tin, Tout> : IRunnablePuzzle
{
    public virtual void Solve(int puzzle)
    {
        Tin dataset = GetDataset();
        Tout partOneResult = PartOne(dataset);
        Tout partTwoResult = PartTwo(dataset);

        Console.WriteLine($"{puzzle}: {new { PartOne = partOneResult, PartTwo = partTwoResult }}");
    }

    internal abstract Tin GetDataset();
    internal abstract Tout PartOne(Tin dataset);
    internal abstract Tout PartTwo(Tin dataset);
}