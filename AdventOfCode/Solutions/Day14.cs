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
    class Day14 : BaseDay
    {
        private readonly List<string> _input;

        public Day14()
        {
            _input = File.ReadAllLines(InputFilePath).ToList();
        }

        public override string Solve_1()
        {
            var reMask = new Regex(@"mask = (?<mask>\w+)");
            var reMem = new Regex(@"mem\[(?<address>\d+)+\] = (?<value>\d+)");
            BitMask mask = new BitMask("0");
            var memory = new Dictionary<int, long>();

            _input.ForEach(line => 
            {
                var m = reMem.Match(line);
                if (m.Success)
                {
                    var k = int.Parse(m.Groups["address"].Value);
                    var v = int.Parse(m.Groups["value"].Value);
                    memory[k] = mask.Apply(v);
                }
                else
                {
                    m = reMask.Match(line);
                    mask = new BitMask(m.Groups["mask"].Value);
                }
            });

            return $"{memory.Values.ToList().Sum()}";
        }

        public override string Solve_2()
        {
            var reMask = new Regex(@"mask = (?<mask>\w+)");
            var reMem = new Regex(@"mem\[(?<address>\d+)+\] = (?<value>\d+)");
            MultiBitMask mask = new MultiBitMask("0");
            var memory = new Dictionary<long, long>();

            _input.ForEach(line =>
            {
                var m = reMem.Match(line);
                if (m.Success)
                {
                    var k = int.Parse(m.Groups["address"].Value);
                    var v = int.Parse(m.Groups["value"].Value);
                    mask.Apply(k).ForEach(key => memory[key] = v);
                }
                else
                {
                    m = reMask.Match(line);
                    mask = new MultiBitMask(m.Groups["mask"].Value);
                }
            });

            return $"{memory.Values.ToList().Sum()}";
        }

        private class BitMask
        {
            private long mask1;
            private long mask0;

            public BitMask(string value)
            {
                mask1 = 0;
                mask0 = 0;
                value.ToCharArray().ToList().ForEach(c => 
                {
                    mask1 = mask1 << 1;
                    mask0 = mask0 << 1;
                    mask1 += c == '1' ? 1 : 0;
                    mask0 += c == '0' ? 1 : 0;
                });
            }

            internal long Apply(long v)
            {
                // 1
                v = v | mask1;

                // 0
                v = ~v;
                v = v | mask0;
                v = ~v;

                return v;
            }
        }

        private class MultiBitMask
        {
            private List<BitMask> masks = new List<BitMask>();

            public MultiBitMask(string mask)
            {
                var maskStrings = new List<StringBuilder>() { new StringBuilder() };
                mask.ToCharArray().ToList().ForEach(c => 
                {
                    var newMaskStrings = new List<StringBuilder>();
                    for (int i = 0; i < maskStrings.Count; i++)
                    {
                        var m = maskStrings[i];
                        switch (c)
                        {
                            case '1':
                                m.Append("1");
                                break;
                            case 'X':
                                var n = new StringBuilder(m.ToString());
                                n.Append("0");
                                m.Append("1");
                                newMaskStrings.Add(n);
                                break;
                            case '0':
                                m.Append("X");
                                break;
                            default:
                                break;
                        }
                    }
                    maskStrings = maskStrings.Union(newMaskStrings).ToList();
                });

                masks = maskStrings.Select(ms => new BitMask(ms.ToString())).ToList();
            }

            internal List<long> Apply(long k)
            {
                var outList = masks.Select(m => m.Apply(k)).ToList();
                return outList;
            }
        }
    }
}
