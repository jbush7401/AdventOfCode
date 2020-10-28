using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdventOfCode._2018
{
    class Day6 : Day
    {
        private List<Point> points;
        private int maxX;
        private int maxY;
        public void part1()
        {
            initialize();
            Point closest = null;
            int distanceClosest = int.MaxValue;
            int check;
            // Get closest point for each grid space
            Point[,] grid = new Point[maxX,maxY];

            for(int x = 0; x < maxX; x++)
                for(int y = 0; y < maxY; y++)
                {
                    foreach(var point in points)
                    {
                        check = getDistance(point, new Point(x, y));
                        if (check < distanceClosest)
                        {
                            distanceClosest = check;
                            closest = point;
                        } else if (check == distanceClosest)
                        {
                            closest = null;
                        }
                    }
                    grid[x, y] = closest;
                    if (closest != null) { 
                        closest.numOfLocationsClose++;
                        if (x == 0 || y == 0 || x == maxX || y == maxY)
                            closest.infinite = true;
                    }
                    closest = null;
                    distanceClosest = int.MaxValue;
                }

            Console.WriteLine($"Part 1: {points.Where(x => x.infinite == false).OrderByDescending(t => t.numOfLocationsClose).Select(x => x.numOfLocationsClose).FirstOrDefault()}");
        }

        public void part2()
        {
            int totDistanceCheck = 0;
            // Get closest point for each grid space
            bool[,] grid = new bool[maxX, maxY];
            int count = 0;
            for (int x = 0; x < maxX; x++)
                for (int y = 0; y < maxY; y++)
                {
                    foreach (var point in points)
                    {
                        totDistanceCheck += getDistance(point, new Point(x, y));
                    }
                    if (totDistanceCheck <= 10000)
                        count++;
                    totDistanceCheck = 0;
                }

            Console.WriteLine($"Part 2: {count}");
        }

        int getDistance(Point pointA, Point pointB)
        {
            return Math.Abs(pointA.x - pointB.x) + Math.Abs(pointA.y - pointB.y);
        }

        void initialize()
        {
            var lines = Utilities.ReadInput("Day6_P1_2018.txt");

            points = new List<Point>();

            int commaIndex;

            foreach (var line in lines)
            {
                commaIndex = line.IndexOf(',');
                points.Add(new Point(int.Parse(line.Substring(0, commaIndex)), int.Parse(line.Substring(commaIndex + 2, line.Length - commaIndex - 2))));
            }

            maxX = points.Max(i => i.x);
            maxY = points.Max(i => i.y);
        }

        public class Point
        {
            public int x;
            public int y;
            public bool infinite = false;
            public int numOfLocationsClose = 0;
            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
    }
}
