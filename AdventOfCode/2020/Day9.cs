using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020
{
    class Day9 : Day
    {
        int preambleLength = 25;

        List<long> theNumbers = new List<long>();
        List<long> rangeCheck = new List<long>();
        long inValidNumberFromPart1;
        long sum = 0;

        public void part1()
        {
            var lines = Utilities.ReadInput("Day9_P1_2020.txt");
            
            foreach (string line in lines)
                theNumbers.Add(long.Parse(line));

            for(int currentNumberCheck = preambleLength; currentNumberCheck < theNumbers.Count; currentNumberCheck++)
            {
                if(!findMatch(currentNumberCheck))
                {
                    inValidNumberFromPart1 = theNumbers[currentNumberCheck];
                    Console.WriteLine($"Part 1: {inValidNumberFromPart1}");
                    break;
                }    
            }
        }

        public void part2()
        {
            for (int x = 0; x < theNumbers.IndexOf(inValidNumberFromPart1); x++) { 
                if(findRangeForPart2(x))
                {
                    Console.WriteLine($"Part 2 {rangeCheck.Min() + rangeCheck.Max()}");
                    break;
                }
            }
        }

        bool findRangeForPart2(int indexToStart)
        {
            rangeCheck.Clear();
            sum = 0;

            while(sum < inValidNumberFromPart1)
            {
                rangeCheck.Add(theNumbers[indexToStart]);
                sum = rangeCheck.Sum(t => t);
                if (sum == inValidNumberFromPart1)
                    return true;
                indexToStart++;
            }
            return false;
        }

        bool findMatch(int currentNumberCheck)
        {
            for (int x = currentNumberCheck - preambleLength; x < currentNumberCheck; x++)
                for (int y = x + 1; y < currentNumberCheck; y++)
                {
                    if (theNumbers[x] + theNumbers[y] == theNumbers[currentNumberCheck])
                    {
                        return true;
                    }
                }
            return false;
        }
    }
}
