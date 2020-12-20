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
    class Day18 : BaseDay
    {
        private readonly List<string> _input;

        public Day18()
        {
            _input = File.ReadAllLines(InputFilePath).ToList();
        }

        public override string Solve_1()
        {
            long count = 0;
            var reExp = new Regex(@"(?<full>\((?<exp>[0-9 +*]+)\))");

            for (int i = 0; i < _input.Count; i++)
            {
                var exp = _input[i];
                while (reExp.Match(exp).Success)
                {
                    var m = reExp.Match(exp);
                    exp = exp.Replace(m.Groups["full"].Value, $"{Eval(m.Groups["exp"].Value)}");
                }
                count += Eval(exp);
            }

            return $"{count}";
        }

        private long Eval(string eq)
        {
            var tokens = eq.Split();
            var val = long.Parse(tokens.First());

            for (int i = 1; i < tokens.Length; i++)
            {
                var op = tokens[i];
                i++;
                switch (op)
                {
                    case "+":
                        val += long.Parse(tokens[i]);
                        break;
                    case "*":
                        val *= long.Parse(tokens[i]);
                        break;
                    default:
                        throw new Exception($"unknown op {op}");
                }
            }
            return val;
        }

        public override string Solve_2()
        {
            long count = 0;
            var reExp = new Regex(@"(?<full>\((?<exp>[0-9 +*]+)\))");

            for (int i = 0; i < _input.Count; i++)
            {
                var exp = _input[i];
                while (reExp.Match(exp).Success)
                {
                    var m = reExp.Match(exp);
                    exp = exp.Substring(0, m.Index) + $"{Eval2(m.Groups["exp"].Value)}" + exp.Substring(m.Index + m.Length);
                }
                count += Eval2(exp);
            }

            return $"{count}";
        }

        private long Eval2(string exp)
        {
            var reExp = new Regex(@"(?<exp>[0-9]+ \+ [0-9]+)");

            while (reExp.Match(exp).Success)
            {
                var m = reExp.Match(exp);
                
                exp = exp.Substring(0, m.Index) + $"{Eval(m.Groups["exp"].Value)}" + exp.Substring(m.Index + m.Length);
            }

            return Eval(exp);
        }
    }
}
