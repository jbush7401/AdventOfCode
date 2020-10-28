using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018
{
    class Day3 : Day
    {
        public void part1()
        {
            var lines = Utilities.ReadInput("Day3_P1_2018.txt");
            int leftOffset;
            int topOffset;
            int width;
            int height;
            string[] parts;
            int count = 0;

            int[,] blanket = new int[1000,1000];

            foreach(var line in lines)
            {
                parts = line.Split(' ');
                leftOffset = int.Parse(parts[2].Substring(0, parts[2].IndexOf(',')));
                topOffset = int.Parse(parts[2].Substring(parts[2].IndexOf(',') + 1, (parts[2].IndexOf(':') - parts[2].IndexOf(',') - 1)));
                width = int.Parse(parts[3].Substring(0, parts[3].IndexOf('x')));
                height = int.Parse(parts[3].Substring(parts[3].IndexOf('x') + 1, (parts[3].Length - parts[3].IndexOf('x') - 1)));

                for (int x = leftOffset; x < leftOffset + width; x++)
                    for (int y = topOffset; y < topOffset + height; y++)
                        blanket[x, y]++;
            }

            for (int x = 0; x <= 999; x++)
                for (int y = 0; y <= 999; y++)
                {
                    if (blanket[x, y] > 1)
                        count++;
                }

            Console.WriteLine($"Part 1: {count}");
        }

        public void part2()
        {
            var lines = Utilities.ReadInput("Day3_P1_2018.txt");
            int id;
            int leftOffset;
            int topOffset;
            int width = 0;
            int height;
            string[] parts;
            HashSet<int> ids = new HashSet<int>();

            int[,] blanket = new int[1000, 1000];

            foreach (var line in lines)
            {
                parts = line.Split(' ');
                id = int.Parse(parts[0].Remove(0, 1));
                ids.Add(id);
                leftOffset = int.Parse(parts[2].Substring(0, parts[2].IndexOf(',')));
                topOffset = int.Parse(parts[2].Substring(parts[2].IndexOf(',') + 1, (parts[2].IndexOf(':') - parts[2].IndexOf(',') - 1)));
                width = int.Parse(parts[3].Substring(0, parts[3].IndexOf('x')));
                height = int.Parse(parts[3].Substring(parts[3].IndexOf('x') + 1, (parts[3].Length - parts[3].IndexOf('x') - 1)));

                for (int x = leftOffset; x < leftOffset + width; x++)
                    for (int y = topOffset; y < topOffset + height; y++)
                    {
                        if (blanket[x, y] == 0)
                            blanket[x, y] = id;
                        else
                        {
                            if(ids.Contains(blanket[x, y]))
                                ids.Remove(blanket[x, y]);
                            ids.Remove(id);
                        }    
                    } 
            }



            Console.WriteLine($"Part 2: {ids.First()}");
        }
    }
}
