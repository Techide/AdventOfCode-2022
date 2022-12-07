internal class Dir
{
    public string Name { get; set; }

    public Dir? Parent { get; set; }

    public List<Dir> Directories { get; set; } = new();

    public List<Fil> Files { get; set; } = new();

    public Dir(string name, Dir parent) : this(name)
    {
        Parent = parent;
    }

    public Dir(string name)
    {
        Name = name;
    }

}
