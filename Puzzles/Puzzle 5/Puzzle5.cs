internal class Puzzle5 : PuzzleBase<List<StackAction>, string>
{
    internal override void Solve()
    {
        List<StackAction> dataset = GetDataset();
        string partOneResult = PartOne(dataset);
        string partTwoResult = PartTwo(dataset);

        Console.WriteLine($"5: {new { PartOne = partOneResult, PartTwo = partTwoResult }}");
    }

    internal override List<StackAction> GetDataset()
    {
        string[] actionLines = File.ReadAllLines(@".\puzzle 5\input.txt");
        List<StackAction> stackActions = new();
        foreach (var actionLine in actionLines)
        {
            stackActions.Add(StackAction.Parse(actionLine));
        }

        return stackActions;
    }

    internal override string PartOne(List<StackAction> stackActions)
    {
        var container = new StackContainer();
        foreach (var stackAction in stackActions)
        {
            PerformActionWithSingleMove(stackAction, container);
        }

        return container.PeekStacks();
    }

    internal override string PartTwo(List<StackAction> stackActions)
    {
        var container = new StackContainer();
        foreach (var stackAction in stackActions)
        {
            PerformActionWithMultipleMoves(stackAction, container);
        }

        return container.PeekStacks();
    }

    private void PerformActionWithMultipleMoves(StackAction stackAction, StackContainer container)
    {
        Stack<char> currentStack = new();
        for (int i = 0; i < stackAction.Amount; i++)
        {
            currentStack.Push(container.TakeFromStack(stackAction.Source));
        }

        for (int i = 0; i < stackAction.Amount; i++)
        {
            container.AddToStack(stackAction.Destination, currentStack.Pop());
        }
    }

    private void PerformActionWithSingleMove(StackAction stackAction, StackContainer container)
    {
        for (int i = 0; i < stackAction.Amount; i++)
        {
            var item = container.TakeFromStack(stackAction.Source);
            container.AddToStack(stackAction.Destination, item);
        }
    }
}
