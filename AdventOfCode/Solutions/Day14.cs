﻿using AoCHelper;
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
            private List<long> masks = new List<long>() { 0 };

            public MultiBitMask(string mask)
            {
                mask.ToCharArray().ToList().ForEach(c => 
                {
                    var newMasks = new List<long>();
                    for (int i = 0; i < masks.Count; i++)
                    {
                        var m = masks[i] << 1;
                        switch (c)
                        {
                            case '1':
                                m = m + 1;
                                break;
                            case 'X':
                                newMasks.Add(m);
                                m = m + 1;
                                break;
                            default:
                                break;
                        }
                        masks[i] = m;
                    }
                    masks = masks.Union(newMasks).ToList();
                });
            }

            internal List<long> Apply(long k)
            {
                var outList = new List<long>();
                masks.ForEach(m => 
                {
                    var v = m | k;
                    outList.Add(v); 
                });
                return outList;
            }
        }
    }
}
