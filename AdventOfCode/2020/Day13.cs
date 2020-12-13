using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    class Day13 : Day
    {
        
        public void part1()
        {
            string[] lines = Utilities.ReadInput("Day13_P1_2020.txt").ToArray();
            int earliestCanLeave = int.Parse(lines[0]);
            int closestMin = 999;
            int closestRoute = 0;
            List<int> busRoutes = busRoutes = lines[1].Split(',').Where(t => t != "x").ToList().Select(g => Convert.ToInt32(g)).ToList();
            int checkClose;
            foreach(int route in busRoutes)
            {
                checkClose = ((earliestCanLeave / route) * route + route) - earliestCanLeave;
                if (checkClose < closestMin) { 
                    closestMin = checkClose;
                    closestRoute = route;
                }
            }

            Console.WriteLine($"Part 1: {closestRoute * closestMin}");
        }

        public void part2()
        {
            string[] line = Utilities.ReadInput("Day13_P1_2020.txt").ToArray()[1].Split(',');
            List<int> busRoutes = new List<int>();
            List<int> mods = new List<int>();
            int modCount = 1;
            foreach(string s in line)
            {
                int x;
                if (int.TryParse(s, out x)) {
                    if(busRoutes.Count > 0)
                        mods.Add(modCount);
                    busRoutes.Add(x);
                    modCount = 1;
                }
                else
                    modCount++;
            }

            long num1 = busRoutes[0];
            long num2 = busRoutes[1];
            int currentMod = 0;
            
            Tuple<long, long> lcm = findLCM(num1, num2, mods[0], num1);

            long currentMultiplier = num1 * num2;
            num1 = lcm.Item2;

            for(int i = 2; i < busRoutes.Count; i++)
            {
                currentMod++;
                num2 = busRoutes[i];
                lcm = findLCM(num1, num2, mods[currentMod], currentMultiplier);
                currentMultiplier = currentMultiplier * num2;
                num1 = lcm.Item2;
            }

            Console.WriteLine($"Part 2: {num1 - mods.Sum(t => t)}");
        }

        public Tuple<long, long> findLCM(long num1, long num2, long mod, long additive)
        {
            long result1 = num1;
            long result2 = num2;
            while(result2 - result1 != mod) {
                if (result1 < result2)
                    result1 += additive;
                else
                    result2 += num2;
            }
            return Tuple.Create(result1, result2);
        }
    }
}
