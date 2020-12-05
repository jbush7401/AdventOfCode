using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    class Day5 : Day
    {
        List<int> seatIds = new List<int>();
        public void part1()
        {
            var lines = Utilities.ReadInput("Day5_P1_2020.txt");

            int largestSeatId = 0;
            int row;
            int rowDivider;
            int columnDivider;
            int column;
            int seatId;

            foreach(string line in lines)
            {
                rowDivider = 64;
                columnDivider = 4;
                row = 0;
                column = 0;

                for(int i = 0; i <= 6; i++)
                {
                    if (line[i] == 'B')
                        row += rowDivider;

                    rowDivider /= 2;
                }

                for(int i = 7; i <= 9; i++)
                {
                    if (line[i] == 'R')
                        column += columnDivider;

                    columnDivider /= 2;
                }

                seatId = row * 8 + column;
                seatIds.Add(seatId);

                if (seatId > largestSeatId) {
                    largestSeatId = seatId;
                }
            }
            Console.WriteLine($"Part 1: {largestSeatId}");
        }

        public void part2()
        {
            int min = seatIds.Min(t => t);

            foreach(int x in seatIds.OrderBy(t => t))
            {
                if (x != min)
                {
                    Console.WriteLine($"Part 2: {min}");
                    break;
                }
                min++;
            }
        }
    }
}
