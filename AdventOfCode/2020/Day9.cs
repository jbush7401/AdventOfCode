using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020
{
    class Day9 : Day
    {
        int preambleLength = 25;

        List<long> theNumbers = new List<long>();
        long inValidNumberFromPart1, sum, min, max = 0;
        int indexOfInvalid;

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
                    indexOfInvalid = currentNumberCheck;
                    Console.WriteLine($"Part 1: {inValidNumberFromPart1}");
                    break;
                }    
            }
        }

        public void part2()
        {
            for (int x = 0; x < indexOfInvalid; x++) { 
                if(findRangeForPart2(x))
                {
                    Console.WriteLine($"Part 2 {min + max}");
                    break;
                }
            }
        }

        bool findRangeForPart2(int indexToStart)
        {
            sum = 0;
            min = theNumbers[indexToStart];
            max = theNumbers[indexToStart];
            while (sum < inValidNumberFromPart1)
            {
                sum += theNumbers[indexToStart];
                if (theNumbers[indexToStart] < min)
                    min = theNumbers[indexToStart];
                if (theNumbers[indexToStart] > max)
                    max = theNumbers[indexToStart];
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
