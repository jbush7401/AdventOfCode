using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AdventOfCode
{
    class AdventOfCode
    {
        public static void Main()
        {
            string YearDayToRun;
            
            Console.Title = "Advent Of Code";
            Console.WindowHeight = 45;
            Console.WindowWidth = 100;
            Console.SetBufferSize(100, 45);
            Console.CursorVisible = true;
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("Enter year, space, day to run...");
            YearDayToRun = Console.ReadLine();
            string[] parts = YearDayToRun.Split(' ');
            string year = parts[0];
            string day = parts[1];
            string  methodToCall = "";
            if(parts[2] != null)
                methodToCall = parts[2];

            Runner runner = new Runner();

            if (methodToCall == "p")
                runner.runDayProfiled(year, day);
            else if (methodToCall == "y")
                runner.runYearProfiled(year, int.Parse(day));
            else
                runner.runDay(year, day);

           
        }
    }
}
