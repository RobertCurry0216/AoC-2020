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
    class Day07 : BaseDay
    {
        private readonly Dictionary<string, List<BagContent>> _input;

        private Dictionary<string, bool> BagContentsCache = new Dictionary<string, bool>();
        private Dictionary<string, int> BagCountCache = new Dictionary<string, int>();

        private struct BagContent
        {
            public int Count;
            public string BagKey;
        }

        public Day07()
        {
            var reKey = new Regex(@"^(?<key>.+?) bags?");
            var reContent = new Regex(@"(?<count>\d\d?) (?<content>.+?) bags?[,.]");

            _input = new Dictionary<string, List<BagContent>>();

            File.ReadAllLines(InputFilePath).ToList().ForEach(line => {
                var contents = new List<BagContent>();

                foreach (Match match in reContent.Matches(line))
                {
                    contents.Add(new BagContent() {
                        Count = int.Parse(match.Groups["count"].Value),
                        BagKey = match.Groups["content"].Value
                    });
                }

                _input.Add(reKey.Match(line).Groups["key"].Value, contents);
            });
        }

        public override string Solve_1()
        {
            var count = 0;

            foreach (var bag in _input.Keys)
            {
                if (CheckBagContents(bag, "shiny gold")) count++;
            }

            return $"{count}";
        }

        private bool CheckBagContents(string bagKey, string targetKey)
        {
            if (BagContentsCache.ContainsKey(bagKey)) return BagContentsCache[bagKey];
            var bag = _input[bagKey];
            var bagContainsKey = bag.Any(b => b.BagKey == targetKey) 
                || bag.Any(b => CheckBagContents(b.BagKey, targetKey));

            BagContentsCache[bagKey] = bagContainsKey;
            return bagContainsKey;
        }

        public override string Solve_2()
        {
            var count = GetBagCount("shiny gold");
            return $"{count}";
        }

        private int GetBagCount(string bagKey)
        {
            if (BagCountCache.ContainsKey(bagKey)) return BagCountCache[bagKey];

            var count = 1;

            foreach (var b in _input[bagKey])
            {
                count += GetBagCount(b.BagKey) * b.Count;
            }

            BagCountCache[bagKey] = count;
            return count;
        }
    }
}
