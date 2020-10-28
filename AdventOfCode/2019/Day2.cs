using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdventOfCode._2019
{
    class Day2 : Day
    {
        public void part1()
        {
            int currentPosition = 0;
            int command;
            int result;

            var lines = Utilities.ReadInputDelimited("Day2_P1_2019.txt");

            int[] input = Array.ConvertAll<string, int>(lines.ToString().Split(','), Convert.ToInt32);

            input[1] = 12;
            input[2] = 2;

            while (currentPosition <= input.Length)
            {
                command = input[currentPosition];

                if (command == 1) { 
                    result = input[input[currentPosition + 1]] + input[input[currentPosition + 2]];
                    input[input[currentPosition + 3]] = result;
                }
                if (command == 2) {
                    result = input[input[currentPosition + 1]] * input[input[currentPosition + 2]];
                    input[input[currentPosition + 3]] = result;
                }
                if (command == 99)
                    break;
                
                currentPosition += 4;
            }

            Console.WriteLine("Part 1: " + input[0]);
        }

        public void part2()
        {
        }
    }
}
