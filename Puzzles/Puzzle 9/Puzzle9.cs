internal class Puzzle9 : BasePuzzle<IEnumerable<Facing2D>, int>
{
    internal override IEnumerable<Facing2D> GetDataset()
    {
        var data = File.ReadLines(@".\puzzle 9\input.txt");

        return data.SelectMany(source => GetFacings(source));

        static IEnumerable<Facing2D> GetFacings(string source)
        {
            for (int i = 0; i < int.Parse(source.Substring(2)); i++)
            {
                yield return ParseDirection(source[0]);
            }
        };

        static Facing2D ParseDirection(char direction) => direction switch
        {
            'U' => Facing2D.Up,
            'R' => Facing2D.Right,
            'D' => Facing2D.Down,
            'L' => Facing2D.Left,
            _ => throw new ArgumentException(nameof(direction), $"Value not expected: {direction}")
        };
    }

    internal override int PartOne(IEnumerable<Facing2D> instructions)
    {
        var visitedPoints = new HashSet<Point2D>() { Point2D.zero };
        var head = Point2D.zero;
        var tail = Point2D.zero;
        foreach (var instruction in instructions)
        {

            if (FollowInstruction(ref head, ref tail, Point2D.Facing(instruction)) != Point2D.zero) { visitedPoints.Add(tail); }
        }

        return visitedPoints.Count;
    }

    private Point2D FollowInstruction(ref Point2D head, ref Point2D tail, Point2D direction)
    {
        head += direction;
        var distance = head - tail;
        var tailDirection = Point2D.Sign(head - tail);

        if (distance == tailDirection)
        {
            return Point2D.zero;
        }

        tail += tailDirection;

        return tailDirection;
    }

    internal override int PartTwo(IEnumerable<Facing2D> instructions)
    {
        List<Point2D> knots = new List<Point2D>();
        var visitedPoints = new HashSet<Point2D>() { Point2D.zero };

        for (int i = 0; i < 10; i++)
        {
            knots.Add(Point2D.zero);
        }

        foreach (var instruction in instructions)
        {
            var direction = Point2D.Facing(instruction);
            for (int i = 0; i < knots.Count - 1; i++)
            {
                var head = knots[i];
                var tail = knots[i + 1];

                direction = FollowInstruction(ref head, ref tail, direction);
                knots[i] = head;

                if (direction == Point2D.zero)
                {
                    break;
                }

                if (i + 1 == 9)
                {
                    knots[9] = tail;
                    visitedPoints.Add(tail);

                }
            }
        }

        return visitedPoints.Count;
    }
}
