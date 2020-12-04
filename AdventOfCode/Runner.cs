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
            Stopwatch stopWatch = new Stopwatch();

            Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(2);
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;

            double part1ElapsedTime = 0;
            double part2ElapsedTime = 0;

            for (int i = 1; i <= 50; i++)
            {
                Day dayToRun = null;
                string classToRun = "AdventOfCode._" + year + ".Day" + day;
                Type type = Type.GetType(classToRun, true);
                dayToRun = (Day)Activator.CreateInstance(type);
                stopWatch.Reset();

                stopWatch.Start();

                dayToRun.part1();

                stopWatch.Stop();

                TimeSpan ts = stopWatch.Elapsed;
                Console.WriteLine("Part 1: " + ts.TotalMilliseconds + " ms");

                part1ElapsedTime += ts.TotalMilliseconds;

                stopWatch.Reset();

                stopWatch.Start();

                dayToRun.part2();

                stopWatch.Stop();

                ts = stopWatch.Elapsed;
                Console.WriteLine("Part 2: " + ts.TotalMilliseconds + " ms");
                part2ElapsedTime += ts.TotalMilliseconds;
            }

            Console.WriteLine("Part 1: " + part1ElapsedTime/50 + " ms");
            Console.WriteLine("Part 2: " + part2ElapsedTime/50 + " ms");
            Console.WriteLine($"Total: {(part1ElapsedTime / 50) + (part2ElapsedTime / 50)} ms");
            Console.ReadLine();
        }

        public void runYearProfiled(string year, int day)
        {
            Stopwatch stopWatch = new Stopwatch();

            Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(2);
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            
            double YearET = 0;

            for(int x  = 1; x <= day; x++) { 
                double part1ElapsedTime = 0;
                double part2ElapsedTime = 0;

                for (int i = 1; i <= 50; i++)
                {
                    Day dayToRun = null;
                    string classToRun = "AdventOfCode._" + year + ".Day" + day;
                    Type type = Type.GetType(classToRun, true);
                    dayToRun = (Day)Activator.CreateInstance(type);
                    stopWatch.Reset();

                    stopWatch.Start();

                    dayToRun.part1();

                    stopWatch.Stop();

                    TimeSpan ts = stopWatch.Elapsed;
                    Console.WriteLine("Part 1: " + ts.TotalMilliseconds + " ms");

                    part1ElapsedTime += ts.TotalMilliseconds;

                    stopWatch.Reset();

                    stopWatch.Start();

                    dayToRun.part2();

                    stopWatch.Stop();

                    ts = stopWatch.Elapsed;
                    Console.WriteLine("Part 2: " + ts.TotalMilliseconds + " ms");
                    part2ElapsedTime += ts.TotalMilliseconds;
                }

                Console.WriteLine("Part 1: " + part1ElapsedTime / 50 + " ms");
                Console.WriteLine("Part 2: " + part2ElapsedTime / 50 + " ms");
                Console.WriteLine($"Total: {(part1ElapsedTime / 50) + (part2ElapsedTime / 50)} ms");
                YearET += (part1ElapsedTime / 50) + (part2ElapsedTime / 50);
                }
            Console.WriteLine($"Year Total: {YearET} ms");
            Console.ReadLine();
        }
    }
    }
