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
            var n = new List<long>();            
            var a = new List<long>();

            for (int i = 0; i < _input.Length; i++)
            {
                if (long.TryParse(_input[i], out var time))
                {
                    n.Add(time);
                    a.Add(time - i);
                }
            }

            var departTime = ChineseRemainderTheorem.Solve(n.ToArray(), a.ToArray());

            return $"{departTime}";
        }

        public static class ChineseRemainderTheorem
        {
            //https://rosettacode.org/wiki/Chinese_remainder_theorem#C.23

            public static long Solve(long[] n, long[] a)
            {
                long prod = n.Aggregate((long)1, (i, j) => i * j);
                long p;
                long sm = 0;
                for (long i = 0; i < n.Length; i++)
                {
                    p = prod / n[i];
                    sm += a[i] * ModularMultiplicativeInverse(p, n[i]) * p;
                }
                return sm % prod;
            }

            private static long ModularMultiplicativeInverse(long a, long mod)
            {
                long b = a % mod;
                for (long x = 1; x < mod; x++)
                {
                    if ((b * x) % mod == 1)
                    {
                        return x;
                    }
                }
                return 1;
            }
        }
    }
}
