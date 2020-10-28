using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdventOfCode._2018
{
    class Day5 : Day
    {

        public string part1Result;
        public void part1()
        {
            var line = Utilities.ReadInputDelimited("Day5_P1_2018.txt");

            part1Result = FindPattern(line);
            Console.WriteLine($"Part 1 {part1Result.Length}");
        }

        public void part2()
        {
            int smallestLength = part1Result.Length;
            int check = 0;
            string tempCheck;
            for (int i = 65; i <= 90; i++)
            {
                tempCheck = part1Result.Replace(((char)i).ToString(), "").Replace(((char)(i + 32)).ToString(), "");
                check = FindPattern(tempCheck).Length;
                if (check < smallestLength)
                    smallestLength = check;
            }

            Console.WriteLine($"Part 2: {smallestLength}");
        }

        string FindPattern(string input)
        {
            string tempInput = input;
            bool done = false;
            while (!done)
            {
                for (int i = 0; i < tempInput.Length - 1; i++)
                {
                    if(Math.Abs(tempInput[i] - tempInput[i+1] )== 32)
                    {
                        tempInput = tempInput.Remove(i, 2);
                        break;
                    }
                    if (i == tempInput.Length - 2)
                    {
                        done = true;
                        break;
                    }
                }
            }
            return tempInput;
        }
    }
}
