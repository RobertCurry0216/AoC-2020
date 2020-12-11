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
    class Day10 : BaseDay
    {
        private readonly List<int> _input;

        public Day10()
        {
            _input = File.ReadAllLines(InputFilePath).Select(i => int.Parse(i)).ToList();
        }

        public override string Solve_1()
        {
            var count1 = 0;
            var count3 = 0;

            var jolts = new List<int>(_input);
            jolts.Add(0);
            jolts.Sort();
            for (int i = 1; i < jolts.Count; i++)
            {
                var diff = jolts[i] - jolts[i - 1];
                if (diff == 1) count1++;
                if (diff == 3) count3++;
            }
            count3++;
            return $"{count1 * count3}";
        }

        public override string Solve_2()
        {
            var jolts = new List<int>(_input);
            jolts.Add(0);
            jolts.Sort();
            jolts.Add(jolts.Last() + 3);
            var cache = new long[jolts.Count];
            cache[cache.Count() - 1] = 1;

            for (int i = jolts.Count - 2; i >= 0; i--)
            {
                long count = 0;
                for (int j = 1; j < 4; j++)
                {
                    if (i + j >= jolts.Count) break;

                    if (jolts[i + j] - jolts[i] <= 3) count += cache[i + j];
                }
                cache[i] = count;
            }

            return $"{cache[0]}";
        }
    }
}
