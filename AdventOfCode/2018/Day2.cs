using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018
{
    class Day2 : Day
    {
        public void part1()
        {
            HashSet<char> checkedLetters = new HashSet<char>();
            int charRepeatCount;
            int repeatsTwice = 0;
            int repeatsThrice = 0;
            int stringLoc = 0;
            int checkSum;
            bool foundTwo;
            bool foundThree;

            var lines = Utilities.ReadInput("Day2_P1_2018.txt");

            foreach(var line in lines)
            {
                checkedLetters.Clear();
                stringLoc = 0;
                foundTwo = false;
                foundThree = false;
                foreach(char c in line)
                {
                    if (!checkedLetters.Contains(c))
                    {
                        charRepeatCount = 0;
                        for(int i = stringLoc; i < line.Length; i++)
                        {
                            if (line[i] == c)
                                charRepeatCount++;
                        }
                        if (charRepeatCount == 3 & !foundThree)
                        { 
                            repeatsThrice++;
                            foundThree = true;
                        }
                        else if (charRepeatCount == 2 & !foundTwo) { 
                            repeatsTwice++;
                            foundTwo = true;
                        }

                        checkedLetters.Add(c);
                    }
                    stringLoc++;
                }
            }

            checkSum = repeatsTwice * repeatsThrice;
            Console.WriteLine($"Part 1: {checkSum}");
        }

        public void part2()
        {
            var lines = Utilities.ReadInput("Day2_P2_2018.txt").ToList();
            string line;
            int linesCounter = 0;
            int lineCheckCounter = 0;
            string tempCheck;
            int charDifference;
            int charDifferenceLoc = 0;
            string correctId = "";

            //foreach (var line in lines)
            while (correctId == "")
            {
                line = lines[linesCounter];
                linesCounter++;
                for (int i = linesCounter; i < lines.Count(); i++) {
                    tempCheck = lines[i];
                    charDifference = 0;
                    lineCheckCounter = 0;
                    foreach(char c in line)
                    {
                        if (c != tempCheck[lineCheckCounter])
                        {
                            charDifference++;
                            charDifferenceLoc = lineCheckCounter; 
                        }
                        if (charDifference > 1)
                            break;
                        lineCheckCounter++;
                    }
                    if (charDifference == 1) { 
                        correctId = line.Remove(charDifferenceLoc, 1);
                        break;
                    }
                }
            }
            Console.WriteLine($"Part 1: {correctId}");
        }
    }
}
