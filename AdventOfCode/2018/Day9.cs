using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018
{
    class Day9 : Day
    {
        public void part1()
        {
            int numPlayers = 430;
            Player[] players = new Player[numPlayers];
            int lastMarble = 71588;
            int currentPlayer = 0;
            int currentMarblePosition = 0;

            List<int> game = new List<int>();

            game.Add(0);
            //i is the current marble number
            //currentMarblePosition is the marble that is the current position
            for (int i = 1; i <= lastMarble; i++)
            {
                //Assign player numbers when unassigned
                if (players[currentPlayer] == null)
                {
                    players[currentPlayer] = new Player();
                    players[currentPlayer].playerNumber = i;
                }

                if (i % 23 == 0)
                {
                    players[currentPlayer].currentScore += i;
                    if (currentMarblePosition - 7 >= 0) {
                        players[currentPlayer].currentScore += game[currentMarblePosition - 7];
                        game.RemoveAt(currentMarblePosition - 7);
                        currentMarblePosition = currentMarblePosition - 7;
                    }
                    else {
                        players[currentPlayer].currentScore += game[game.Count - Math.Abs(currentMarblePosition - 7)];
                        game.RemoveAt(game.Count - Math.Abs(currentMarblePosition - 7));
                        currentMarblePosition = game.Count - Math.Abs(currentMarblePosition - 6);
                    }
                }
                else {
                    //Place current marble
                    if (currentMarblePosition + 2 == game.Count)
                        game.Insert(currentMarblePosition + 2, i);
                    else if (currentMarblePosition + 1 == game.Count)
                        game.Insert(1, i);
                    else
                        game.Insert(currentMarblePosition + 2, i);

                    //Go to next Player
                    currentMarblePosition = game.IndexOf(i);
                }
                if (currentPlayer == (numPlayers - 1))
                    currentPlayer = 0;
                else
                    currentPlayer++;
            }
            Console.WriteLine($"Part 1: {players.Max(t => t.currentScore)}");
        }

        public void part2()
        {
            int numPlayers = 430;
            Player[] players = new Player[numPlayers];
            int lastMarble = 7158800;
            int currentPlayer = 0;

            LinkedList<int> game = new LinkedList<int>();
            game.AddFirst(0);
            LinkedListNode<int> currentMarblePosition = game.First;

            //i is the current marble number
            //currentMarblePosition is the marble that is the current position
            for (int i = 1; i <= lastMarble; i++)
            {
                //Assign player numbers when unassigned
                if (players[currentPlayer] == null)
                {
                    players[currentPlayer] = new Player();
                    players[currentPlayer].playerNumber = i;
                }

                if (i % 23 == 0)
                {
                    players[currentPlayer].currentScore += i;
                    for (int j = 1; j <= 6; j++)
                    {
                        if (currentMarblePosition == game.First)
                            currentMarblePosition = game.Last;
                        else
                            currentMarblePosition = currentMarblePosition.Previous;
                    }
                    players[currentPlayer].currentScore += currentMarblePosition.Previous.Value;
                    game.Remove(currentMarblePosition.Previous);
                }
                else
                {
                    if(currentMarblePosition == game.Last)
                        currentMarblePosition = game.First;
                    else
                        currentMarblePosition = currentMarblePosition.Next;
                    game.AddAfter(currentMarblePosition, i);
                    if (currentMarblePosition == game.Last)
                        currentMarblePosition = game.First;
                    else
                        currentMarblePosition = currentMarblePosition.Next;
                }
                if (currentPlayer == (numPlayers - 1))
                    currentPlayer = 0;
                else
                    currentPlayer++;
            }
            Console.WriteLine($"Part 2: {players.Max(t => t.currentScore)}");
        }

        //Completely unnecessary class after troubleshooting further.
        public class GameArray
        {
            public int[] game;
            public int tail = 0;
            int currentMarblePosition = 0;

            public GameArray(int lastMarble)
            {
                game = new int[lastMarble];
                game[0] = 0;
            }

            public int ScoreMarble(int i)
            {
                int score = i;
                if (currentMarblePosition - 7 >= 0)
                {
                    score += (int)(game[currentMarblePosition - 7]);
                    removeItem(currentMarblePosition - 7);
                    currentMarblePosition = currentMarblePosition - 7;
                }
                else
                {
                    score += (int)(game[tail + 1 - Math.Abs(currentMarblePosition - 7)]);
                    removeItem(tail + 1 - Math.Abs(currentMarblePosition - 7));
                    currentMarblePosition = tail + 1 - Math.Abs(currentMarblePosition - 6);
                }
                return score;
            }

            public void placeMarble(int marble)
            {
                if (currentMarblePosition == tail)
                {
                    insertMarble(1, marble);
                    currentMarblePosition = 1;
                }
                else
                {
                    insertMarble(currentMarblePosition + 2, marble);
                    currentMarblePosition = currentMarblePosition + 2;
                }
            }

            void insertMarble(int insertHere, int replacement)
            {
                for(int i = tail; i >= insertHere; i--)
                {
                    game[i+1] = game[i];
                }
                game[insertHere] = replacement;
                tail++;
            }

            void removeItem(int fromIndex)
            {
                for(int i = fromIndex; i <= tail; i++)
                {
                    game[i] = game[i + 1];
                }
                tail--;
            }
        }

        class Player
        {
            public int playerNumber = 0;
            public long currentScore = 0;
        }
    }
}
