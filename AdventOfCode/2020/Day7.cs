using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    class Day7 : Day
    {
        List<Bag> bags = new List<Bag>();
        Bag bagToCheck;
        public void part1()
        {
            Initialize();
            HashSet<Bag> totalBagColors = new HashSet<Bag>();
            bagToCheck = bags.Where(t => t.name == "shiny gold").Single();

            List<Bag> parentsToCheck = new List<Bag>();
            parentsToCheck.Add(bagToCheck);

            while (parentsToCheck.Count > 0)
            {
                foreach(Bag b in parentsToCheck.ToList())
                {
                    foreach (Bag parent in b.parents)
                    {
                        totalBagColors.Add(parent);
                        if (!parentsToCheck.Contains(parent))
                            parentsToCheck.Add(parent);
                    }
                    parentsToCheck.Remove(b);
                }
            }

            
            Console.WriteLine($"Part 1: {totalBagColors.Count}");
        }

        public void part2()
        {
            int totalIndividualBags = 0;
            List<Child_With_Multiplier> childrenToCheck = new List<Child_With_Multiplier>();

            childrenToCheck.Add(new Child_With_Multiplier(new KeyValuePair<Bag, int>(bagToCheck, 1), 1));

            while (childrenToCheck.Count > 0) { 
                foreach(var child in childrenToCheck.ToList())
                {
                    totalIndividualBags += child.child.Key.children.Sum(t => t.Value) * child.multiplier;
                    foreach (var c in child.child.Key.children)
                    {
                        childrenToCheck.Add(new Child_With_Multiplier(c, c.Value * child.multiplier));
                    }

                    childrenToCheck.Remove(child);
                }
            }

            Console.WriteLine($"Part 2 {totalIndividualBags}");
        }
        
        public void Initialize()
        {
            var lines = Utilities.ReadInput("Day7_P1_2020.txt");
            foreach(string line in lines)
            {
                string bagName = line.Substring(0, line.IndexOf("bags") - 1);
                Bag b = bags.Where(t => t.name == bagName).SingleOrDefault();
                
                if(b == null)
                {
                    b = new Bag(bagName);
                    bags.Add(b);
                }
                
                string[] children = line.Substring(line.IndexOf("contain") + 8, (line.Length - (line.IndexOf("contain") + 8))).Split(',');
                
                foreach(string child in children)
                {
                    string name = child.Substring(2, child.IndexOf("bag") - 3).Trim();
                    int quantity;

                    if(int.TryParse(child.Trim()[0].ToString(), out quantity)){ 
                        Bag childB = bags.Where(t => t.name == name).SingleOrDefault();

                        if (childB == null)
                        {
                            childB = new Bag(name);
                            bags.Add(childB);
                        }

                        b.children.Add(childB, quantity);
                        childB.parents.Add(b);
                    }
                }
            }
        }

        public class Bag
        {
            public string name;
            public List<Bag> parents = new List<Bag>();
            public Dictionary<Bag, int> children = new Dictionary<Bag, int>();

            public Bag(string name)
            {
                this.name = name;
            }
        }

        public class Child_With_Multiplier
        {
            public KeyValuePair<Bag, int> child;
            public int multiplier;

            public Child_With_Multiplier(KeyValuePair<Bag, int> child, int multiplier)
            {
                this.child = child;
                this.multiplier = multiplier;
            }
        }
    }
}
