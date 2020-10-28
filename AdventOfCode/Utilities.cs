using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Utilities
    {
        public static IEnumerable<string> ReadInput(string fileName)
        {
            string resourceName = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Input\\" + fileName;

            var lines = File.ReadLines(resourceName);

            return lines;
        }

        public static string ReadInputDelimited(string fileName)
        {
            string resourceName = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Input\\" + fileName;

            var lines = File.ReadAllText(resourceName);

            return lines;
        }
    }
}
