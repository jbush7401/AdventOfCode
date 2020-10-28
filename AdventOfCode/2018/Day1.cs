using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode._2018
{
    class Day1 : Day
    {
        public void part1()
        {
            var lines = Utilities.ReadInput("Day1_P1_2018.txt");
            Console.WriteLine("Part 1: " + TotalFrequency(lines));
        }

        public void part2()
        {
            var lines = Utilities.ReadInput("Day1_P2_2018.txt");
            Console.WriteLine("Part 2: " + FirstFreqTwice(lines));
        }

        private int TotalFrequency(IEnumerable<string> lines)
        {
            int totalFrequency = 0;
            foreach (string line in lines)
            {
                int digit = int.Parse(line);
                totalFrequency += digit;
            }

            return totalFrequency;
        }

        private int FirstFreqTwice(IEnumerable<string> lines)
        {
            HashSet<int> freqs = new HashSet<int>();
            int totalFrequency = 0;
            bool found = false;
            while (!found) { 
                foreach (string line in lines)
                {
                    int digit = int.Parse(line);
                    totalFrequency += digit;
                    if (freqs.Contains(totalFrequency))
                        return totalFrequency;
                    else
                        freqs.Add(totalFrequency);

                }
            }
            return 0;
        }
    }
}

   
