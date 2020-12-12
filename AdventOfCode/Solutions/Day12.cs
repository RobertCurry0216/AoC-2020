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
                { "N", new[] { 0, 1 } },
                { "S", new[] { 0, -1 } },
                { "E", new[] { 1, 0 } },
                { "W", new[] { -1, 0 } }
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
            var ferry = new[] { 0, 0 };
            var waypoint = new[] { 10, 1 };

            _input.ForEach(inst =>
            {
                if ("NSEW".Contains(inst.action))
                {
                    waypoint[0] += Dirs[inst.action][0] * inst.value;
                    waypoint[1] += Dirs[inst.action][1] * inst.value;
                }
                else if (inst.action == "F")
                {
                    ferry[0] += waypoint[0] * inst.value;
                    ferry[1] += waypoint[1] * inst.value;
                }
                else
                {
                    var rot = inst.action == "R" ? inst.value : 360 - inst.value;
                    var coord = (int[])waypoint.Clone();
                    switch (rot)
                    {
                        case 90:
                            waypoint[0] = coord[1];
                            waypoint[1] = coord[0] * -1;
                            break;
                        case 180:
                            waypoint[0] = coord[0] * -1;
                            waypoint[1] = coord[1] * -1;
                            break;
                        case 270:
                            waypoint[0] = coord[1] * -1;
                            waypoint[1] = coord[0];
                            break;
                        default:
                            throw new Exception("Invalid rotation value");
                    }
                }
            });

            return $"{Math.Abs(ferry[0]) + Math.Abs(ferry[1])}";
        }
    }
}