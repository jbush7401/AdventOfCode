using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020
{
    class Day10 : Day
    {
        List<int> input = new List<int>();
        List<int> singleDigitDifferenceRange = new List<int>();

        public void part1()
        {
            var lines = Utilities.ReadInput("Day10_P1_2020.txt");
            Dictionary<int, int> differences = new Dictionary<int, int>();

            foreach(string line in lines)
            {
                input.Add(int.Parse(line));
            }

            input.Sort();

            differences[input[0]] = 1;

            for (int x = 0; x < input.Count-1; x++)
            {
                if (differences.ContainsKey(input[x + 1] - input[x]))
                    differences[input[x + 1] - input[x]] += 1;
                else
                    differences[input[x + 1] - input[x]] = 1;
            }

            differences[3] += 1;

            Console.WriteLine(differences[1] * differences[3]);
        }

        public void part2()
        {
            input.Add(input.Max() + 3);
            int previousNumber = 0;
            long adaptersCanSuckItFinalAnswer = 1;

            List<int> multipliers = new List<int>();
            Dictionary<int, int> alreadyDoneComboCalcs = new Dictionary<int, int>();

            singleDigitDifferenceRange.Add(0);
            foreach (int i in input)
            {
                if (i - previousNumber == 1)
                {
                    singleDigitDifferenceRange.Add(i);
                }
                else
                {
                    if (singleDigitDifferenceRange.Count > 2) {
                        if (alreadyDoneComboCalcs.ContainsKey(singleDigitDifferenceRange.Count))
                            multipliers.Add(alreadyDoneComboCalcs[singleDigitDifferenceRange.Count]);
                        else
                        {
                            alreadyDoneComboCalcs[singleDigitDifferenceRange.Count] = numberOfCombos();
                            multipliers.Add(alreadyDoneComboCalcs[singleDigitDifferenceRange.Count]);
                        }
                    }
                    singleDigitDifferenceRange.Clear();
                    singleDigitDifferenceRange.Add(i);
                }
                previousNumber = i;
            }
            foreach (int i in multipliers)
            {
                adaptersCanSuckItFinalAnswer *= i;
            }
            Console.WriteLine($"Part 2: {adaptersCanSuckItFinalAnswer}");
        }

        int numberOfCombos()
        {
            int count = singleDigitDifferenceRange.Count - 2;
            List<List<int>> PossibleCombos = new List<List<int>>();
            PossibleCombos.AddRange(GetAllCombos(singleDigitDifferenceRange));

            //Remove bad combos
            foreach(var combo in PossibleCombos.ToList())
            {
                if (!combo.Contains(singleDigitDifferenceRange[0]) || !combo.Contains(singleDigitDifferenceRange[singleDigitDifferenceRange.Count -1]))
                {
                    PossibleCombos.Remove(combo);
                } else
                for(int i = 1; i < combo.Count; i++)
                {
                    if (combo[i] - combo[i - 1] > 3)
                        PossibleCombos.Remove(combo);
                }
            }
            return PossibleCombos.Count;
        }

        public List<List<int>> GetAllCombos(List<int> list)
        {
            int comboCount = (int)Math.Pow(2, list.Count) - 1;
            List<List<int>> result = new List<List<int>>();
            for (int i = 1; i < comboCount + 1; i++)
            {
                // make each combo here
                result.Add(new List<int>());
                for (int j = 0; j < list.Count; j++)
                {
                    if ((i >> j) % 2 != 0)
                        result.Last().Add(list[j]);
                }
            }
            return result;
        }
    }
}
