namespace Puzzles.Puzzle7;
internal class CommandHandler
{
    private readonly StructureBuilder _structureBuilder;

    public CommandHandler(StructureBuilder structureBuilder)
    {
        _structureBuilder = structureBuilder;
    }

    internal Dir ExecuteGetRootCommand()
    {
        return _structureBuilder.GetRoot();
    }

    internal Dir InterpretCommand(string command, Dir entry)
    {
        if (command.StartsWith("$", StringComparison.OrdinalIgnoreCase))
        {
            return HandleCommand(command, entry);
        }

        var data = command.Split(' ');
        if (command.StartsWith("dir", StringComparison.OrdinalIgnoreCase))
        {
            return _structureBuilder.AddDirectory(data[1], entry);
        }

        return _structureBuilder.AddFile(data[1], long.Parse(data[0]), entry);
    }

    private Dir HandleCommand(string command, Dir entry)
    {
        var fragments = command.Split(' ');
        if (fragments[1].Equals("ls", StringComparison.OrdinalIgnoreCase)) { return entry; }

        return HandleChangeDirectory(fragments[2], entry);

    }

    private Dir HandleChangeDirectory(string name, Dir entry)
    {
        if (name.Equals("..")) { return entry.Parent!; }
        if (name.Equals(@"/")) { return _structureBuilder.GetRoot(); }

        return entry.Directories.Single(x => x.Name.Equals(name));
    }

}
