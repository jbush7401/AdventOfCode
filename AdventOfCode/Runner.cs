using System;
using System.Diagnostics;
using System.Threading;

namespace AdventOfCode
{
    class Runner
    {
        public void runDay(string year, string day) {
            Day dayToRun = null;
            string classToRun = "AdventOfCode._" + year + ".Day" + day;
            Type type = Type.GetType(classToRun, true);
            dayToRun = (Day)Activator.CreateInstance(type);
            
            Stopwatch stopWatch = new Stopwatch();

            Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(2);
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;

            stopWatch.Reset();

            stopWatch.Start();

            dayToRun.part1();
            
            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            double elapsedTime = ts.TotalMilliseconds;

            Console.WriteLine(elapsedTime + " ms");

            stopWatch.Reset();

            stopWatch.Start();

            dayToRun.part2();

            stopWatch.Stop();

            ts = stopWatch.Elapsed;
            elapsedTime = ts.TotalMilliseconds;

            Console.WriteLine(elapsedTime + " ms");
            Console.ReadLine();
        }
        public void runDayProfiled(string year, string day)
        {
            Day dayToRun = null;
            string classToRun = "AdventOfCode._" + year + ".Day" + day;
            Type type = Type.GetType(classToRun, true);
            dayToRun = (Day)Activator.CreateInstance(type);

            Stopwatch stopWatch = new Stopwatch();

            Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(2);
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;

            double part1ElapsedTime = 0;
            double part2ElapsedTime = 0;

            for (int i = 1; i <= 10; i++)
            {
                stopWatch.Reset();

                stopWatch.Start();

                dayToRun.part1();

                stopWatch.Stop();

                TimeSpan ts = stopWatch.Elapsed;
                part1ElapsedTime += ts.TotalMilliseconds;

                stopWatch.Reset();

                stopWatch.Start();

                dayToRun.part2();

                stopWatch.Stop();

                ts = stopWatch.Elapsed;
                part2ElapsedTime += ts.TotalMilliseconds;
            }

            Console.WriteLine("Part 1: " + part1ElapsedTime/10 + " ms");
            Console.WriteLine("Part 2: " + part2ElapsedTime/10 + " ms");
            Console.ReadLine();
        }
    }

   
}
