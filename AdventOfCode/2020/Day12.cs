using System;
using System.Collections.Generic;

namespace AdventOfCode._2020
{
    class Day12 : Day
    {
        List<Command> commands = new List<Command>();
        
        public void part1()
        {
            var lines = Utilities.ReadInput("Day12_P1_2020.txt");
            Ship ship = new Ship();
            foreach (string line in lines)
            {
                commands.Add(new Command(line[0], int.Parse(line.Substring(1, line.Length - 1))));
            }

            foreach(Command c in commands)
            {
                switch (c.action)
                {
                    case 'N':
                        ship.NorthSouthValue += c.value;
                        break;
                    case 'E':
                        ship.EastWestValue += c.value;
                        break;
                    case 'S':
                        ship.NorthSouthValue -= c.value;
                        break;
                    case 'W':
                        ship.EastWestValue -= c.value;
                        break;
                    case 'F':
                        ship.MoveDirection(c.value);
                        break;
                    case 'R':
                        ship.RotateShip(true, c.value);
                        break;
                    case 'L':
                        ship.RotateShip(false, c.value);
                        break;
                }
            }

            Console.WriteLine($"Part 1: {Math.Abs(ship.NorthSouthValue) + Math.Abs(ship.EastWestValue)}");
        }

        public void part2()
        {
            Ship ship = new Ship();
            Waypoint waypoint = new Waypoint();

            foreach (Command c in commands)
            {
                switch (c.action)
                {
                    case 'N':
                        waypoint.NorthSouthValue += c.value;
                        break;
                    case 'E':
                        waypoint.EastWestValue += c.value;
                        break;
                    case 'S':
                        waypoint.NorthSouthValue -= c.value;
                        break;
                    case 'W':
                        waypoint.EastWestValue -= c.value;
                        break;
                    case 'F':
                        ship.MoveDirection(c.value, true, waypoint);
                        break;
                    case 'R':
                        if(c.value == 270)
                            waypoint.RotateWaypoint(false, 90);
                        else
                            waypoint.RotateWaypoint(true, c.value);
                        break;
                    case 'L':
                        if (c.value == 270)
                            waypoint.RotateWaypoint(true, 90);
                        else
                            waypoint.RotateWaypoint(false, c.value);
                        break;
                }
            }

            Console.WriteLine($"Part 2: {Math.Abs(ship.NorthSouthValue) + Math.Abs(ship.EastWestValue)}");
        }

        class Waypoint
        {
            public int NorthSouthValue = 1;
            public int EastWestValue = 10;

            public void RotateWaypoint(bool rightTurn, int value)
            {
                int temp;
                if (rightTurn)
                {
                    if(value == 90) { 
                        temp = EastWestValue;
                        EastWestValue = NorthSouthValue;
                        NorthSouthValue = temp * -1;
                    }
                    if (value == 180) { 
                        NorthSouthValue *= -1;
                        EastWestValue *= -1;
                    }
                }
                else
                {
                    if (value == 90)
                    {
                        temp = EastWestValue;
                        EastWestValue = NorthSouthValue * -1;
                        NorthSouthValue = temp;
                    }
                    if (value == 180)
                    {
                        NorthSouthValue *= -1;
                        EastWestValue *= -1;
                    }
                }
            }
        }

        class Ship
        {
            public int NorthSouthValue = 0;
            public int EastWestValue = 0;

            public Direction directionFacing = Direction.East;

            public void MoveDirection(int value, bool part2 = false, Waypoint w = null)
            {
                if (part2) {
                    NorthSouthValue += value * w.NorthSouthValue;
                    EastWestValue += value * w.EastWestValue;
                }
                else { 
                    switch (directionFacing)
                    {
                        case Direction.East:
                            EastWestValue += value;
                            break;
                        case Direction.South:
                            NorthSouthValue -= value;
                            break;
                        case Direction.West:
                            EastWestValue -= value;
                            break;
                        case Direction.North:
                            NorthSouthValue += value;
                            break;
                    }
                }
            }

            public void RotateShip(bool rightTurn, int value)
            {
                int changeNum = value / 90;
                int enumChange;
                if (rightTurn) { 
                    enumChange = (int)directionFacing + changeNum;
                    if(enumChange > 3)
                    {
                        enumChange -= 4;
                    }
                }
                else
                {
                    enumChange = (int)directionFacing - changeNum;
                    if (enumChange < 0)
                    {
                        enumChange += 4;
                    }
                }

                directionFacing = (Direction)enumChange;
            }
        }

        class Command
        {
            public char action;
            public int value;

            public Command(char action, int value)
            {
                this.action = action;
                this.value = value;
            }
        }

       
        enum Direction { East, South, West, North }
    }
}
