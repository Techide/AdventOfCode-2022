internal abstract class PuzzleBase<Tin, Tout>
{
    internal abstract void Solve();
    internal abstract Tin GetDataset();
    internal abstract Tout PartOne(Tin dataset);
    internal abstract Tout PartTwo(Tin dataset);
}