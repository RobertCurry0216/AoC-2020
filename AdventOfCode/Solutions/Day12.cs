using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    internal class Day12 : BaseDay
    {
        private readonly List<(string action, int value)> _input;
        private Dictionary<string, int[]> Dirs;
        private string[] DirList = new[] { "N", "E", "S", "W" };

        public Day12()
        {
            var re = new Regex(@"(?<act>\w)(?<val>\d+)");
            _input = File.ReadAllLines(InputFilePath).Select(l =>
            {
                var m = re.Match(l);
                return (m.Groups["act"].Value, int.Parse(m.Groups["val"].Value));
            }).ToList();

            Dirs = new Dictionary<string, int[]>()
            {
                { "N", new[] { 1, 0 } },
                { "S", new[] { -1, 0 } },
                { "E", new[] { 0, 1 } },
                { "W", new[] { 0, -1 } }
            };
        }

        public override string Solve_1()
        {
            var ferry = new[] { 0, 0 };
            var ferryHeading = 1;

            _input.ForEach(inst =>
            {
                if ("NSEW".Contains(inst.action))
                {
                    ferry[0] += Dirs[inst.action][0] * inst.value;
                    ferry[1] += Dirs[inst.action][1] * inst.value;
                }
                else if (inst.action == "F")
                {
                    ferry[0] += Dirs[DirList[ferryHeading]][0] * inst.value;
                    ferry[1] += Dirs[DirList[ferryHeading]][1] * inst.value;
                }
                else
                {
                    var rot = inst.value / 90;
                    rot *= inst.action == "L" ? -1 : 1;
                    ferryHeading = (ferryHeading + 4 + rot) % 4;
                }
            });

            return $"{Math.Abs(ferry[0]) + Math.Abs(ferry[1])}";
        }

        public override string Solve_2()
        {
            var count = 0;
            return $"{count}";
        }
    }
}