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
    class Day03 : BaseDay
    {
        private readonly List<List<char>> _input;

        public Day03()
        {
            _input = File.ReadAllLines(InputFilePath).Select(
                    l => l.Trim().ToCharArray().ToList()
                ).ToList();
        }

        public override string Solve_1()
        {
            var count = 0;
            var x = 0;

            _input.ForEach(r =>
            {
                if (r[x] == '#') count++;
                x = (x + 3) % 31;
            });

            return $"{count}";
        }

        public override string Solve_2()
        {
            long count = 1;
            var slopes = new[] { 
                new[] { 1, 1 } ,
                new[] { 3, 1 } ,
                new[] { 5, 1 } ,
                new[] { 7, 1 } ,
                new[] { 1, 2 }
            };

            foreach (var slope in slopes)
            {
                var c = 0;
                var x = 0;
                for (int i = 0; i < _input.Count; i += slope[1])
                {
                    if (_input[i][x] == '#') c++;
                    x = (x + slope[0]) % 31;
                }
                count *= c;
            }

            return $"{count}";
        }
    }
}
