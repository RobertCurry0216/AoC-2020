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
    class Day13 : BaseDay
    {
        private readonly string[] _input;
        private int DepartTime;

        public Day13()
        {
            var input = File.ReadAllLines(InputFilePath).ToList();
            DepartTime = int.Parse(input[0]);
            _input = input[1].Split(',');
        }

        public override string Solve_1()
        {
            var times = _input.Where(s => int.TryParse(s, out var i)).Select(i =>
            {
                var id = int.Parse(i);
                var time = ((DepartTime / id) + 1) * id;
                var wait = time - DepartTime;
                return (id, time, wait);
            }).OrderBy(v => v.time).ThenBy(v => v.wait).ToList();
            return $"{times[0].id * times[0].wait}";
        }

        public override string Solve_2()
        {
            var times = _input.Where(i => int.TryParse(i, out _)).Select(i => int.Parse(i)).ToList();
            var lcm = times.Aggregate(times.First(), (int acc, int val) => Lcm(acc, val));

            

            return $"{-1}";
        }

        private int Lcm(int a, int b)
        {
            if (a == 0 || b == 0) return 0;
            return (a * b) / Gcd(a, b);
        }

        private int Gcd(int a, int b)
        {
            if (a < 1 || b < 1)
            {
                throw new Exception("a or b is less than 1");
            }
            int remainder;
            do
            {
                remainder = a % b;
                a = b;
                b = remainder;
            } while (b != 0);
            return a;
        }
    }
}
