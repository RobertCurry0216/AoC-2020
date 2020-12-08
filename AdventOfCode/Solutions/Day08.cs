using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AdventOfCode.Computer;

namespace AdventOfCode
{
    class Day08 : BaseDay
    {
        private readonly string _input;

        public Day08()
        {
            _input = File.ReadAllText(InputFilePath);
        }

        public override string Solve_1()
        {
            var handheld = new HandHeld(_input);
            handheld.Run();

            return $"{handheld.Accumulator}";
        }

        public override string Solve_2()
        {
            HandHeld hh;
            int last = int.MaxValue;

            while (last > 0)
            {
                hh = new HandHeld(_input);
                for (var i = hh.Operations.Count - 1; i >=0; i--)
                {
                    if(i < last && 
                        (hh.Operations[i].Type == HandHeld.OperationType.jmp 
                        || hh.Operations[i].Type == HandHeld.OperationType.nop))
                    {
                        last = i;
                        hh.Operations[i].Type = hh.Operations[i].Type == HandHeld.OperationType.jmp
                            ? HandHeld.OperationType.nop
                            : HandHeld.OperationType.jmp;
                        break;
                    }
                }
                hh.Run();
                if (hh.Status == HandHeld.RunStatus.Done)
                {
                    return $"{hh.Accumulator}";
                }
            }

            return $"{-1}";
        }
    }
}
