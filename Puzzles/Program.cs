using System.Reflection;

var assembly = Assembly.GetExecutingAssembly();
var types = assembly.GetTypes()
    .Where(t => t.Name.StartsWith("puzzle", StringComparison.OrdinalIgnoreCase))
    .Select<Type, (Type type, int day)>(x => new(x, int.Parse(x.Name.Last().ToString())));

int lower = types.MinBy(t => t.day).day;
int upper = types.MaxBy(t => t.day).day;


while (true)
{
    var choice = GetInput();
    if (string.IsNullOrWhiteSpace(choice))
    {
        continue;
    }

    string input = choice!;
    if (input.Equals("q", StringComparison.OrdinalIgnoreCase)) { break; }
    if (input.Equals("c", StringComparison.OrdinalIgnoreCase))
    {
        Console.Clear();
        continue;
    }

    if (!int.TryParse(input, out int day))
    {
        continue;
    }

    if (day < lower || day > upper)
    {
        continue;
    }

    IRunnablePuzzle puzzle = GetPuzzle(day);
    puzzle.Solve(day);
    break;
}

IRunnablePuzzle GetPuzzle(int day)
{
    var type = types.First(x => x.day == day).type;

    return (IRunnablePuzzle)Activator.CreateInstance(type)!;
}

string? GetInput()
{
    Console.Write($"Please choose a day to run [{lower} - {upper}]: ");

    return Console.ReadLine();
}