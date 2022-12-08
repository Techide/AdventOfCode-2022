internal class Tree
{
    public bool Visible { get; set; }

    public int ScenicScore { get; set; }

    public int Height { get; set; }
}

internal class Puzzle8 : PuzzleBase<Tree[,], int>
{
    private int _highestTreeLevel = 0;
    internal override Tree[,] GetDataset()
    {
        var data = File.ReadLines(@".\puzzle 8\input.txt").ToArray();
        Tree[,] dataset = new Tree[data.Length, data[0].Length];
        for (int row = 0; row < data.Length; row++)
        {
            for (int column = 0; column < data[row].Length; column++)
            {
                var height = int.Parse(data[row][column].ToString());
                dataset[row, column] = new Tree
                {
                    Height = height,
                    ScenicScore = -1
                };

                if (height > _highestTreeLevel) { _highestTreeLevel = height; }
            }
        }

        return dataset;
    }

    private enum ScanDirection
    {
        Horizontal,
        Vertical
    }

    internal override int PartOne(Tree[,] dataset)
    {
        ExploreTrees(dataset, ScanDirection.Horizontal);
        ExploreTrees(dataset, ScanDirection.Vertical);
        int sum = 0;
        foreach (var tree in dataset)
        {
            if (tree.Visible)
            {
                sum++;
            }
        }

        return sum;
    }

    internal override int PartTwo(Tree[,] dataset)
    {
        int rowCount = dataset.GetLength(0);
        int columnCount = dataset.GetLength(1);

        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
            {
                FindScenicScore(dataset, rowIndex, columnIndex, rowCount, columnCount);
            }
        }

        List<Tree> treeList = new();
        foreach (var tree in dataset)
        {
            treeList.Add(tree);
        }

        var test = treeList.All(x => x.ScenicScore >= 0);

        return treeList.MaxBy(x => x.ScenicScore)!.ScenicScore;
    }

    private void FindScenicScore(Tree[,] dataset, int rowIndex, int columnIndex, int rowCount, int columnCount)
    {
        var tree = dataset[rowIndex, columnIndex];

        (int north, int east, int south, int west) score = new(0, 0, 0, 0);
        for (int localColumnIndex = columnIndex; localColumnIndex <= columnCount; localColumnIndex++)
        {
            if (localColumnIndex + 1 > columnCount - 1)
            {
                break;
            }

            score.east++;
            if (tree.Height <= dataset[rowIndex, localColumnIndex + 1].Height)
            {
                break;
            }
        }


        for (int localColumnIndex = columnIndex; localColumnIndex >= 0; localColumnIndex--)
        {
            if (localColumnIndex - 1 < 0)
            {
                break;
            }

            score.west++;
            if (tree.Height <= dataset[rowIndex, localColumnIndex - 1].Height)
            {
                break;
            }
        }

        for (int localRowIndex = rowIndex; localRowIndex >= 0; localRowIndex--)
        {
            if (localRowIndex - 1 < 0)
            {
                break;
            }

            score.north++;
            if (tree.Height <= dataset[localRowIndex - 1, columnIndex].Height)
            {
                break;
            }
        }

        for (int localRowIndex = rowIndex; localRowIndex <= rowCount; localRowIndex++)
        {
            if (localRowIndex + 1 > rowCount - 1)
            {
                break;
            }

            score.south++;
            if (tree.Height <= dataset[localRowIndex + 1, columnIndex].Height)
            {
                break;
            }
        }

        tree.ScenicScore = score.east * score.west * score.north * score.south;
    }

    private void ExploreTrees(Tree[,] trees, ScanDirection scanDirection)
    {
        int rowCount = trees.GetLength(0);
        int columnCount = trees.GetLength(1);

        if (scanDirection is ScanDirection.Horizontal)
        {
            ScanHorizontally(trees, rowCount, columnCount, -1);
        }

        if (scanDirection is ScanDirection.Vertical)
        {
            ScanVertically(trees, rowCount, columnCount, -1);
        }
    }

    private void ScanHorizontally(Tree[,] trees, int rowCount, int columnCount, int minimumHeight)
    {
        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            int tallestTreeEncountered = minimumHeight;
            for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
            {
                ExamineTree(trees, rowIndex, columnIndex, ref tallestTreeEncountered);
            }

            tallestTreeEncountered = minimumHeight;
            for (int columnIndex = columnCount - 1; columnIndex >= 0; columnIndex--)
            {
                ExamineTree(trees, rowIndex, columnIndex, ref tallestTreeEncountered);
            }
        }
    }

    private void ScanVertically(Tree[,] trees, int rowCount, int columnCount, int minimumHeight)
    {
        for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
        {
            int tallestTreeEncountered = minimumHeight;
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                ExamineTree(trees, rowIndex, columnIndex, ref tallestTreeEncountered);
            }

            tallestTreeEncountered = minimumHeight;
            for (int rowIndex = rowCount - 1; rowIndex >= 0; rowIndex--)
            {
                ExamineTree(trees, rowIndex, columnIndex, ref tallestTreeEncountered);
            }
        }
    }

    private void ExamineTree(Tree[,] trees, int rowIndex, int columnIndex, ref int tallestTreeEncountered)
    {
        var tree = trees[rowIndex, columnIndex];
        if (tree.Height <= tallestTreeEncountered) { return; }
        tallestTreeEncountered = tree.Height;

        if (tree.Visible) { return; }
        tree.Visible = true;
    }

}
