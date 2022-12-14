using System.Text;

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
        long register = 1;
        int cycle = 1;
        int drawPosition = 0;

        bool run = true;
        Queue<Operation> queue = new(ParseInstruction(dataset));
        StringBuilder display = new();
        Operation executingOrder = queue.Dequeue();
        Operation nextOrder = executingOrder;
        do
        {
            if (queue.Count > 0 && executingOrder.Cycle == cycle)
            {
                nextOrder = queue.Dequeue();
            }

            if (drawPosition % 40 == 0 && drawPosition > 0)
            {
                display.AppendLine();
                drawPosition = 0;
            }

            char pixel = GetIsCycleInSpritePosition(drawPosition, register) ? '#' : '.';
            display.Append(pixel);

            if (executingOrder.Cycle == cycle)
            {
                register += executingOrder.Value;
                executingOrder = nextOrder;
            }

            drawPosition++;
            cycle++;
            if (queue.Count == 0 && cycle > executingOrder.Cycle)
            {
                run = false;
            }
        }
        while (run);

        bool GetIsCycleInSpritePosition(int cycle, long register)
        {
            return cycle == register - 1 || cycle == register || cycle == register + 1;
        }

        Console.WriteLine(display.ToString());

        return 0;
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
