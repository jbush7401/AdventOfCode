using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    class Day3 : Day
    {
        char[,] slopes = new char[31, 323];

        int tobogganXCooord = 0;
        int tobogganYCoord = 0;

        public void part1()
        {
            var lines = Utilities.ReadInput("Day3_P1_2020.txt");
            
            int currentX = 0;
            int currentY = 0;

            foreach(string line in lines)
            {
                foreach (char c in line) { 
                    slopes[currentX, currentY] = c;
                    currentX++;
                }
                currentY++;
                currentX = 0;
            }

            Console.WriteLine($"Part 1: {Traverse(new Point(3, 1))}");
        }

        public void part2()
        {
            long product = 1;
            Point[] points = new Point[5] { new Point(1, 1), new Point(3, 1), new Point(5, 1), new Point(7, 1), new Point(1, 2) };

            foreach(Point p in points)
            {
                product *= Traverse(p);
            }

            Console.WriteLine($"Part 2: {product}");
        }

        public int Traverse(Point p)
        {
            tobogganXCooord = 0;
            tobogganYCoord = 0;
            int treeCount = 0;
            while (tobogganYCoord < (323 - p.y))
            {
                tobogganXCooord += p.x;
                tobogganYCoord += p.y;
                if (tobogganXCooord > 30)
                    tobogganXCooord -= 31;

                if (slopes[tobogganXCooord, tobogganYCoord] == '#')
                    treeCount++;
            }
            return treeCount;
        }

        public class Point
        {
            public int x;
            public int y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
    }
}
