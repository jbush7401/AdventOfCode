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
        public void part1()
        {
            var lines = Utilities.ReadInput("Day6_P1_2018.txt");
            HashSet<Point> points = new HashSet<Point>();

            foreach(var line in lines)
            {
                points.Add(new Point(int.Parse(line.Substring(0, line.IndexOf(','))), 0));
            }
        }

        public void part2()
        {

        }

        class Point
        {
            int x;
            int y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
    }
}
