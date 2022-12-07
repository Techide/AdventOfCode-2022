namespace Puzzles.Puzzle4;
internal class DatasetGenerator
{
    private readonly string _filePath;

    public DatasetGenerator(string filePath)
    {
        _filePath = filePath;
    }

    public IEnumerable<Pair> GetDataSet()
    {
        var data = File.ReadAllLines(_filePath);
        var result = new List<Pair>();

        return data.Select(line =>
        {
            var split = line.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            return new Pair(split[0], split[1]);
        });
    }
}
