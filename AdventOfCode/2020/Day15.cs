using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    class Day15 : Day
    {
        public void part1()
        {
            Dictionary<int, int> input = new Dictionary<int, int>();
            string[] s = "6,13,1,15,2,0".Split(',').ToArray();

            for (int x = 0; x < s.Length-1; x++)
                input[int.Parse(s[x])] = x+1;

            int turn = input.Count + 1;
            int lastNum = int.Parse(s[s.Length-1]);
            int temp;

            for(int i = turn; i <= 2020; i++)
            {
                if (input.ContainsKey(lastNum)) {
                    temp = lastNum;
                    lastNum = (i) - input[lastNum];
                    input[temp] = i;
                }
                else
                {
                    input[lastNum] = i;
                    lastNum = 0;
                }
            }

            Console.WriteLine($"Part 1: {input.Where(t => t.Value == 2020).SingleOrDefault().Key}");

            for (int i = 2021; i <= 30000000; i++)
            {
                if (input.ContainsKey(lastNum))
                {
                    temp = lastNum;
                    lastNum = (i) - input[lastNum];
                    input[temp] = i;
                }
                else
                {
                    input[lastNum] = i;
                    lastNum = 0;
                }
            }

            Console.WriteLine($"Part 2: {input.Where(t => t.Value == 30000000).SingleOrDefault().Key}");
        }

        public void part2()
        {
        }
    }
}
