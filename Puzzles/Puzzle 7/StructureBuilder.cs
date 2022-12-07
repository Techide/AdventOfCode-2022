internal class StructureBuilder
{
    private readonly Dir _base = new("root");

    public StructureBuilder()
    {
    }

    public Dir GetRoot() => _base;

    public Dir AddDirectory(string directoryName, Dir entry)
    {
        entry.Directories.Add(new Dir(directoryName, entry));

        return entry;
    }

    public Dir AddFile(string fileName, long fileSize, Dir entry)
    {
        entry.Files.Add(new Fil(fileName, fileSize));

        return entry;
    }
}