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
    class Day09 : BaseDay
    {
        private readonly List<long> _input;

        public Day09()
        {
            _input = File.ReadAllLines(InputFilePath).Select(i => long.Parse(i)).ToList();
        }

        public override string Solve_1()
        {
            for (int i = 25; i < _input.Count; i++)
            {
                var preamble = _input.GetRange(i - 25, 25);
                var value = _input[i];

                if (preamble.All(v => !preamble.Contains(value - v))) 
                {
                    return $"{value}";
                }
            }

            return $"{-1}";
        }

        public override string Solve_2()
        {
            var target = 36845998;

            var sets = new List<List<long>>();

            foreach (var l in _input)
            {
                sets.Add(new List<long>());
                var remove = new List<List<long>>();

                foreach (var set in sets)
                {
                    set.Add(l);
                    var sum = set.Sum();
                    if (sum == target && set.Count() > 1)
                    {
                        set.Sort();
                        return $"{set.First() + set.Last()}";
                    }
                    else if (sum > target)
                    {
                        remove.Add(set);
                    }
                }
                remove.ForEach(r => sets.Remove(r));
            }

            return $"{-1}";
        }
    }
}
