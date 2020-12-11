using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day11 : BaseDay
    {
        private char[][] _input;

        public Day11()
        {
            _input = File.ReadAllLines(InputFilePath).Select(l => l.ToCharArray()).ToArray();
        }

        private static T[][] CloneNestedArray<T>(T[][] nested) 
            => nested.Select(r => (T[])r.Clone()).ToArray();

        public override string Solve_1()
        {
            var count = 0;
            var seats = CloneNestedArray(_input);
            var seatCache = CloneNestedArray(_input);
            var seatChanged = true;

            while (seatChanged)
            {
                seatChanged = false;
                for (int y = 0; y < seats.Length; y++)
                {
                    for (int x = 0; x < seats[0].Length; x++)
                    {
                        if (CheckSeatSwap(seats, y, x))
                        {
                            seatChanged = true;
                            seatCache[y][x] = seats[y][x] == 'L' ? '#' : 'L';
                        }
                    }
                }
                seats = seatCache;
                seatCache = CloneNestedArray(seatCache);
            }
            count = seats.Sum(r => r.Sum(s => s == '#' ? 1 : 0));
            return $"{count}";
        }

        private static bool CheckSeatSwap(char[][] seats, int y, int x)
        {
            if (seats[y][x] == '.') return false;
            var adj = CheckAdjacent(seats, y, x);
            if (seats[y][x] == 'L' && adj == 0) return true;
            if (seats[y][x] == '#' && adj >= 4) return true;
            return false;
        }

        private static int CheckAdjacent(char[][] seats, int y, int x)
        {
            var dirs = new int[][] {
                new[] { -1, -1},
                new[] { -1, 0},
                new[] { -1, 1},
                new[] { 0, -1},
                new[] { 0, 1},
                new[] { 1, -1},
                new[] { 1, 0},
                new[] { 1, 1},
            };
            var count = 0;
            foreach (var dir in dirs)
            {
                var dy = y + dir[0];
                var dx = x + dir[1];
                if (dx < 0 || dy < 0 || dx >= seats[0].Length || dy >= seats.Length) continue;
                if (seats[dy][dx] == '#') count++;
            }

            return count;
        }

        public override string Solve_2()
        {
            var count = 0;
            return $"{count}";
        }
    }
}
