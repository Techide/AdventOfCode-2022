internal class Puzzle6 : PuzzleBase<FileStream, int>
{
    internal override void Solve()
    {
        FileStream dataset = GetDataset();
        int partOneResult = PartOne(dataset);
        int partTwoResult = PartTwo(dataset);

        Console.WriteLine($"6: {new { PartOne = partOneResult, PartTwo = partTwoResult }}");
    }

    internal override FileStream GetDataset()
    {
        return File.OpenRead(@".\puzzle 6\input.txt");
    }

    internal override int PartOne(FileStream dataset)
    {
        return SeekStream(dataset, 4);
    }

    internal override int PartTwo(FileStream dataset)
    {
        return SeekStream(dataset, 14);
    }

    private static int SeekStream(FileStream dataset, int size)
    {
        dataset.Seek(0, SeekOrigin.Begin);
        var index = 0;
        var buffer = new char[size];
        while (buffer.Distinct().Count() != size)
        {
            using var reader = new StreamReader(dataset, leaveOpen: true);
            reader.Read(buffer, 0, buffer.Length);
            index++;
            dataset.Seek(index, SeekOrigin.Begin);
        }

        return index + size - 1;
    }
}
