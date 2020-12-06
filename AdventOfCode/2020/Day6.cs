using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    class Day6 : Day
    {
        string[] groups;
        int total = 0;
        HashSet<char> yeses = new HashSet<char>();

        public void part1()
        {
            var lines = Utilities.ReadInputDelimited("Day6_P1_2020.txt");

            groups = lines.Split(new string[] { "\r\n\r\n" },StringSplitOptions.RemoveEmptyEntries);

            foreach(string group in groups)
            {
                foreach (char c in group.Replace("\r\n", ""))
                {
                    yeses.Add(c);
                }
                total += yeses.Count();
                yeses.Clear();
            }

            Console.WriteLine($"Part 1: {total}");
        }

        public void part2()
        {
            total = 0;

            foreach(string group in groups)
            {
                foreach (char c in group.Replace("\r\n", ""))
                {
                    yeses.Add(c);
                }

                foreach (string person in group.Split(new string[] { "\r\n" },StringSplitOptions.RemoveEmptyEntries))
                {
                    foreach(char c in yeses.ToArray())
                    {
                        if (!person.Contains(c))
                            yeses.Remove(c);
                    }
                }

                total += yeses.Count();
            }

            Console.WriteLine($"Part 2: {total}");
        }
    }
}
