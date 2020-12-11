using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    class Day11 : Day
    {
        WaitingArea waitingArea;
        int columns;
        int rows;
        public void part1()
        {
            string[] lines = Utilities.ReadInput("Day11_P1_2020.txt").ToArray();

            List<WaitingArea> waitingAreas = new List<WaitingArea>();
            bool isStabilized = false;
            columns = lines[0].Length;
            rows = lines.Length;
            waitingArea = new WaitingArea(columns, rows);

            for (int row = 0; row < waitingArea.rows; row++)
                for(int col = 0; col < waitingArea.columns; col++) {
                    if (lines[row][col] == 'L')
                        waitingArea.seats[row, col] = new Seat(TileType.Seat);
                    else
                        waitingArea.seats[row, col] = new Seat(TileType.Floor);
                }

            waitingAreas.Add(waitingArea);

            while (!isStabilized)
            {
                WaitingArea newWaitingArea = new WaitingArea(columns, rows);
                isStabilized = true;
                for (int row = 0; row < waitingArea.seats.GetLength(0); row++)
                    for (int col = 0; col < waitingArea.seats.GetLength(1); col++)
                    {
                        newWaitingArea.seats[row, col] = checkAdjacency(waitingAreas.Last(), row, col);

                        if (newWaitingArea.seats[row, col].IsOccupied != waitingAreas.Last().seats[row, col].IsOccupied)
                            isStabilized = false;
                    }
                waitingAreas.Add(newWaitingArea);
            }

            int finalNumOccupied = 0;

            foreach(Seat s in waitingAreas.Last().seats)
            {
                if (s.IsOccupied)
                    finalNumOccupied++;
            }
            Console.WriteLine($"Part 1: {finalNumOccupied}");
        }

        public void part2()
        {
            List<WaitingArea> waitingAreas = new List<WaitingArea>();
            bool isStabilized = false;

            waitingAreas.Add(waitingArea);

            while (!isStabilized)
            {
                WaitingArea newWaitingArea = new WaitingArea(columns, rows);
                isStabilized = true;
                for (int row = 0; row < waitingArea.seats.GetLength(0); row++)
                    for (int col = 0; col < waitingArea.seats.GetLength(1); col++)
                    {
                        newWaitingArea.seats[row, col] = checkAdjacencyPart2(waitingAreas.Last(), row, col);

                        if (newWaitingArea.seats[row, col].IsOccupied != waitingAreas.Last().seats[row, col].IsOccupied)
                            isStabilized = false;
                    }

                waitingAreas.Add(newWaitingArea);
            }

            int finalNumOccupied = 0;

            foreach (Seat s in waitingAreas.Last().seats)
            {
                if (s.IsOccupied)
                    finalNumOccupied++;
            }

            Console.WriteLine($"Part 2: {finalNumOccupied}");
        }

        Seat checkAdjacency(WaitingArea w, int row, int col)
        {
            Seat s = new Seat(w.seats[row, col].tile);
            s.IsOccupied = w.seats[row, col].IsOccupied;
            int numAdjSeatsOccupied = 0;

            if (s.tile == TileType.Floor)
                return s;

            if (row - 1 >= 0 && col - 1 >= 0 && w.seats[row - 1, col - 1].IsOccupied)
                numAdjSeatsOccupied++;
            if (row - 1 >= 0 && w.seats[row - 1, col].IsOccupied)
                numAdjSeatsOccupied++;
            if (row - 1 >= 0 && col + 1 < columns && w.seats[row - 1 , col + 1].IsOccupied)
                numAdjSeatsOccupied++;
            if (col + 1 < columns && w.seats[row, col + 1].IsOccupied)
                numAdjSeatsOccupied++;
            if (row + 1 < rows && col + 1 < columns && w.seats[row + 1, col + 1].IsOccupied)
                numAdjSeatsOccupied++;
            if (row + 1 < rows && w.seats[row + 1, col].IsOccupied)
                numAdjSeatsOccupied++;
            if (row + 1 < rows && col -1 >= 0 && w.seats[row + 1, col - 1].IsOccupied)
                numAdjSeatsOccupied++;
            if (col - 1 >= 0 && w.seats[row, col - 1].IsOccupied)
                numAdjSeatsOccupied++;

            if (numAdjSeatsOccupied == 0 && w.seats[row, col].IsOccupied == false)
                s.IsOccupied = true;

            if (numAdjSeatsOccupied >= 4 && w.seats[row, col].IsOccupied == true)
                s.IsOccupied = false;

            return s;
        }

        Seat checkAdjacencyPart2(WaitingArea w, int row, int col)
        {
            Seat s = new Seat(w.seats[row, col].tile);
            s.IsOccupied = w.seats[row, col].IsOccupied;
            int numAdjSeatsOccupied = 0;
            int numModifier;
            if (s.tile == TileType.Floor)
                return s;

            //UpLeft
            numModifier = 1;
            while(row - numModifier >= 0 && col - numModifier >= 0) {
                if (w.seats[row - numModifier, col - numModifier].tile == TileType.Seat && !w.seats[row - numModifier, col - numModifier].IsOccupied)
                    break;
                if (w.seats[row - numModifier, col - numModifier].IsOccupied) { 
                    numAdjSeatsOccupied++;
                    break;
                }
                numModifier++;
            }

            numModifier = 1;
            //Up
            while (row - numModifier >= 0)
            {
                if (w.seats[row - numModifier, col].tile == TileType.Seat && !w.seats[row - numModifier, col].IsOccupied)
                    break;
                if (w.seats[row - numModifier, col].IsOccupied)
                {
                    numAdjSeatsOccupied++;
                    break;
                }
                numModifier++;
            }

            numModifier = 1;
            //UpRight
            while (row - numModifier >= 0 && col + numModifier < columns)
            {
                if (w.seats[row - numModifier, col + numModifier].tile == TileType.Seat && !w.seats[row - numModifier, col + numModifier].IsOccupied)
                    break;
                if (w.seats[row - numModifier, col + numModifier].IsOccupied)
                {
                    numAdjSeatsOccupied++;
                    break;
                }
                numModifier++;
            }

            numModifier = 1;
            //Right
            while (col + numModifier < columns)
            {
                if (w.seats[row, col + numModifier].tile == TileType.Seat && !w.seats[row, col + numModifier].IsOccupied)
                    break;
                if (w.seats[row, col + numModifier].IsOccupied)
                {
                    numAdjSeatsOccupied++;
                    break;
                }
                numModifier++;
            }

            numModifier = 1;
            //DownRight
            while (row + numModifier < rows && col + numModifier < columns)
            {
                if (w.seats[row + numModifier, col + numModifier].tile == TileType.Seat && !w.seats[row + numModifier, col + numModifier].IsOccupied)
                    break;
                if (w.seats[row + numModifier, col + numModifier].IsOccupied)
                {
                    numAdjSeatsOccupied++;
                    break;
                }
                numModifier++;
            }

            numModifier = 1;
            //Down
            while (row + numModifier < rows)
            {
                if (w.seats[row + numModifier, col].tile == TileType.Seat && !w.seats[row + numModifier, col].IsOccupied)
                    break;
                if (w.seats[row + numModifier, col].IsOccupied)
                {
                    numAdjSeatsOccupied++;
                    break;
                }
                numModifier++;
            }

            numModifier = 1;
            //DownLeft
            while (row + numModifier < rows && col - numModifier >= 0)
            {
                if (w.seats[row + numModifier, col - numModifier].tile == TileType.Seat && !w.seats[row + numModifier, col - numModifier].IsOccupied)
                    break;
                if (w.seats[row + numModifier, col - numModifier].IsOccupied)
                {
                    numAdjSeatsOccupied++;
                    break;
                }
                numModifier++;
            }

            numModifier = 1;
            //Left
            while (col - numModifier >= 0)
            {
                if (w.seats[row, col - numModifier].tile == TileType.Seat && !w.seats[row, col - numModifier].IsOccupied)
                    break;
                if (w.seats[row, col - numModifier].IsOccupied)
                {
                    numAdjSeatsOccupied++;
                    break;
                }
                numModifier++;
            }

            if (numAdjSeatsOccupied == 0 && w.seats[row, col].IsOccupied == false)
                s.IsOccupied = true;

            if (numAdjSeatsOccupied >= 5 && w.seats[row, col].IsOccupied == true)
                s.IsOccupied = false;

            return s;
        }

        class WaitingArea
        {
            public int columns;
            public int rows;

            public Seat[,] seats;

            public WaitingArea(int columns, int rows)
            {
                this.columns = columns;
                this.rows = rows;
                seats = new Seat[rows, columns];
            }
        }

        class Seat
        {
            public TileType tile;
            public bool IsOccupied = false;

            public Seat(TileType tile)
            {
                this.tile = tile;
            }
        }

        enum TileType { Seat, Floor }
    }
}
