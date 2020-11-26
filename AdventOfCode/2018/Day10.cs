using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018
{
    class Day10 : Day
    {
        IEnumerable<string> lines;

        public void part1()
        {
            Initialize();

            List<Point> points = new List<Point>();

            int minX, maxX, minY, maxY;
            int seconds = 0;

            foreach (var line in lines)
            {
                points.Add(new Point(int.Parse(line.Substring(10, 6)), int.Parse(line.Substring(17, 7)), int.Parse(line.Substring(36, 2)), int.Parse(line.Substring(39, 3))));
            }

            maxX = points.Max(t => t.x);
            minX = points.Min(t => t.x);
            maxY = points.Max(t => t.y);
            minY = points.Min(t => t.y);

            while (maxX - minX > 100 || maxY - minY > 100)
            {
                foreach(Point p in points)
                {
                    p.x += p.xVel;
                    p.y += p.yVel;
                }

                maxX = points.Max(t => t.x);
                minX = points.Min(t => t.x);
                maxY = points.Max(t => t.y);
                minY = points.Min(t => t.y);
                seconds++;
            }

            while (maxX - minX < 200)
            {
                Console.Clear();
                for (int y = points.Min(t => t.y); y <= points.Max(t => t.y); y++)
                   {
                    for (int x = points.Min(t => t.x); x <= points.Max(t => t.x); x++)
                    {
                        if (points.Where(t => t.x == x).Where(t => t.y == y).Any() != false)
                            Console.Write("*");
                        else
                            Console.Write(" ");
                    }
                    Console.WriteLine();
                }

                foreach (Point p in points)
                {
                    p.x += p.xVel;
                    p.y += p.yVel;
                }

                maxX = points.Max(t => t.x);
                minX = points.Min(t => t.x);
                maxX = points.Max(t => t.x);
                minX = points.Min(t => t.x);
                Console.Write($"Seconds {seconds}");
                Console.ReadLine();
                seconds++;
            }

        }

        public void part2()
        {
        }

        public void Initialize()
        {
            lines = Utilities.ReadInput("Day10_P1_2018.txt");
        }

        class Point
        {
            public int x;
            public int y;

            public int xVel = 0;
            public int yVel = 0;

            public Point(int x, int y, int xVel, int yVel)
            {
                this.x = x;
                this.y = y;
                this.xVel = xVel;
                this.yVel = yVel;
            }
        }
    }
}
