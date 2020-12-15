using AoCHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day15 : BaseDay
    {
        private readonly List<int> _input;

        public Day15()
        {
            _input = File.ReadAllText(InputFilePath).Split(',').Select(n => int.Parse(n)).ToList();
        }

        public override string Solve_1()
        {
            var spokenValues = new Dictionary<int, int>();
            for (int i = 0; i < _input.Count - 1; i++)
            {
                spokenValues[_input[i]] = i;
            }

            var lastSpoken = _input.Last();
            for (int i = spokenValues.Count; i < 2020 - 1; i++)
            {
                if (!spokenValues.ContainsKey(lastSpoken))
                {
                    spokenValues[lastSpoken] = i;
                }
                var speaking = i - spokenValues[lastSpoken];
                spokenValues[lastSpoken] = i;
                lastSpoken = speaking;
            }

            return $"{lastSpoken}";
        }

        public override string Solve_2()
        {
            var spokenValues = new Dictionary<long, long>();
            for (int i = 0; i < _input.Count - 1; i++)
            {
                spokenValues[_input[i]] = i;
            }

            long lastSpoken = _input.Last();
            for (long i = spokenValues.Count; i < 30000000 - 1; i++)
            {
                if (!spokenValues.ContainsKey(lastSpoken))
                {
                    spokenValues[lastSpoken] = i;
                }
                var speaking = i - spokenValues[lastSpoken];
                spokenValues[lastSpoken] = i;
                lastSpoken = speaking;
            }

            return $"{lastSpoken}";
        }
    }
}
