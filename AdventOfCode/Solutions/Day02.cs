using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day02 : BaseDay
    {
        private readonly List<string> _input;

        public Day02()
        {
            _input = File.ReadAllLines(InputFilePath).ToList();
        }

        public override string Solve_1()
        {
            var count = 0;
            foreach (var input in _input)
            {
                
            }
            return $"{count}";
        }

        public override string Solve_2()
        {
            
            return $"n/a";
        }
    }
}
