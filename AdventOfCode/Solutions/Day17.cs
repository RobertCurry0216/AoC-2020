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
    class Day17 : BaseDay
    {
        private bool[,,] pocket;

        public Day17()
        {
            var y = 498;
            pocket = CreatePocket();
            File.ReadAllLines(InputFilePath).ToList()
                .ForEach(l => 
                {
                    var x = 498;
                    foreach (var c in l.ToCharArray())
                    {
                        if (c == '#')
                            pocket[x, y, 0] = true;
                        x++;
                    }
                    y++;
                });
        }

        private bool[,,] CreatePocket()
        {
            return new bool[1000,1000,1000];
        }

        public override string Solve_1()
        {
            var count = 0;
            return $"{count}";
        }

        public override string Solve_2()
        {
            var count = 0;
            return $"{count}";
        }
    }
}
