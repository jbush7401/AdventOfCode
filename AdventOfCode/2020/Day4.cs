using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020
{
    class Day4 : Day
    {
        int totalValidPassportsPart1 = 0;
        int totalValidPassportsPart2 = 0;
        PassportCheck passport = new PassportCheck();
        
        public void part1()
        {
            var lines = Utilities.ReadInput("Day4_P1_2020.txt");

            int currentIndex = 0;
            bool getValue = false;
            string fieldType = "";
            string value = "";
            
            foreach (string line in lines)
            {
                foreach (char c in line)
                {
                    if (c == ':')
                    {
                        fieldType = line.Substring(currentIndex - 3, 3);
                        getValue = true;
                    }
                    else if (c == ' ')
                    {
                        passport.addField(fieldType, value);
                        value = "";
                        getValue = false;
                    }
                    else if (getValue)
                        value += c;

                    currentIndex++;
                }

                if (line.Length == 0)
                {
                    CheckPassport();
                }
                else
                {
                    passport.addField(fieldType, value);
                    value = "";
                    getValue = false;
                }

                currentIndex = 0;
            }
            CheckPassport();

            Console.WriteLine($"Part 1: {totalValidPassportsPart1}");
        }

        void CheckPassport()
        {
            if (passport.isValidPart1()) { 
                totalValidPassportsPart1++;
                if (passport.isValidPart2())
                    totalValidPassportsPart2++;
            }
            
            passport = null;
            passport = new PassportCheck();
        }

        public void part2()
        {
            Console.WriteLine($"Part 2: {totalValidPassportsPart2}");
        }

        public class Field
        {
            public string value;
            public bool isValid = false;
        }

        public class PassportCheck
        {
            Dictionary<string, Field> requiredFields = new Dictionary<string, Field>() { { "byr", new Field() }, { "iyr", new Field() }, { "eyr", new Field() }, { "hgt", new Field() }, { "hcl", new Field() }, { "ecl", new Field() }, { "pid", new Field() } };
            string validhcl = "0123456789abcdef";
            string[] validecl = new string[7] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

            public void addField(string field, string value)
            {
                if (field == "cid")
                    return;
                requiredFields[field].isValid = true;
                requiredFields[field].value = value;
            }

            public bool isValidPart1()
            {
                foreach(KeyValuePair<string, Field> field in requiredFields)
                {
                    if(field.Value.isValid == false) 
                        return false;
                }
                return true;
            }

            public bool isValidPart2()
            {
                int num;
                string check;

                if (int.Parse(requiredFields["byr"].value) < 1920 || int.Parse(requiredFields["byr"].value) > 2002)
                    return false;

                if (int.Parse(requiredFields["iyr"].value) < 2010 || int.Parse(requiredFields["iyr"].value) > 2020)
                    return false;

                if (int.Parse(requiredFields["eyr"].value) < 2020 || int.Parse(requiredFields["eyr"].value) > 2030 || requiredFields["eyr"].value.Length != 4)
                    return false;

                check = requiredFields["hgt"].value.Substring(requiredFields["hgt"].value.Length - 2, 2);

                if (!int.TryParse(requiredFields["hgt"].value.Substring(0, requiredFields["hgt"].value.Length - 2), out num))
                    return false;

                if ((check == "cm" && num < 150) || (check == "cm" && num > 193) || (check == "in" && num < 59) || (check == "in" && num > 76))
                    return false;

                if (requiredFields["hcl"].value[0] != '#' || requiredFields["hcl"].value.Length != 7)
                    return false;
                for (int i = 1; i <= 6; i++)
                {
                    if (validhcl.IndexOf(requiredFields["hcl"].value[i]) == -1)
                        return false;
                }

                if (!validecl.Contains(requiredFields["ecl"].value))
                    return false;

                if (requiredFields["pid"].value.Length != 9 || !int.TryParse(requiredFields["pid"].value, out num))
                    return false;

                return true;
            }
        }
    }
}
