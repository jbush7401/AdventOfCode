using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020
{
    class Day2 : Day
    {
        List<PasswordCandidate> passwordCandidates = new List<PasswordCandidate>();
        public void part1()
        {
            var lines = Utilities.ReadInput("Day2_P1_2020.txt");
            int checkCount;

            foreach(string line in lines)
            {
                passwordCandidates.Add(new PasswordCandidate(int.Parse(line.Substring(0, (line.IndexOf('-')))), int.Parse(line.Substring((line.IndexOf('-') + 1), (line.IndexOf(' ') - line.IndexOf('-')))), line[line.IndexOf(':') - 1], line.Substring(line.IndexOf(':') + 2, (line.Length - line.IndexOf(':') - 2))));
            }

            foreach (PasswordCandidate candidate in passwordCandidates)
            {
                checkCount = 0;
                foreach(char c in candidate.password)
                {
                    if (c == candidate.character)
                        checkCount++;
                }

                if (checkCount >= candidate.minCharCount && checkCount <= candidate.maxCharCount)
                    candidate.valid = true;

            }

            Console.WriteLine($"Part 1: {passwordCandidates.Count(t => t.valid == true)}");
        }

        public void part2()
        {
            foreach(PasswordCandidate candidate in passwordCandidates)
            {
                if (candidate.password[candidate.minCharCount - 1] == candidate.character ^ candidate.password[candidate.maxCharCount - 1] == candidate.character)
                    candidate.valid = true;
                else
                    candidate.valid = false;
            }

            Console.WriteLine($"Part 2: {passwordCandidates.Count(t => t.valid == true)}");
        }

        class PasswordCandidate
        {
            public int minCharCount;
            public int maxCharCount;

            public char character;

            public string password;
            public bool valid = false;

            public PasswordCandidate(int minCharCount, int maxCharCount, char character, string password)
            {
                this.minCharCount = minCharCount;
                this.maxCharCount = maxCharCount;
                this.character = character;
                this.password = password;
            }
        }
    }
}
