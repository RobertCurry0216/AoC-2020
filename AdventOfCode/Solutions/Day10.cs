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
            long count;

            var jolts = new List<int>(_input);
            jolts.Sort();

            count = GetChargerArrangements(0, jolts, new long[jolts.Count]);

            return $"{count}";
        }

        private long GetChargerArrangements(int idx, List<int> jolts, long[] cache)
        {
            if (cache[idx] != 0) return cache[idx];
            if (idx >= jolts.Count - 1) return 1;

            var value = jolts[idx];
            long count = 0;

            for (int i = idx + 1; i < Math.Min(jolts.Count, idx + 4); i++)
            {
                if (jolts[i] - value <= 3) count += GetChargerArrangements(i, jolts, cache);
            }

            cache[idx] = count;

            return count;
        }
    }
}
