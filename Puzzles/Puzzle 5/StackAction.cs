internal struct StackAction
{
    public int Amount { get; set; }

    public int Source { get; set; }

    public int Destination { get; set; }

    public static StackAction Parse(string actionString)
    {
        var actions = actionString
            .Replace("move", "", StringComparison.OrdinalIgnoreCase)
            .Replace("from", ",", StringComparison.OrdinalIgnoreCase)
            .Replace("to", ",", StringComparison.OrdinalIgnoreCase)
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        return new()
        {
            Amount = int.Parse(actions[0]),
            Source = int.Parse(actions[1]),
            Destination = int.Parse(actions[2]),
        };

    }
}
