using System;
using System.Data;

internal class Puzzle3 : BasePuzzle<string[], long>
{
    private class RuckSack
    {
        public string First { get; set; } = string.Empty;
        public string Second { get; set; } = string.Empty;
    }

    private readonly Dictionary<char, int> _priorities = new();

    internal override string[] GetDataset()
    {
        ConstructPriorities();
        return File.ReadAllLines(@".\puzzle 3\input.txt");
    }

    internal override long PartOne(string[] dataset)
    {
        var ruckSacks = new List<RuckSack>();
        ruckSacks.AddRange(CreateRucksacks(dataset));

        long sum = 0;
        foreach (var item in ruckSacks)
        {
            var matchingItems = item.First.Intersect(item.Second);
            sum += GetItemSum(matchingItems);
        }

        return sum;
    }

    internal override long PartTwo(string[] dataset)
    {
        long sum = 0;
        var groups = SplitIntoGroups(dataset, 3);
        foreach (var group in groups)
        {
            var badge = group.Select(x => x.AsEnumerable())
                              .Aggregate((a, b) => a.Intersect(b))
                              .Single();

            sum += _priorities[badge];
        }

        return sum;
    }

    private void ConstructPriorities()
    {
        var all = new List<char>();
        all.AddRange(GetSequence('a'));
        all.AddRange(GetSequence('A'));

        var priority = 0;
        foreach (var letter in all)
        {
            _priorities[letter] = ++priority;
        }
    }

    private static IEnumerable<char> GetSequence(int first)
    {
        for (int i = first; i < first + 26; i++)
        {
            yield return (char)i;
        }
    }

    private IEnumerable<IEnumerable<string>> SplitIntoGroups(string[] dataset, int size)
    {
        for (int i = 0; i < dataset.Length / size; i++)
        {
            yield return dataset.Skip(i * size).Take(size);
        }
    }

    private long GetItemSum(IEnumerable<char> matchingItems)
    {
        var sum = 0;
        foreach (var character in matchingItems)
        {
            sum += _priorities[character];
        }

        return sum;
    }

    private static IEnumerable<RuckSack> CreateRucksacks(string[] dataset)
    {
        foreach (var item in dataset)
        {
            yield return new RuckSack
            {
                First = item[..(item.Length / 2)],
                Second = item.Substring(item.Length / 2, item.Length / 2)
            };
        }
    }
}