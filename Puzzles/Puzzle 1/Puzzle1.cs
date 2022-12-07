using System.IO;

internal sealed class Puzzle1 : PuzzleBase<IEnumerable<long>, long>
{
    internal override IEnumerable<long> GetDataset()
    {
        List<long> sums = new();
        long sum = 0;
        foreach (var item in File.ReadLines(@".\puzzle 1\input.txt"))
        {
            if (string.IsNullOrWhiteSpace(item))
            {
                sums.Add(sum);
                sum = 0;
                continue;
            }

            sum += long.Parse(item);
        }

        return sums;
    }

    internal override long PartOne(IEnumerable<long> dataset)
    {
        dataset.ToList().Sort();

        return dataset.Last();
    }

    internal override long PartTwo(IEnumerable<long> dataset)
    {
        return dataset.OrderByDescending(x => x).Take(3).Sum();
    }
}
