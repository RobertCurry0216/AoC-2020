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
        private readonly List<int> _input;

        public Day01()
        {
            _input = File.ReadAllText(InputFilePath).Split('\n').Select(n => int.Parse(n)).ToList();
        }

        public override string Solve_1()
        {
            foreach (var i in _input)
            {
                foreach (var j in _input)
                {
                    if (i + j == 2020) return $"{i * j}";
                }
            }
            return $"n/a";
        }

        public override string Solve_2()
        {
            foreach (var i in _input)
            {
                foreach (var j in _input)
                {
                    foreach (var k in _input)
                    {
                        if (i + j + k == 2020) return $"{i * j * k}";
                    }
                }
            }
            return $"n/a";
        }
    }
}
