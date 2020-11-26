using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018
{
    class Day12 : Day
    {
        //#Part 1 Variables
        List<char> currentGeneration = new List<char>();
        List<char> nextGeneration = new List<char>();
        List<Pattern> patterns = new List<Pattern>();

        int firstIndex;
        int lastIndex;
        int putNumber;

        public void part1()
        {
            Initialize();
            int insertIndex = 2;
            int sum = 0;

            for (int i = 1; i <= 20; i++) {
                insertIndex = 2;
                nextGeneration.AddRange(currentGeneration.GetRange(0, 2));
                for(int genIndex = 0; genIndex <= currentGeneration.Count - 5; genIndex++) { 
                    foreach(Pattern p in patterns)
                    {
                        if (ComparePattern(currentGeneration.GetRange(genIndex, 5), p)){
                            nextGeneration.Add(p.nextGenResult);
                            break;
                        }
                    }
                    insertIndex++;
                }
                nextGeneration.AddRange(currentGeneration.GetRange(currentGeneration.Count - 2, 2));
                currentGeneration.Clear();
                currentGeneration.AddRange(nextGeneration);
                CheckDotBuffer();
                nextGeneration.Clear();
            }

            putNumber = firstIndex;
            foreach(char x in currentGeneration)
            {
                if (x == '#')
                    sum += putNumber;
                putNumber++;
            }

            Console.WriteLine($"Part 1: {sum}");
        }

        public void part2()
        {
            //I used a calculator after finding a pattern between 50/500/5000 etc
            Initialize();
            long sum = 0;
            long prevSum = 0;
            long prevDifference = 0;
            long difference = 0;
            int insertIndex = 2;
            int repeatCount = 0;

            for (long i = 1; i <= 50000000000; i++)
            {
                insertIndex = 2;
                nextGeneration.AddRange(currentGeneration.GetRange(0, 2));
                for (int genIndex = 0; genIndex <= currentGeneration.Count - 5; genIndex++)
                {
                    foreach (Pattern p in patterns)
                    {
                        if (ComparePattern(currentGeneration.GetRange(genIndex, 5), p))
                        {
                            nextGeneration.Add(p.nextGenResult);
                            break;
                        }
                    }
                    insertIndex++;
                }
                nextGeneration.AddRange(currentGeneration.GetRange(currentGeneration.Count - 2, 2));
                currentGeneration.Clear();
                currentGeneration.AddRange(nextGeneration);
                CheckDotBuffer();
                nextGeneration.Clear();
                putNumber = firstIndex;

                sum = 0;
                putNumber = firstIndex;
                foreach (char x in currentGeneration)
                {
                    if (x == '#')
                        sum += putNumber;
                    putNumber++;
                }

                difference = sum - prevSum;
                if (difference == prevDifference)
                {
                    if (repeatCount == 5)
                    {
                        sum = (sum + (difference * (50000000000 - i)));
                        break;
                    }
                    repeatCount++;
                }
                else
                    repeatCount = 0;
                prevSum = sum;
                prevDifference = difference;
            }
            Console.WriteLine($"Part 2: {sum}");
        }

        public void CheckDotBuffer() 
        {

            int firstFlower = currentGeneration.IndexOf('#');
            int frontPadding = 5 - firstFlower;
            
            if(firstFlower < 5) { 
                currentGeneration.InsertRange(0, Enumerable.Repeat('.', frontPadding));
                firstIndex = firstIndex - frontPadding;
            }

            if(firstFlower > 9)
            {
                currentGeneration.RemoveRange(0, 5);
                firstIndex += 5;
            }
                

            int lastFlower = currentGeneration.LastIndexOf('#');
            int backPadding = currentGeneration.Count - lastFlower + 4;

            if (lastFlower > currentGeneration.Count - 5) { 
                currentGeneration.AddRange(Enumerable.Repeat('.', backPadding));
                lastIndex = lastIndex + backPadding;
            }

        }

        bool ComparePattern(List<char> check, Pattern p)
        {
            if (check.SequenceEqual(p.pattern) )
                return true;
            return false;
        }


        public void Initialize()
        {
            currentGeneration.Clear();
            nextGeneration.Clear();
            var lines = Utilities.ReadInput("Day12_P1_2018.txt");
            int counter = 1;
            foreach (var line in lines)
            {
                if (counter == 1)
                {
                    string input = line.Substring(15, line.Length - 15);
                    currentGeneration = input.ToList();
                    firstIndex = 0;
                    lastIndex = currentGeneration.Count - 1;
                    CheckDotBuffer();
                }
                else if (counter == 2) { }
                else
                {
                     patterns.Add(new Pattern(line.Substring(0, 5).ToArray(), line[9]));
                }
                counter++;
            }
        }

        class Pattern
        {
            public char[] pattern = new char[5];

            public char nextGenResult;

            public Pattern(char[] pattern, char c)
            {
                this.pattern = pattern;
                this.nextGenResult = c;
            }
        }
    }
}
