using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020
{
    class Day17 : Day
    {
        public Dictionary<string, Cube> universe = new Dictionary<string, Cube>();
        Dictionary<string, Cube4d> universe4d = new Dictionary<string, Cube4d>();
        
        public void part1()
        {
            string[] lines = Utilities.ReadInput("Day17_P1_2020.txt").ToArray();

            for (int x = 0; x < lines.Count(); x++)
                for (int y = 0; y < lines.Count(); y++)
                {
                    if (lines[x][y] == '#')
                        universe[y + ":" + x + ":" + 0] = new Cube(y, x, 0, true);
                    else
                        universe[y + ":" + x + ":" + 0 ] = new Cube(y, x, 0);
                }

            for (int i = 1; i <= 6; i++)
            {

                //Get my ranges I need to check
                Dictionary<string, Cube> universeToCheck = universe.Where(t => t.Value.active == true).ToDictionary(t => t.Key, t => t.Value);

                int smallestX = universeToCheck.Min(t => t.Value.x) - 1;
                int smallestY = universeToCheck.Min(t => t.Value.y) - 1;
                int smallestZ = universeToCheck.Min(t => t.Value.z) - 1;
                int largestX = universeToCheck.Max(t => t.Value.x) + 1;
                int largestY = universeToCheck.Max(t => t.Value.y) + 1;
                int largestZ = universeToCheck.Max(t => t.Value.z) + 1;

                //Get a collection of cubes that need to be checked
                for (int x = smallestX; x <= largestX; x++)
                    for (int y = smallestY; y <= largestY; y++)
                        for (int z = smallestZ; z <= largestZ; z++)
                        {
                            Cube c;
                            if (universe.ContainsKey(x + ":" + y + ":" + z))
                                c = universe[x + ":" + y + ":" + z];
                            else
                            {
                                c = new Cube(x, y, z);
                                universe[x + ":" + y + ":" + z] = c;
                            }
                        }

                for (int x = smallestX; x <= largestX; x++)
                    for (int y = smallestY; y <= largestY; y++)
                        for (int z = smallestZ; z <= largestZ; z++)
                        {
                            Cube c = universe[x + ":" + y + ":" + z];

                            int checkNeighbors = c.CheckCountActiveNeighbors(universe);
                            if (c.active == true && checkNeighbors >= 2 && checkNeighbors <= 3)
                                c.nextRound = true;
                            else if (c.active == false && checkNeighbors == 3)
                                c.nextRound = true;
                            else
                                c.nextRound = false;
                        }

                foreach (KeyValuePair<string, Cube> c in universe)
                {
                    c.Value.active = c.Value.nextRound;
                    c.Value.nextRound = false;
                }

                Console.WriteLine($"{i}: {universe.Count(t => t.Value.active == true)}");
            }

            Console.WriteLine($"Part 1 {universe.Count(t => t.Value.active == true)}");
        }
        
        public void part2()
        {
            string[] lines = Utilities.ReadInput("Day17_P1_2020.txt").ToArray();

            for (int x = 0; x < lines.Count(); x++)
                for (int y = 0; y < lines.Count(); y++)
                {
                    if (lines[x][y] == '#')
                        universe4d[y + ":" + x + ":" + 0 + ":" + 0] = new Cube4d(y, x, 0, 0, true);
                    else
                        universe4d[y + ":" + x + ":" + 0 + ":" + 0] = new Cube4d(y, x, 0, 0);
                }

            for (int i = 1; i <= 6; i++)
            {

                //Get my ranges I need to check
                Dictionary<string, Cube4d> universeToCheck = universe4d.Where(t => t.Value.active == true).ToDictionary(t => t.Key, t => t.Value);

                int smallestX = universeToCheck.Min(t => t.Value.x) - 1;
                int smallestY = universeToCheck.Min(t => t.Value.y) - 1;
                int smallestZ = universeToCheck.Min(t => t.Value.z) - 1;
                int largestX = universeToCheck.Max(t => t.Value.x) + 1;
                int largestY = universeToCheck.Max(t => t.Value.y) + 1;
                int largestZ = universeToCheck.Max(t => t.Value.z) + 1;
                int smallestW = universeToCheck.Min(t => t.Value.w) - 1;
                int largestW = universeToCheck.Max(t => t.Value.w) + 1;

                //Get a collection of cubes that need to be checked
                for (int x = smallestX; x <= largestX; x++)
                    for (int y = smallestY; y <= largestY; y++)
                        for (int z = smallestZ; z <= largestZ; z++)
                            for (int w = smallestW; w <= largestW; w++)
                            {
                                Cube4d c;
                                if(universe4d.ContainsKey(x + ":" + y + ":" + z + ":" + w))
                                    c = universe4d[x + ":" + y + ":" + z + ":" + w];
                                else { 
                                    c = new Cube4d(x, y, z, w);
                                    universe4d[x + ":" + y + ":" + z + ":" + w] = c;
                                }
                            }

                for (int x = smallestX; x <= largestX; x++)
                    for (int y = smallestY; y <= largestY; y++)
                        for (int z = smallestZ; z <= largestZ; z++)
                            for (int w = smallestW; w <= largestW; w++)
                            {
                                Cube4d c = universe4d[x + ":" + y + ":" + z + ":" + w];

                                int checkNeighbors = c.CheckCountActiveNeighbors(universe4d);
                                if (c.active == true && checkNeighbors >= 2 && checkNeighbors <= 3)
                                    c.nextRound = true;
                                else if (c.active == false && checkNeighbors == 3)
                                    c.nextRound = true;
                                else
                                    c.nextRound = false;
                        }

                foreach (KeyValuePair<string, Cube4d> c in universe4d)
                {
                    c.Value.active = c.Value.nextRound;
                    c.Value.nextRound = false;
                }

            }
            Console.WriteLine($"Part 1 {universe4d.Count(t => t.Value.active == true)}");
        }

        public class Cube
        {
            public int x, y, z;
            public bool active = false;
            public bool nextRound = false;

            public Cube(int x, int y, int z, bool active = false)
            {
                this.x = x;
                this.y = y;
                this.z = z;
                this.active = active;
            }

            public int CheckCountActiveNeighbors(Dictionary<string, Cube> universe)
            {
                int count = 0;
                for (int x = this.x - 1; x <= this.x + 1; x++)
                    for (int y = this.y - 1; y <= this.y + 1; y++)
                        for (int z = this.z - 1; z <= this.z + 1; z++)
                        {
                            if (!(x == this.x && y == this.y && z == this.z))
                                if (universe.ContainsKey(x + ":" + y + ":" + z) && universe[x + ":" + y + ":" + z].active == true)
                                    count++;
                        }
                return count;
            }
        }

        public class Cube4d
        {
            public int x, y, z, w;
            public bool active = false;
            public bool nextRound = false;

            public Cube4d(int x, int y, int z, int w, bool active = false)
            {
                this.x = x;
                this.y = y;
                this.z = z;
                this.w = w;
                this.active = active;
            }

            public int CheckCountActiveNeighbors(Dictionary<string, Cube4d> universe)
            {
                int count = 0;
                for (int x = this.x - 1; x <= this.x + 1; x++)
                    for (int y = this.y - 1; y <= this.y + 1; y++)
                        for (int z = this.z - 1; z <= this.z + 1; z++)
                            for (int w = this.w - 1; w <= this.w + 1; w++)
                            {
                            if (!(x == this.x && y == this.y && z == this.z && w == this.w))
                                if (universe.ContainsKey(x + ":" + y + ":" + z + ":" + w) && universe[x + ":" + y + ":" + z + ":" + w].active == true)
                                    count++;
                        }
                return count;
            }

            public override string ToString()
            {
                return this.x + ":" + this.y + ":" + this.z + ":" + this.w;
            }
        }
    }
}
