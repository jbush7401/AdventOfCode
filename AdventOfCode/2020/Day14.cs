using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    class Day14 : Day
    {
        Dictionary<long, Memory> memories = new Dictionary<long, Memory>();

        public void part1()
        {
            string currentMask = "";
            
            var lines = Utilities.ReadInput("Day14_P1_2020.txt");

            foreach(string line in lines)
            {

                if (line.Substring(0, 3) == "mas")
                    currentMask = line.Substring(7, line.Length - 7);
                else
                {
                    int index = int.Parse(line.Substring(line.IndexOf('[') + 1, line.IndexOf(']') - (line.IndexOf('[') + 1)));
                    int v = int.Parse(line.Substring(line.IndexOf('=') + 2, line.Length - (line.IndexOf('=') + 2)));

                    if (!memories.ContainsKey(index))
                        memories[index] = new Memory();

                    memories[index].StoreDecimal(v, currentMask);
                        
                }
            }
            long sum = 0;

            foreach(KeyValuePair<long, Memory> m in memories)
            {
                sum += m.Value.ReturnDecimal();
            }

            Console.WriteLine($"Part 1: {sum}");
        }

        public void part2()
        {
            string currentMask = "";
            memories.Clear();

            var lines = Utilities.ReadInput("Day14_P1_2020.txt");

            foreach (string line in lines)
            {

                if (line.Substring(0, 3) == "mas")
                    currentMask = line.Substring(7, line.Length - 7);
                else
                {
                    int index = int.Parse(line.Substring(line.IndexOf('[') + 1, line.IndexOf(']') - (line.IndexOf('[') + 1)));
                    int v = int.Parse(line.Substring(line.IndexOf('=') + 2, line.Length - (line.IndexOf('=') + 2)));

                    ProcessLinePart2(v, currentMask, index);

                }
            }
            long sum = 0;

            foreach (KeyValuePair<long, Memory> m in memories)
            {
                sum += m.Value.ReturnDecimal();
            }

            Console.WriteLine($"Part 2: {sum}");
        }

        public void ProcessLinePart2(int dec, string mask, int index)
        {
            mask = Reverse(mask);
            string binary = Reverse(Convert.ToString(dec, 2));
            string indexBin = Reverse(Convert.ToString(index, 2).PadLeft(36, '0'));
            List<int> floatPositions = new List<int>();
            List<string> floats = new List<string>();
            Memory mToStore = new Memory();

            //Store Value
            for (int x = 0; x < 36; x++)
            {
                if (x < binary.Length && (binary[x] == '1' || binary[x] == '0'))
                    mToStore.mem[pow(x)] = binary[x] - 48;
                else
                    mToStore.mem[pow(x)] = 0;
            }

            //Get float replacements
            for (int x = 0; x < 36; x++)
            {
                if (mask[x] == 'X')
                {
                    floatPositions.Add(x);
                }
            }

            for (int i = 0; i < pow(floatPositions.Count); i++)
            {
                floats.Add(Convert.ToString(i, 2).PadLeft(floatPositions.Count, '0'));
            }


            //Get final memory values
            foreach(string f in floats){
                Memory indexToSet = new Memory();
                long iToSetCheck = 0;
                StringBuilder s = new StringBuilder(indexBin);
                for (int i = 0; i < floatPositions.Count; i++)
                {
                    s[floatPositions[i]] = f[i];
                }
                indexBin = s.ToString();
                indexToSet.StoreBinaryEasy(indexBin, mask);
                iToSetCheck = indexToSet.ReturnDecimal();
                memories[iToSetCheck] = mToStore;
            }
        }

        class Memory
        {
            public Dictionary<long, int> mem = new Dictionary<long, int>();

            public long ReturnDecimal()
            {
                return mem.Where(t => t.Value == 1).Sum(t => t.Key);
            }

            public void StoreDecimal(int dec, string mask)
            {
                mask = Reverse(mask);
                string binary = Reverse(Convert.ToString(dec, 2));
                for(int x = 0; x < 36; x++)
                {
                    if (mask[x] == '1' || mask[x] == '0')
                        mem[pow(x)] = mask[x] - 48;
                    else if (x < binary.Length && (binary[x] == '1' || binary[x] == '0'))
                        mem[pow(x)] = binary[x] - 48;
                    else
                        mem[pow(x)] = 0;
                }
            }

            public void StoreBinaryEasy(string bin, string mask)
            {
                for (int x = 0; x < 36; x++)
                {
                    if (mask[x] == '1')
                        mem[pow(x)] = 1;
                    else if (x < bin.Length && (bin[x] == '1' || bin[x] == '0'))
                        mem[pow(x)] = bin[x] - 48;
                }
            }
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static long pow(int exponent)
        {
            long power = 1;
            if (exponent == 0)
                return 1;
            for (int i = 1; i <= exponent; i++)
                power *= 2;
            return power;
        }
    }
}
