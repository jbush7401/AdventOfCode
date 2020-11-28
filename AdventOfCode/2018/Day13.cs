using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018
{
    class Day13 : Day
    {

        Tile[,] grid = new Tile[150, 150];
        List<Cart> carts = new List<Cart>();
        int crashesFound = 0;
        public void part1()
        {
            Initialize();

            while (carts.Count > 1)
            {
                foreach(Cart c in carts.OrderBy(t => t.locY).ThenBy(t => t.locX))
                {
                    HandleCartMove(c);
                }
            }

            Console.WriteLine($"Part 2: {carts[0].locX}, {carts[0].locY}");
        }

        public void part2()
        {
            //Unnessary
        }

        void HandleCartMove(Cart c)
        {
            switch (grid[c.locX, c.locY].tileType)
            {
                case '-':
                    if (c.directionFacing == DirectionFacing.East)
                        c.locX++;
                    else
                        c.locX--;
                    break;
                case '|':
                    if (c.directionFacing == DirectionFacing.North)
                        c.locY--;
                    else
                        c.locY++;
                    break;
                case '/':
                    if (c.directionFacing == DirectionFacing.North) { 
                        c.locX++;
                        c.directionFacing = DirectionFacing.East;
                    }
                    else if (c.directionFacing == DirectionFacing.South)
                    {
                        c.locX--;
                        c.directionFacing = DirectionFacing.West;
                    }
                    else if (c.directionFacing == DirectionFacing.East)
                    {
                        c.locY--;
                        c.directionFacing = DirectionFacing.North;
                    }
                    else {
                        c.locY++;
                        c.directionFacing = DirectionFacing.South;
                    }
                    break;
                case '\\':
                    if (c.directionFacing == DirectionFacing.North)
                    {
                        c.locX--;
                        c.directionFacing = DirectionFacing.West;
                    }
                    else if (c.directionFacing == DirectionFacing.South)
                    {
                        c.locX++;
                        c.directionFacing = DirectionFacing.East;
                    }
                    else if (c.directionFacing == DirectionFacing.East)
                    {
                        c.locY++;
                        c.directionFacing = DirectionFacing.South;
                    }
                    else
                    {
                        c.locY--;
                        c.directionFacing = DirectionFacing.North;
                    }
                    break;
                case '+':
                    c.HandleIntersection();
                    break;
                default:
                    throw new Exception("Invalid Tile Type " + grid[c.locX, c.locY].tileType);
            }

            if (carts.GroupBy(t => new {t.locX, t.locY}).Where(g => g.Count() > 1).Count() > 0) { 
                crashesFound++;
                if (crashesFound == 1)
                {
                    Console.WriteLine($"Part 1: {c.locX}, {c.locY}");
                }
                carts.RemoveAll(t => t.locX == c.locX && t.locY == c.locY);
            }
        }

        void Initialize()
        {
            var lines = Utilities.ReadInput("Day13_P1_2018.txt");

            int xcoord = 0;
            int ycoord = 0;
            foreach (var line in lines)
            {
                foreach(char c in line)
                {
                    switch (c) {
                        case '<':
                            carts.Add(new Cart(DirectionFacing.West, xcoord, ycoord));
                            grid[xcoord, ycoord] = new Tile(xcoord, ycoord, '-');
                            break;
                        case '^':
                            carts.Add(new Cart(DirectionFacing.North, xcoord, ycoord));
                            grid[xcoord, ycoord] = new Tile(xcoord, ycoord, '|');
                            break;
                        case '>':
                            carts.Add(new Cart(DirectionFacing.East, xcoord, ycoord));
                            grid[xcoord, ycoord] = new Tile(xcoord, ycoord, '-');
                            break;
                        case 'v':
                            carts.Add(new Cart(DirectionFacing.South, xcoord, ycoord));
                            grid[xcoord, ycoord] = new Tile(xcoord, ycoord, '|');
                            break;
                        default:
                            grid[xcoord, ycoord] = new Tile(xcoord, ycoord, c);
                            break;
                    }
                    xcoord++;
                }
                xcoord = 0;
                ycoord++;
            }
        }

        public enum DirectionOptions { Left, Straight, Right }

        public enum DirectionFacing { North, East, South, West }

        public class Cart
        {
            public DirectionOptions nextDirection = DirectionOptions.Left;

            public DirectionFacing directionFacing;

            public int locX;
            public int locY;

            public void HandleIntersection() {
                if(nextDirection == DirectionOptions.Left)
                {
                    if ((int)directionFacing > 0) { 
                        directionFacing--;
                    }
                    else
                        directionFacing = DirectionFacing.West;
                }
                if (nextDirection == DirectionOptions.Right)
                {
                    if ((int)directionFacing < 3)
                        directionFacing++;
                    else
                        directionFacing = DirectionFacing.North;
                }
                MoveInDirection();
                if (nextDirection == DirectionOptions.Right)
                    nextDirection = DirectionOptions.Left;
                else
                    nextDirection++;
            } 

            public void MoveInDirection()
            {
                switch (directionFacing)
                {
                    case DirectionFacing.North:
                        locY--;
                        break;
                    case DirectionFacing.South:
                        locY++;
                        break;
                    case DirectionFacing.West:
                        locX--;
                        break;
                    case DirectionFacing.East:
                        locX++;
                        break;
                }
            }

            public Cart(DirectionFacing directionFacing, int locX, int locY)
            {
                this.directionFacing = directionFacing;
                this.locX = locX;
                this.locY = locY;
            }
        }

        public class Tile
        {
            int xCoord;
            int yCoord;

            public char tileType;

            public Tile(int xCoord, int yCoord, char tileType)
            {
                this.xCoord = xCoord;
                this.yCoord = yCoord;
                this.tileType = tileType;
            }
        }

    }
}
