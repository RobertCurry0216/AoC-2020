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
    class Day06 : BaseDay
    {
        private readonly List<List<string>> _input;

        public Day06()
        {
            _input = new List<List<string>>();
            var lineGroup = new List<string>();
            foreach (string line in File.ReadAllLines(InputFilePath))
            {
                if(string.IsNullOrEmpty(line))
                {
                    _input.Add(lineGroup);
                    lineGroup = new List<string>();
                    continue;
                }
                lineGroup.Add(line.Trim());
            }
            _input.Add(lineGroup);
        }

        public override string Solve_1()
        {
            var count = 0;

            count = _input.Aggregate(0, (acc, group) =>
            {
                var s = new HashSet<char>();
                group.ForEach(l => s.UnionWith(l.ToCharArray()));
                return acc + s.Count;
            });

            return $"{count}";
        }

        public override string Solve_2()
        {
            var count = 0;

            count = _input.Aggregate(0, (acc, group) =>
            {
                var m = new Dictionary<char, int>();
                group.ForEach(line => line.ToCharArray().ToList().ForEach(c => m[c] = m.ContainsKey(c) ? m[c]+ 1 : 1));
                return acc + m.Where(kvp => kvp.Value == group.Count).Count();
            });

            return $"{count}";
        }
    }
}
