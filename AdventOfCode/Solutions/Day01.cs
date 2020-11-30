using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day01 : BaseDay
    {
        private readonly string _input;

        public Day01()
        {
            _input = File.ReadAllText(InputFilePath);
        }

        public override string Solve_1() => $"Solution to {ClassPrefix} {CalculateIndex()}, {_input}";

        public override string Solve_2() => $"Solution to {ClassPrefix} {CalculateIndex()}, {_input}";
    }
}
