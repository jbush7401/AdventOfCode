using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
            int lastMarble = 71588;
            int currentPlayer = 0;

            List<GameArray> games= new List<GameArray>();
            games.Add(new GameArray(10000));

            GameArray game = new GameArray(lastMarble);

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
                    players[currentPlayer].currentScore += game.ScoreMarble(i);
                }
                else
                {
                    //Place current marble
                    game.placeMarble(i);
                    //Go to next Player
                }
                if (currentPlayer == (numPlayers - 1))
                    currentPlayer = 0;
                else
                    currentPlayer++;
            }
            Console.WriteLine($"Part 2: {players.Max(t => t.currentScore)}");
        }

      
        class Player
        {
            public int playerNumber = 0;
            public int currentScore = 0;
        }

        class GameArray
        {
            int[] game;
            int tail = 0;
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
    }
}
