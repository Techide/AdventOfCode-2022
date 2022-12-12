internal readonly struct Operation
{
    public readonly int Value;
    public readonly int Cycle;

    public Operation(int value, int cycleTime)
    {
        Value = value;
        Cycle = cycleTime;
    }
}

internal class Puzzle10 : BasePuzzle<string[], long>
{
    internal override string[] GetDataset()
    {
        return File.ReadAllLines(@".\puzzle 10\input.txt");
    }

    internal override long PartOne(string[] dataset)
    {

        long value = 1;
        Queue<Operation> queue = new(ParseInstruction(dataset));
        List<long> poiValues = new();
        int cycle = 0;
        bool poi = true;
        do
        {
            cycle++;
            if (cycle % 20 == 0)
            {
                if (poi)
                {
                    poiValues.Add(cycle * value);
                }

                poi = !poi;
            }

            if (queue.Peek().Cycle == cycle)
            {
                var instruction = queue.Dequeue();
                value += instruction.Value;
            }
        }
        while (queue.Count > 0);

        return poiValues.Sum();
    }

    internal override long PartTwo(string[] dataset)
    {
        long result = 0;
        return result;
    }

    private IEnumerable<Operation> ParseInstruction(string[] instructions)
    {
        var i = 0;
        foreach (var instruction in instructions)
        {
            yield return instruction.StartsWith("addx", StringComparison.OrdinalIgnoreCase) switch
            {
                true => new(int.Parse(instruction.Split(" ")[1]), i += 2),
                false => new(0, i += 1)
            };
        }

    }
}
