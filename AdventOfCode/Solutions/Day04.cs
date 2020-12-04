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
    class Day04 : BaseDay
    {
        private readonly List<Dictionary<string,string>> _input;

        public Day04()
        {
            _input = new List<Dictionary<string, string>>();
            var passport = new Dictionary<string, string>();
            var re = new Regex(@"(?<key>\S+):(?<value>\S+)");
            File.ReadAllLines(InputFilePath).ToList().ForEach(l => {
                var m = re.Matches(l);
                if (m.Count == 0)
                {
                    _input.Add(passport);
                    passport = new Dictionary<string, string>();
                }
                else
                {
                    foreach (Match pair in m)
                    {
                        passport.Add(pair.Groups["key"].Value, pair.Groups["value"].Value);
                    }
                }
            });
            _input.Add(passport);
        }

        public override string Solve_1()
        {
            var count = 0;
            _input.ForEach(passport =>
            {
                if (passport.Count == 8 || (passport.Count == 7 && !passport.ContainsKey("cid"))) count++;
            });
            return $"{count}";
        }

        public override string Solve_2()
        {
            var count = 0;

            var reHgt = new Regex(@"(\d+)(cm|in)");
            var reHcl = new Regex(@"#[0-9a-f]{6}");
            var rePid = new Regex(@"^[0-9]{9}$");
            var ecl = new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            _input.ForEach(passport =>
            {
                if (passport.Count == 8 || (passport.Count == 7 && !passport.ContainsKey("cid")))
                {
                    int byr, iyr, eyr, hgt;
                    if (!(int.TryParse(passport["byr"], out byr) && 1920 <= byr && byr <= 2002)) return;
                    if (!(int.TryParse(passport["iyr"], out iyr) && 2010 <= iyr && iyr <= 2020)) return;
                    if (!(int.TryParse(passport["eyr"], out eyr) && 2020 <= eyr && eyr <= 2030)) return;

                    var hgtMatch = reHgt.Match(passport["hgt"]);
                    if (hgtMatch.Success)
                    {
                        if (hgtMatch.Groups[2].Value == "cm")
                        {
                            if (!(int.TryParse(hgtMatch.Groups[1].Value, out hgt) && 150 <= hgt && hgt <= 193)) return;
                        }
                        else
                        {
                            if (!(int.TryParse(hgtMatch.Groups[1].Value, out hgt) && 59 <= hgt && hgt <= 76)) return;
                        }
                    }
                    else
                    {
                        return;
                    }

                    if (!reHcl.Match(passport["hcl"]).Success) return;
                    if (!ecl.Contains(passport["ecl"])) return;
                    if (!rePid.Match(passport["pid"]).Success) return;

                    count++;
                }
            });
            return $"{count}";
        }
    }
}
