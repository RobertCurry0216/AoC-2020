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
    class Day16 : BaseDay
    {
        private List<Rule> Rules = new List<Rule>();
        private List<List<int>> Tickets = new List<List<int>>();

        public Day16()
        {
            var reRule = new Regex(@"^(?<cat>.+): (?<v1>\d+)-(?<v2>\d+) or (?<v3>\d+)-(?<v4>\d+)");
            var reTicket = new Regex(@"(\d+,)+");
            foreach (var line in File.ReadAllLines(InputFilePath))
            {
                if (reTicket.IsMatch(line))
                {
                    Tickets.Add(line.Split(',').Select(i => int.Parse(i)).ToList());
                }
                else if (reRule.IsMatch(line))
                {
                    Rules.Add(new Rule(reRule.Match(line).Groups));
                }
            }

        }

        public override string Solve_1()
        {
            var count = 0;

            foreach (var ticket in Tickets.Skip(1))
            {
                ticket.ForEach(v =>
                {
                    if (!Rules.Any(r => r.IsValid(v)))
                        count += v;
                });
            }

            return $"{count}";
        }

        public override string Solve_2()
        {
            var valid = Tickets.Skip(1).Where(t => t.All(t => Rules.Any(r => r.IsValid(t)))).ToList();
            Rules.ForEach(r =>
                r.index = Enumerable.Range(0, Rules.Count)
                .Where(i => valid.All(t => r.IsValid(t[i])))
                .ToList()
            );

            while (Rules.Any(r => r.index.Count > 1))
            {
                foreach (var rule in Rules)
                {
                    if(rule.index.Count == 1)
                    {
                        Rules.ForEach(r => { if (r != rule) r.index.Remove(rule.index[0]); });
                    }
                }
            }

            long count = Rules.Where(r => r.Category.StartsWith("departure"))
                .Aggregate((long)1, (acc, rule) => 
                {
                    return acc * rule.Get(Tickets[0]);
                });

            return $"{count}";
        }

        internal class Rule
        {
            private int[] bounds;
            public string Category;
            public List<int> index;

            public Rule(GroupCollection groups)
            {
                Category = groups["cat"].Value;
                bounds = new[]
                {
                    int.Parse(groups["v1"].Value),
                    int.Parse(groups["v2"].Value),
                    int.Parse(groups["v3"].Value),
                    int.Parse(groups["v4"].Value)
                };
            }

            public bool IsValid(int v)
            {
                return (bounds[0] <= v && v <= bounds[1]) || (bounds[2] <= v && v <= bounds[3]);
            }

            public int Get(List<int> ticket)
            {
                return ticket[index[0]];
            }
        }
    }
}
