using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020
{
    class Day8 : Day
    {
        public List<Command> commands = new List<Command>();
        bool infiniteLoop = false;
        int index = 0;
        int accumulator = 0;
        int stepNum = 1;

        public void part1()
        {
            var lines = Utilities.ReadInput("Day8_P1_2020.txt");
            foreach (string line in lines)
            {
                commands.Add(new Command((Direction)Enum.Parse(typeof(Direction), line.Substring(0, 3)), int.Parse(line.Substring(4, line.Length - 4))));
            }

            while (!infiniteLoop)
            {
                ProcessCommand(commands[index]);
                if (commands[index].step > -1)
                    infiniteLoop = true;
            }

            Console.WriteLine($"Part 1: {accumulator}");
        }

        public void part2()
        {
            for(int change = 0; change < commands.Count; change++)
            {
                index = 0;
                accumulator = 0;
                stepNum = 1;
                infiniteLoop = false;

                foreach(Command c in commands)
                {
                    c.step = -1;
                }

                if(commands[change].direction == Direction.jmp || commands[change].direction == Direction.nop) { 
                    if (commands[change].direction == Direction.jmp)
                        commands[change].direction = Direction.nop;
                    else
                        commands[change].direction = Direction.jmp;
                

                    while (!infiniteLoop && index < commands.Count)
                    {
                        if (index == commands.Count - 1)
                        {
                            ProcessCommand(commands[index]);
                            Console.WriteLine($"Part 2: {accumulator}");
                            break;
                        }
                        ProcessCommand(commands[index]);
                       
                        if (index < commands.Count && commands[index].step > -1)
                            infiniteLoop = true;
                    }


                    if (commands[change].direction == Direction.jmp)
                        commands[change].direction = Direction.nop;
                    else
                        commands[change].direction = Direction.jmp;
                }
            }
        }


        void ProcessCommand(Command command, bool switcheroo = false)
        {
           
            switch (command.direction)
            {
                case Direction.acc:
                    accumulator += command.amount;
                    command.step = stepNum;
                    index++;
                    break;
                case Direction.nop:
                    command.step = stepNum;
                    index++;
                    break;
                case Direction.jmp:
                    command.step = stepNum;
                    index = command.Jump(index, commands.Count);
                    break;
            }
            stepNum++;
        }

        public class Command
        {
            public Direction direction;
            public int amount;
            public int step = -1;

            public Command(Direction direction, int amount)
            {
                this.direction = direction;
                this.amount = amount;
            }

            public int Jump(int index, int max)
            {
                if (index + amount > max-1)
                {
                    return (index + amount) % max;
                }
                if (index + amount < 0)
                {
                    return max + index - (Math.Abs(amount) % max);
                }
                return index += amount;
            }
        }

        public enum Direction { acc, nop, jmp }
    }
}
