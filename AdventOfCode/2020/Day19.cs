using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    enum RuleType { endNode, orNode, singleNode}
    

    class Day19 : Day
    {
        List<string> receivedMessages = new List<string>();
        Dictionary<int, Rule> rules = new Dictionary<int, Rule>();
        HashSet<List<string>> possibleMessages = new HashSet<List<string>>();
        List<string> finalPossibleMessage = new List<string>();
        List<string> matchedMessages = new List<string>();
        int longestMessage = 0;

        int matches = 0;
        public void part1()
        {
            InitializeData();
            // Process message data
            possibleMessages.Add(rules[0].nums.Select(t => t.ToString()).ToList());
            bool keepGoing = true;
            while (keepGoing)
            {
                keepGoing = false;
                foreach (List<string> possibleMessage in possibleMessages.ToList())
                {
                    if (processPossibleMessage(possibleMessage))
                    {
                        keepGoing = true;
                    }
                }
            }

            foreach (List<string> possibleMessage in possibleMessages)
            {
                finalPossibleMessage.Add(string.Join("", possibleMessage.ToArray()));
            }

            foreach (string s in finalPossibleMessage)
            {
                if (receivedMessages.Contains(s))
                {
                    matchedMessages.Add(s);
                    matches++;
                }
            }

            Console.WriteLine($"Part 1: {matches}");
        }

        public void part2()
        {
            
            rules[8].ruleType = RuleType.orNode;
            rules[8].nums = new List<int> { 42, 42, 8 };
            rules[11].ruleType = RuleType.orNode;
            rules[11].nums = new List<int> { 42, 31, 42, 11, 31 };

            possibleMessages.Clear();
            finalPossibleMessage.Clear();

            // Process message data
            possibleMessages.Add(rules[0].nums.Select(t => t.ToString()).ToList());
            bool keepGoing = true;
            while (keepGoing)
            {
                keepGoing = false;
                foreach (List<string> possibleMessage in possibleMessages.ToList())
                {
                    if (processPossibleMessage(possibleMessage))
                    {
                        keepGoing = true;
                    }
                }
            }

            foreach (List<string> possibleMessage in possibleMessages)
            {
                finalPossibleMessage.Add(string.Join("", possibleMessage.ToArray()));
            }

            foreach (string s in finalPossibleMessage)
            {
                if (receivedMessages.Contains(s))
                    matches++;
            }

            Console.WriteLine($"Part 2: {matches}");
        }

      

        public bool processPossibleMessage(List<string> possibleMessage)
        {
            bool stillMoreToProcess = false;
            for (int i = 0; i < possibleMessage.Count; i++)
            {
                int ruleIndex;
                if (int.TryParse(possibleMessage[i], out ruleIndex))
                {
                    if (rules[ruleIndex].ruleType == RuleType.endNode)
                        possibleMessage[i] = rules[ruleIndex].finalChar;
                    else if (rules[ruleIndex].ruleType == RuleType.singleNode)
                    {
                        possibleMessage[i] = rules[ruleIndex].nums[0].ToString();
                        if(rules[ruleIndex].nums.Count > 1)
                            possibleMessage.Insert(i + 1, rules[ruleIndex].nums[1].ToString());
                        return true;
                    }
                    else
                    {
                        List<string> messageCopy = possibleMessage.ToList();
                        if (rules[ruleIndex].nums.Count == 4)
                        {
                            messageCopy[i] = rules[ruleIndex].nums[0].ToString();
                            messageCopy.Insert(i + 1, rules[ruleIndex].nums[1].ToString());
                            possibleMessages.Add(messageCopy);
                            possibleMessage[i] = rules[ruleIndex].nums[2].ToString();
                            possibleMessage.Insert(i + 1, rules[ruleIndex].nums[3].ToString());
                        }
                        else if (rules[ruleIndex].nums.Count == 3)
                        {
                            messageCopy[i] = rules[ruleIndex].nums[0].ToString();
                            possibleMessages.Add(messageCopy);
                            possibleMessage[i] = rules[ruleIndex].nums[1].ToString();
                            possibleMessage.Insert(i + 1, rules[ruleIndex].nums[2].ToString());
                        }
                        else if (rules[ruleIndex].nums.Count == 5)
                        {
                            messageCopy[i] = rules[ruleIndex].nums[0].ToString();
                            messageCopy.Insert(i + 1, rules[ruleIndex].nums[1].ToString());
                            possibleMessages.Add(messageCopy);
                            possibleMessage[i] = rules[ruleIndex].nums[2].ToString();
                            possibleMessage.Insert(i + 1, rules[ruleIndex].nums[3].ToString());
                            possibleMessage.Insert(i + 2, rules[ruleIndex].nums[4].ToString());
                        }
                        else
                        {
                            messageCopy[i] = rules[ruleIndex].nums[0].ToString();
                            possibleMessages.Add(messageCopy);
                            possibleMessage[i] = rules[ruleIndex].nums[1].ToString();
                        }
                        
                        if(messageCopy.Count >= longestMessage)
                        {
                            for(int j = 0; j < messageCopy.Count; j++)
                            {
                                messageCopy[j] = "z";
                            }
                        }

                        if (possibleMessage.Count >= longestMessage)
                        {
                            for (int j = 0; j < possibleMessage.Count; j++)
                            {
                                possibleMessage[j] = "z";
                            }
                        }

                        return true;
                    }
                }
            }
            return stillMoreToProcess;
        }

        public void InitializeData()
        {
            var lines = Utilities.ReadInput("Day19_P1_2020.txt");
            bool gettingRules = true;
            foreach (var line in lines)
            {
                if (line == "")
                {
                    gettingRules = false;
                    continue;
                }
                if (gettingRules)
                {
                    Rule r;
                    int ruleIndex = int.Parse(line.Substring(0, line.IndexOf(':')));
                    string rule = line.Substring(line.IndexOf(':') + 2, line.Length - line.IndexOf(':') - 2);
                    if (rule.Contains('|'))
                    {
                        string[] digits = rule.Replace("|", string.Empty).Replace("  ", " ").Split(' ');
                        r = new Rule(digits, RuleType.orNode);
                    }
                    else if (rule.Contains('\"'))
                    {
                        r = new Rule(new string[0], RuleType.endNode, rule[1].ToString());
                    }
                    else
                    {
                        string[] digits = rule.Replace("  ", " ").Split(' ');
                        r = new Rule(digits, RuleType.singleNode);
                    }
                    rules[ruleIndex] = r;
                }
                else
                {
                    receivedMessages.Add(line);
                }
            }
            longestMessage = receivedMessages.Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur).Length;
        }

        class Rule
        {
            public RuleType ruleType;
            public List<int> nums = new List<int>();
            public string finalChar;

            public Rule(string[] digits, RuleType r, string finalChar = "")
            {
                int digit;
                for (int i = 0; i < digits.Length; i++)
                {
                    int.TryParse(digits[i], out digit);
                    nums.Add(digit);
                }
                ruleType = r;
                if (r == RuleType.endNode)
                    this.finalChar = finalChar;
            }
        }
    }
}
