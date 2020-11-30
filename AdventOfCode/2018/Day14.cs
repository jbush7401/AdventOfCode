using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018
{
    class Day14 : Day
    {
        int input = 147061;
        int elf1Position = 0;
        int elf2Position = 1;
        int currentElf1Score;
        int currentElf2Score;
        int sum;
        int part2Found = 0;

        List<int> recipes = new List<int>();

        public void part1()
        {
            recipes.Add(3);
            recipes.Add(7);

            while (recipes.Count() <= input + 10 || part2Found == 0)
            {
                //Get the sum of the current two elf recipes
                currentElf1Score = recipes.ElementAt(elf1Position);
                currentElf2Score = recipes.ElementAt(elf2Position);
                sum = currentElf1Score + currentElf2Score;

                //Add the new recipes to the LinkedList
                if (sum > 9)
                {
                    recipes.Add(1);
                    CheckSequence();
                    recipes.Add(sum % 10);
                    CheckSequence();
                }
                else { 
                    recipes.Add(sum);
                    CheckSequence();
                }

                //Update elf positions
                MoveElf(ref elf1Position);
                MoveElf(ref elf2Position);
            }

            Console.Write("Part 1: ");
            for(int x = 0; x <= 9; x++)
            {
                Console.Write(recipes.ElementAt(input + x).ToString());
            }
            Console.WriteLine();

            Console.WriteLine("Part 2: " + part2Found);
        }

        public void part2()
        {
        }

        List<int> GetIntArray(int num)
        {
            List<int> listOfInts = new List<int>();
            while (num > 0)
            {
                listOfInts.Add(num % 10);
                num = num / 10;
            }
            listOfInts.Reverse();
            return listOfInts;
        }

        void CheckSequence()
        {
            if(part2Found == 0) { 
                if (recipes.Count > 6) { 
                    if ((recipes[recipes.Count() - 1]) + (recipes[recipes.Count() - 2] * 10) + (recipes[recipes.Count() - 3] * 100) + (recipes[recipes.Count() - 4] * 1000) + (recipes[recipes.Count() - 5] * 10000) + (recipes[recipes.Count() - 6] * 100000) == input)
                    {
                        part2Found = recipes.Count() - 6;
                    }
                }
            }
        }

        void MoveElf(ref int elfPosition)
        {
            if ((elfPosition + recipes.ElementAt(elfPosition) + 1) > (recipes.Count() - 1)) { 
                elfPosition = elfPosition + ((recipes.ElementAt(elfPosition) + 1) % recipes.Count());
                if (elfPosition > recipes.Count() - 1)
                    elfPosition -= recipes.Count();
            }
            else
                elfPosition = elfPosition + 1 + recipes.ElementAt(elfPosition);
        }
    }
}
