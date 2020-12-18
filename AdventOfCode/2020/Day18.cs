using System;
using System.Collections.Generic;

namespace AdventOfCode._2020
{
    class Day18 : Day
    {
        Stack<Operation> operations = new Stack<Operation>();
        public void part1()
        {
            var lines = Utilities.ReadInput("Day18_P1_2020.txt");

            long total = 0;
            foreach (string line in lines)
            {
                operations.Push(new Operation());

                foreach (char c in line.Replace(" ", string.Empty))
                {
                    switch (c)
                    {
                        case '+':
                            operations.Peek().operators.Add('+');
                            break;
                        case '*':
                            operations.Peek().operators.Add('*');
                            break;
                        case '(':
                            operations.Push(new Operation());
                            break;
                        case ')':
                            long final = operations.Peek().calculate();
                            operations.Pop();
                            operations.Peek().numbers.Add(final);
                            if (operations.Peek().operators.Count == 0)
                                operations.Peek().operators.Add('+');
                            break;
                        default:
                            operations.Peek().numbers.Add(int.Parse(c.ToString()));
                            if (operations.Peek().operators.Count == 0)
                                operations.Peek().operators.Add('+');
                            break;
                    }
                }

                total += operations.Pop().calculate();
            }

            Console.WriteLine($"Part 1: {total}");
        }

        public void part2()
        {
            operations = null;
            operations = new Stack<Operation>();
            var lines = Utilities.ReadInput("Day18_P1_2020.txt");

            long total = 0;
            foreach (string line in lines)
            {
                operations.Push(new Operation());

                foreach (char c in line.Replace(" ", string.Empty))
                {
                    switch (c)
                    {
                        case '+':
                            operations.Peek().operators.Add('+');
                            break;
                        case '*':
                            operations.Peek().operators.Add('*');
                            break;
                        case '(':
                            operations.Push(new Operation());
                            break;
                        case ')':
                            long final = operations.Peek().calculateNotReallyRealMath();
                            operations.Pop();
                            operations.Peek().numbers.Add(final);
                            if (operations.Peek().operators.Count == 0)
                                operations.Peek().operators.Add('+');
                            break;
                        default:
                            operations.Peek().numbers.Add(int.Parse(c.ToString()));
                            if (operations.Peek().operators.Count == 0)
                                operations.Peek().operators.Add('+');
                            break;
                    }
                }

                total += operations.Pop().calculateNotReallyRealMath();
            }

            Console.WriteLine($"Part 2: {total}");
        }

        class Operation
        {
            public List<long> numbers = new List<long>();
            public List<char> operators = new List<char>();

            public long calculate()
            {
                long total = 0;

                for(int i = 0; i < numbers.Count; i++)
                {
                    if (operators[i] == '+')
                        total += numbers[i];
                    else
                        total *= numbers[i];
                }

                return total;
            }

            public long calculateNotReallyRealMath()
            {
                long total = 1;
                long additionResult = 0;
                List<long> additionsFirstResult = new List<long>();
                for (int i = 0; i < numbers.Count; i++)
                {
                    if (operators[i] == '+')
                        additionResult += numbers[i];
                    else { 
                        additionsFirstResult.Add(additionResult);
                        additionResult = 0;
                        additionResult += numbers[i];
                    }
                }

                additionsFirstResult.Add(additionResult);
                foreach (long i in additionsFirstResult)
                    total *= i;

                return total;
            }
        }
    }
}
