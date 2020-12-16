using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    class Day16 : Day
    {
        public Ticket myTicket;
        public List<Ticket> nearbyTickets = new List<Ticket>();
        public List<Field> fields = new List<Field>();
        public void part1()
        {
            Initialize();
            
            int sum = 0;
            foreach (Ticket nearby in nearbyTickets.ToList())
            {
                foreach (int value in nearby.values.ToList())
                {
                    if (!isValidValue(value)) { 
                        sum += value;
                        nearbyTickets.Remove(nearby);
                    }
                }
            }

            Console.WriteLine($"Part 1: {sum}");
        }

        public void part2()
        {
            List<Field> possibleFields = fields.Where(t => t.orderIndex == -1).ToList();
            while(possibleFields.Count > 0) { 
                for (int currentPlaceCheck = 0; currentPlaceCheck < myTicket.values.Count; currentPlaceCheck++)
                {
                    foreach (Field f in fields)
                    {
                        foreach (Ticket nearby in nearbyTickets)
                        {
                            if (!f.ranges.NumberInRange(nearby.values[currentPlaceCheck]))
                            {
                                possibleFields.Remove(f);
                                break;
                            }
                        }
                    }
                    if(possibleFields.Count == 1)
                        possibleFields[0].orderIndex = currentPlaceCheck;
                    
                    possibleFields = fields.Where(t => t.orderIndex == -1).ToList();
                }
            }

            long total = 1;

            var departureFields = fields.Where(t => t.name.Contains("departure"));

            foreach (var f in departureFields)
                total *= myTicket.values[f.orderIndex];

            Console.WriteLine($"Part 2: {total}");
        }

        public bool isValidValue(int value)
        {
            foreach (Field f in fields)
            {
                if (f.ranges.NumberInRange(value))
                    return true;
            }
            return false;
        }

        public void Initialize()
        {
            var lines = Utilities.ReadInput("Day16_P1_2020.txt");
            
            bool yourTicketNext = false;
            bool nearbyTicketNext = false;

            foreach(string line in lines)
            {
                if ((line.Length >= 4) && line.Substring(0, 4) == "your") { 
                    yourTicketNext = true;
                }
                else if ((line.Length >= 6) && line.Substring(0, 6) == "nearby")
                {
                    nearbyTicketNext = true;
                }
                else if (yourTicketNext)
                {
                    myTicket = new Ticket(line.Split(',').Select(t => int.Parse(t)).ToList());
                    yourTicketNext = false;
                }
                else if (nearbyTicketNext)
                {
                    nearbyTickets.Add(new Ticket(line.Split(',').Select(t => int.Parse(t)).ToList()));
                }
                else
                {
                    if(line.Length > 0) { 
                        var result = Regex.Matches(line, @"\d+");
                        fields.Add(new Field(line.Substring(0, line.IndexOf(':')), new Ranges(int.Parse(result[0].Value), int.Parse(result[1].Value), int.Parse(result[2].Value), int.Parse(result[3].Value))));
                    }
                }
            }
        }

        public class Ticket
        {
            public List<int> values;

            public Ticket(List<int> values)
            {
                this.values = values;
            }
        }

        public class Field
        {
            public string name;
            public Ranges ranges;
            public int orderIndex = -1;
            public Field(string name, Ranges ranges)
            {
                this.name = name;
                this.ranges = ranges;
            }
        }

        public class Ranges
        {
            int Range1Min;
            int Range1Max;
            int Range2Min;
            int Range2Max;

            public Ranges(int range1Min, int range1Max, int range2Min, int range2Max)
            {
                Range1Min = range1Min;
                Range1Max = range1Max;
                Range2Min = range2Min;
                Range2Max = range2Max;
            }

            public bool NumberInRange(int num)
            {
                return (num >= Range1Min && num <= Range1Max) || (num >= Range2Min && num <= Range2Max);
            }
        }
    }
}
