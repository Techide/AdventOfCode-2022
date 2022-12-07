using System.Text;

internal class StackContainer
{
    private readonly Dictionary<int, Stack<char>> _stacks = new();

    public StackContainer()
    {
        for (int i = 1; i <= 9; i++)
        {
            _stacks.Add(i, new Stack<char>());
        }

        SetupStacks();
    }

    private void SetupStacks()
    {
        List<(int index, string stack)> stackStrings = new()
        {
            new (1, "TPZCSLQN"),
            new (2, "LPTVHCG"),
            new (3, "DCZF"),
            new (4, "GWTDLMVC"),
            new (5, "PWC"),
            new (6, "PFJDCTSZ"),
            new (7, "VWGBD"),
            new (8, "NJSQHW"),
            new (9, "RCQFSLV")
        };

        foreach ((int index, string stack) in stackStrings)
        {
            AddToStack(index, stack);
        }
    }

    private void AddToStack(int index, string stackString)
    {
        foreach (var character in stackString)
        {
            AddToStack(index, character);
        }
    }

    public void AddToStack(int index, char character)
    {
        _stacks[index].Push(character);
    }

    public char TakeFromStack(int index)
    {
        return _stacks[index].Pop();
    }

    public string PeekStacks()
    {
        StringBuilder builder = new();
        for (int i = 0; i < _stacks.Count; i++)
        {
            builder.Append(_stacks[i+1].Peek());
        }

        return builder.ToString();
    }
}
