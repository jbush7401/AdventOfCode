using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode._2018
{
    class Day7 : Day
    {
        List<Step> steps = new List<Step>();
        public void part1()
        {
            Initialize();
            string finalOrder = "";

            while(steps.Count > 0) { 
                var readyToBeProcessed = steps.Where(t => t.requirements.Count == 0).OrderBy(t => t.letter).FirstOrDefault();
                while(readyToBeProcessed != null) { 
                    finalOrder += readyToBeProcessed.letter;
                    steps.Remove(readyToBeProcessed);
                    var removeLetterFromReq = steps.Where(t => t.requirements.Contains(readyToBeProcessed.letter));
                    foreach(Step s in removeLetterFromReq)
                    {
                        s.requirements.Remove(readyToBeProcessed.letter);
                    }
                    readyToBeProcessed = steps.Where(t => t.requirements.Count == 0).OrderBy(t => t.letter).FirstOrDefault();
                }
            }
            Console.WriteLine($"Part 1: {finalOrder}");
        }

        public void part2()
        {
            Initialize();
            int totalSeconds = 0;
            Worker[] workers = new Worker[5] { 
                new Worker(), new Worker(), new Worker(), new Worker(), new Worker()
            };

            while (steps.Count > 0 || workers.Where(t => t.currentJob != null).Count() > 0)
            {
                var completedJobs = workers.Where(t => t.secondsLeft == 0);
                foreach(var worker in completedJobs)
                {
                    
                    var removeLetterFromReq = steps.Where(t => t.requirements.Contains(worker.currentJob.letter));
                    foreach (Step s in removeLetterFromReq)
                    {
                        s.requirements.Remove(worker.currentJob.letter);
                    }
                    worker.currentJob = null;
                    worker.secondsLeft = -1;
                }

                var getAWorker = workers.Where(t => t.currentJob == null);

                foreach (var worker in getAWorker)
                {
                    var availableJob = steps.Where(t => t.requirements.Count == 0).OrderBy(t => t.letter).FirstOrDefault();
                    if(availableJob != null) {
                        worker.currentJob = availableJob;
                        steps.Remove(worker.currentJob);
                        worker.secondsLeft = availableJob.letter - 4;
                    }
                }
                foreach(Worker w in workers)
                {
                    w.secondsLeft--;
                }

                totalSeconds++;
            }

            totalSeconds--;
            Console.WriteLine($"Part 2: {totalSeconds}");
        }

        void Initialize()
        {
            steps = new List<Step>();
            var lines = Utilities.ReadInput("Day7_P1_2018.txt");

            char mustBeFinished;
            char beforeCanBegin;
            Step addToStep;
            foreach (var line in lines)
            {
                mustBeFinished = line[5];
                beforeCanBegin = line[36];

                addToStep = steps.Where(t => t.letter == mustBeFinished).SingleOrDefault();

                if (addToStep == null)
                {
                    addToStep = new Step(mustBeFinished);
                    steps.Add(addToStep);
                }

                addToStep = steps.Where(t => t.letter == beforeCanBegin).SingleOrDefault();

                if (addToStep == null)
                {
                    addToStep = new Step(beforeCanBegin);
                    steps.Add(addToStep);
                }

                addToStep.requirements.Add(mustBeFinished);
            }
        }

        class Worker
        {
            public Step currentJob;
            public int secondsLeft = -1;
        }
        class Step
        {
            public char letter;
            public HashSet<char> requirements = new HashSet<char>();

            public Step(char letter)
            {
                this.letter = letter;
            }
        }
    }
}
