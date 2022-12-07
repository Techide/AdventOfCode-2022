namespace Puzzles.Puzzle4;
internal struct Boundary
{
    public int Lower { get; set; }
    public int Upper { get; set; }

    public Boundary(string boundaryString)
    {
        var split = boundaryString.Split('-');
        Lower = int.Parse(split[0]);
        Upper = int.Parse(split[1]);
    }

    public int[] ToArray()
    {
        var numbers = new List<int>();
        for (int i = Lower; i <= Upper; i++)
        {
            numbers.Add(i);
        }

        return numbers.ToArray();
    }
}

internal struct Pair
{
    public Boundary First { get; set; }
    public Boundary Second { get; set; }

    public Pair(string first, string second)
    {
        First = new Boundary(first);
        Second = new Boundary(second);
    }
}
