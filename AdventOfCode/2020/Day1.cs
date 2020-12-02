using System;
using System.Collections.Generic;

namespace AdventOfCode._2020
{
    class Day1 : Day
    {
        List<int> expenses = new List<int>();
        public void part1()
        {
            var lines = Utilities.ReadInput("Day1_P1_2020.txt");
            bool done = false;
            int i = 0;
            foreach(string line in lines)
            {
                expenses.Add(int.Parse(line));
            }

            while (!done)
            {
                for(int j = i+1; j < expenses.Count; j++)
                {
                    if(expenses[i] + expenses[j] == 2020) { 
                        Console.WriteLine($"Part 1: {expenses[i] * expenses[j]}");
                        done = true;
                        break;
                    }
                }
                i++;
            }
        }

        public void part2()
        {
            Console.WriteLine($"Part 2: {runPart2()}");
           
        }

        int runPart2()
        {
            bool done = false;
            int i = 0;

            while (!done)
            {
                for (int j = i + 1; j < expenses.Count; j++)
                {
                    for (int k = j + 1; k < expenses.Count; k++)
                    {
                        if (expenses[i] + expenses[j] + expenses[k] == 2020)
                        {
                            return expenses[i] * expenses[j] * expenses[k];
                        }
                    }
                }
                i++;
            }
            return 0;
        }
    }
}
