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
    class Day05 : BaseDay
    {
        private readonly List<string> _input;

        public Day05()
        {
            _input = File.ReadAllLines(InputFilePath).ToList();
        }

        private (int row, int col) ScanBoardingPass(string code)
        {
            int row = 0;
            int col = 0;

            int max = 127;
            int min = 0;
            for (int i = 0; i < 7; i++)
            {
                if (code[i] == 'F')
                {
                    max = (max + min) / 2;
                }
                else
                {
                    min = (max + min) / 2;
                }
            }
            row = max;

            max = 7;
            min = 0;
            for (int i = 7; i < 10; i++)
            {
                if (code[i] == 'L')
                {
                    max = (max + min) / 2;
                }
                else
                {
                    min = (max + min) / 2;
                }
            }
            col = max;

            return (row, col);
        }

        private int SeatId((int row, int col) seat) => (seat.row * 8) + seat.col;

        public override string Solve_1()
        {
            var highest = 0;

            _input.ForEach(l =>
            {
                var seat = ScanBoardingPass(l);
                highest = Math.Max(highest, SeatId(seat));
            });

            return $"{highest}";
        }

        public override string Solve_2()
        {
            var seatIds = _input.Select(s => SeatId(ScanBoardingPass(s))).ToList();
            seatIds.Sort();

            for (int i = 0; i < seatIds.Count-1; i++)
            {
                if (seatIds[i + 1] - seatIds[i] != 2) continue;
                return $"{seatIds[i] + 1}";
            }

            return $"{-1}";
        }
    }
}
