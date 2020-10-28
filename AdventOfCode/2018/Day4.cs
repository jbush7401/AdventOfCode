using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2018
{
    class Day4 : Day
    {
        public void part1()
        {
            var lines = Utilities.ReadInput("Day4_P1_2018.txt");

            process(lines, 1);
        }

        public void part2()
        {
            var lines = Utilities.ReadInput("Day4_P1_2018.txt");

            process(lines, 2);
        }

        void process(IEnumerable<string> lines, int part)
        {
            Dictionary<int, int[]> guards = new Dictionary<int, int[]>();
            DateTime fallsAsleep = new DateTime();
            DateTime wakesUp = new DateTime();

            HashSet<Instruction> instructions = new HashSet<Instruction>();

            int currentGuard = 0;

            int hoursSum = 0;
            int mostHours = 0;
            int guardMostHours = 0;
            int biggestMinuteAsleep = 0;
            int maxIndex = 0;

            foreach (var line in lines)
            {
                instructions.Add(new Instruction(DateTime.Parse(line.Substring(1, 16)), line.Substring(19, line.Length - 19)));
            }

            List<Instruction> sortedInstructions = instructions.ToList();
            sortedInstructions.Sort((x, y) => DateTime.Compare(x.login, y.login));

            foreach (var item in sortedInstructions)
            {
                if (item.text[0] == 'G')
                {
                    currentGuard = int.Parse(Regex.Match(item.text, @"\d+").Value);
                }
                else if (item.text[0] == 'f')
                {
                    fallsAsleep = item.login;
                }
                else
                {
                    wakesUp = item.login;
                    if (!guards.ContainsKey(currentGuard))
                    {
                        guards.Add(currentGuard, new int[60]);
                    }
                    for (int x = fallsAsleep.Minute; x < wakesUp.Minute; x++)
                    {
                        guards[currentGuard][x]++;
                    }
                }
            }
            if (part == 1)
            {
                foreach (KeyValuePair<int, int[]> guard in guards)
                {
                    hoursSum = guard.Value.Sum();
                    if (hoursSum > mostHours)
                    {
                        mostHours = hoursSum;
                        guardMostHours = guard.Key;
                    }
                }
            }
            else
            {
                foreach (KeyValuePair<int, int[]> guard in guards)
                {
                    if (guard.Value.Max() > biggestMinuteAsleep)
                    {
                        guardMostHours = guard.Key;
                        biggestMinuteAsleep = guard.Value.Max();
                        maxIndex = guard.Value.ToList().IndexOf(biggestMinuteAsleep);
                    }
                }
            }

            int mostMinute = guards[guardMostHours].Max();
            int mostMinuteIndex = Array.IndexOf(guards[guardMostHours], mostMinute);

            if(part == 1)
                Console.WriteLine($"Part 1: {guardMostHours * mostMinuteIndex}");
            else {
                Console.WriteLine($"Part 2: {guardMostHours * maxIndex}");
            }
        }
        class Instruction
        {
            public DateTime login;
            public string text;
            public Instruction(DateTime login, string text)
            {
                this.login = login;
                this.text = text;
            }
        }
    }
}
