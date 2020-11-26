using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018
{
    class Day11 : Day
    {
        int puzzleInput = 9995;
        int[,] powerGrid = new int[300, 300];

        public void part1()
        {
            int[,] fuelTotals = new int[298, 298];
            for (int x = 0; x < 300; x++)
            {
                for (int y = 0; y < 300; y++)
                {
                    powerGrid[x, y] = calculatePowerGrid(x + 1, y + 1);
                }
            }

            int LargestPower = 0;
            int check;
            FuelCell largest = new FuelCell();
            for (int x = 0; x < 298; x++)
            {
                for (int y = 0; y < 298; y++)
                {
                    check = powerGrid[x, y] + powerGrid[x + 1, y] + powerGrid[x + 2, y] + powerGrid[x, y + 1] + powerGrid[x + 1, y + 1] + powerGrid[x + 2, y + 1] + powerGrid[x, y + 2] + powerGrid[x + 1, y + 2] + powerGrid[x + 2, y + 2];
                    if (check > LargestPower) { 
                        LargestPower = check;
                        largest.x = x + 1;
                        largest.y = y + 1;
                    }
                }
            }
            Console.WriteLine($"{largest.x},{largest.y}");
        }

        public void part2()
        {
            FuelCell check;
            FuelCell largest = new FuelCell();
            for (int x = 0; x < 300; x++)
            {
                for (int y = 0; y < 300; y++)
                {
                    check = checkSquares(new FuelCell(x, y));
                    if (check.largestPower > largest.largestPower)
                    {
                        largest = check;
                    }
                }
            }
            Console.WriteLine($"{largest.x + 1},{largest.y + 1},{largest.largestSize}");
        }

       

        FuelCell checkSquares(FuelCell c)
        {
            int largest = 0;
            int square = 0;
            int check = 0;

            while (square + c.x < 300 && square + c.y < 300)
            {
                for (int x = c.x; x <= c.x + square; x++)
                {
                    check += powerGrid[x, (square + c.y)];
                }
                for (int y = c.y; y <= c.y + square - 1; y++)
                {
                    check += powerGrid[(square + c.x), y];
                }
                    
                if (check > largest)
                {
                    c.largestPower = check;
                    c.largestSize = square+1;
                    largest = check;
                }
                square++;
            }
            return c;
        }

        int calculatePowerGrid(int x, int y)
        {
            return (((((x + 10) * y) + puzzleInput) * (x + 10)) / (int)Math.Pow(10, 3 - 1)) % 10 - 5;
        }

        class FuelCell
        {
            public int x;
            public int y;
            public int largestSize;
            public int largestPower;

            public FuelCell(int x = 0, int y = 0)
            {
                this.x = x;
                this.y = y;
            }
        }
    }
}
