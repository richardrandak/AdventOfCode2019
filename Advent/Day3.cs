using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advent
{
    class Day3 : IPuzzle
    {
        public string SolvePart1(string input)
        {
            var firstLine = input.Split(Environment.NewLine)[0].Split(",");
            var secondLine = input.Split(Environment.NewLine)[1].Split(",");

            var path1 = CreatePath(firstLine);
            var path2 = CreatePath(secondLine);

            var minDist = int.MaxValue;
            foreach (var point in path1)
            {
                if (point.x == 0 && point.y == 0) continue;

                if (path2.Contains(point)) //intersection
                {
                    var dist = point.x + point.y;
                    if (dist < minDist)
                        minDist = dist;
                }
            }

            return minDist.ToString();
        }

        public string SolvePart2(string input)
        {
            var firstLine = input.Split(Environment.NewLine)[0].Split(",");
            var secondLine = input.Split(Environment.NewLine)[1].Split(",");

            var path1 = CreatePath(firstLine);
            var path2 = CreatePath(secondLine);

            var minDist = int.MaxValue;
            foreach (var point in path1)
            {
                if (point.x == 0 && point.y == 0) continue;

                if (path2.Contains(point)) //intersection
                {
                    var dist = path1.GetNumberOfSteps(point) + path2.GetNumberOfSteps(point);
                    if (dist < minDist)
                        minDist = dist;
                }
            }

            return minDist.ToString();
        }

        private Path CreatePath(string[] instructions)
        {
            var path = new Path();
            path.Add(new Point(0, 0));

            foreach (var inst in instructions)
            {
                var dir = inst[0];
                var length = int.Parse(inst.Replace("" + dir, ""));

                switch (dir)
                {
                    case 'R':
                        for (int i = 1; i <= length; i++)
                            path.Add(new Point(path.Last().x + 1, path.Last().y));
                        break;
                    case 'L':
                        for (int i = 1; i <= length; i++)
                            path.Add(new Point(path.Last().x - 1, path.Last().y));
                        break;
                    case 'U':
                        for (int i = 1; i <= length; i++)
                            path.Add(new Point(path.Last().x, path.Last().y + 1));
                        break;
                    case 'D':
                        for (int i = 1; i <= length; i++)
                            path.Add(new Point(path.Last().x, path.Last().y - 1));
                        break;
                }
            }
            return path;
        }

        class Point
        {
            public int x;
            public int y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public override string ToString()
            {
                return $"[{x},{y}]";
            }
            public override bool Equals(object obj)
            {
                return ToString().Equals(obj.ToString());
            }

            public override int GetHashCode()
            {
                return ToString().GetHashCode();
            }
        }

        class Path : IEnumerable<Point>
        {
            private readonly List<Point> positionsList = new List<Point>();
            private readonly ISet<Point> positionsSet = new HashSet<Point>();

            public void Add(Point point)
            {
                positionsList.Add(point);
                positionsSet.Add(point);
            }

            public Point Last()
            {
                return positionsList.Last();
            }

            public bool Contains(Point point)
            {
                return positionsSet.Contains(point);
            }

            public IEnumerator<Point> GetEnumerator()
            {
                return positionsList.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return positionsList.GetEnumerator();
            }

            internal int GetNumberOfSteps(Point point)
            {
                return positionsList.IndexOf(point);
            }
        }
    }
}
