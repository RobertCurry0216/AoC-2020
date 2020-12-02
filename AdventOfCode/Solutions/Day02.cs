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
                var re = new Regex(@"(?<low>\d+)-(?<high>\d+) (?<chr>\w): (?<str>\w+)");
                var m = re.Match(input);
                var low = int.Parse(m.Groups["low"].Value);
                var high = int.Parse(m.Groups["high"].Value);
                var c = new Regex(m.Groups["chr"].Value).Matches(m.Groups["str"].Value).Count;
                if (low <= c && c <= high) count++;
            }
            return $"{count}";
        }

        public override string Solve_2()
        {
            var count = 0;
            foreach (var input in _input)
            {
                var re = new Regex(@"(?<index1>\d+)-(?<index2>\d+) (?<chr>\w): (?<str>\w+)");
                var m = re.Match(input);
                var index1 = int.Parse(m.Groups["index1"].Value) - 1;
                var index2 = int.Parse(m.Groups["index2"].Value) - 1;
                var c = m.Groups["chr"].Value.ToCharArray().First();
                var str = m.Groups["str"].Value;
                if (str[index1] == c ^ str[index2] == c) count++;
            }
            return $"{count}";
        }
    }
}
