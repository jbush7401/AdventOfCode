using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdventOfCode._2018
{
    class Day8 : Day
    {
        enum Mode { Header, Meta }

        string lines;
        private int[] input;

        public void part1()
        {
            Initialize();
            int counter = 0;

            Mode mode = Mode.Header;
            List<Node> nodesToProcess = new List<Node>();
            List<Node> nodesCompleted = new List<Node>();
            Node newNode;
            Node currentNode;

            while(counter < input.Length)
            {
                if (mode == Mode.Header)
                {
                    newNode = new Node(input[counter], input[counter + 1]);
                    nodesToProcess.Add(newNode);
                    counter += 2;
                }

                currentNode = nodesToProcess.LastOrDefault();
                if (currentNode.childNodesRemaining == 0)
                {
                    mode = Mode.Meta;
                }
                else
                {
                    mode = Mode.Header;
                    currentNode.childNodesRemaining--;
                }

                if (mode == Mode.Meta)
                {
                    for (int i = 0; i < currentNode.qMetadata; i++)
                    {
                        currentNode.metadata[i] = input[counter];
                        counter++;
                    }
                    if (currentNode.childNodesRemaining == 0)
                        mode = Mode.Meta;
                    else
                        mode = Mode.Header;
                    nodesCompleted.Add(currentNode);
                    nodesToProcess.Remove(currentNode);
                }
            }

            int sum = nodesCompleted.Sum(t => t.metadata.Sum());
            Console.WriteLine($"Part 2: {sum}");

        }

        public void part2()
        {
            int counter = 0;

            Mode mode = Mode.Header;
            List<Node> nodesToProcess = new List<Node>();
            List<Node> nodesCompleted = new List<Node>();
            Node newNode;
            Node currentNode = null;

            while (counter < input.Length)
            {
                if (mode == Mode.Header)
                {
                    newNode = new Node(input[counter], input[counter + 1]);
                    newNode.parent = currentNode;
                    nodesToProcess.Add(newNode);
                    counter += 2;
                }

                currentNode = nodesToProcess.LastOrDefault();
                if (currentNode.childNodesRemaining == 0)
                {
                    mode = Mode.Meta;
                }
                else
                {
                    mode = Mode.Header;
                    currentNode.childNodesRemaining--;
                }

                if (mode == Mode.Meta)
                {
                    for (int i = 0; i < currentNode.qMetadata; i++)
                    {
                        currentNode.metadata[i] = input[counter];
                        counter++;
                    }
                    if (currentNode.childNodesRemaining == 0)
                        mode = Mode.Meta;
                    else
                        mode = Mode.Header;
                    nodesCompleted.Add(currentNode);
                    nodesToProcess.Remove(currentNode);

                    if(currentNode.parent != null)
                        currentNode.parent.Children.Add(currentNode);
                }
            }

            Node n = nodesCompleted.Where(t => t.parent == null).SingleOrDefault();
            List<Node> nodes = new List<Node>();
            nodes.Add(n);
            bool noChildren = false;
            List<Node> currentGen = new List<Node>();
            List<Node> tempHolder = new List<Node>();
            currentGen.Add(n);
            while (!noChildren)
            {
                tempHolder = currentGen.ToList();
                currentGen.Clear();
                if (tempHolder.Count == 0)
                    noChildren = true;
                foreach(Node x in tempHolder)
                {
                    foreach (Node y in x.Children) { 
                        currentGen.Add(y);
                        if (y.Children.Count == 0)
                            y.value = y.metadata.Sum();
                    }
                }
                nodes.AddRange(currentGen);
            }

            for(int i = nodes.Count - 1; i >= 0; i--)
            {
                if(nodes[i].Children.Count > 0)
                {
                    foreach(int m in nodes[i].metadata)
                    {
                        if (m <= nodes[i].Children.Count())
                        {
                            nodes[i].value += nodes[i].Children[m - 1].value;
                        }
                    }
                }
            }

            Console.WriteLine($"Part 2: {n.value}");
        }

        public void Initialize()
        {
            lines = Utilities.ReadInputDelimited("Day8_P1_2018.txt");
            input = Array.ConvertAll(lines.Split(' '), int.Parse);
        }

        class Node
        {
            public int qChildNodes;
            public int qMetadata;

            public int[] metadata;
            public int childNodesRemaining;

            public List<Node> Children;
            public Node parent;
            public int value;

            public Node(int qChildNodes, int qMetadata)
            {
                this.qChildNodes = qChildNodes;
                this.Children = new List<Node>();
                this.childNodesRemaining = qChildNodes;
                this.qMetadata = qMetadata;
                this.metadata = new int[qMetadata];
            }
        }
    }
}
