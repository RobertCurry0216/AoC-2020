using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Computer
{
    public class HandHeld
    {

        #region helpers

        public enum RunStatus
        {
            Idle,
            Running,
            Done,
            InfiniteLoop
        }

        public enum OperationType
        {
            nop,
            acc,
            jmp
        }

        public class Operation
        {
            public OperationType Type;
            public int Argument;

            public Operation()
            {

            }
        }
        #endregion

        #region Properties

        public int Accumulator { get; set; } = 0;
        public int InstructionPointer { get; set; } = 0;
        public List<Operation> Operations { get; set; } = new List<Operation>();
        public List<int> Executed { get; set; } = new List<int>();
        public bool Done { get => InstructionPointer >= Operations.Count; }
        public RunStatus Status { get; private set; } = RunStatus.Idle;

        private string Input;

        #endregion

        #region Constructor
        public HandHeld(string input)
        {
            Input = input;
            ParseInput();
        }

        private void ParseInput()
        {
            var re = new Regex(@"(?<op>\w\w\w) (?<arg>[+-]\d+)");
            Input.Split('\n').ToList().ForEach(line =>
            {
                var match = re.Match(line);
                var op = new Operation()
                {
                    Type = (OperationType)Enum.Parse(typeof(OperationType), match.Groups["op"].Value, true),
                    Argument = int.Parse(match.Groups["arg"].Value)
                };

                Operations.Add(op);
            });
        }

        #endregion

        #region Methods

        public void Run()
        {
            Status = RunStatus.Running;

            while (!Executed.Contains(InstructionPointer))
            {
                Executed.Add(InstructionPointer);
                NextOp();
                if (InstructionPointer >= Operations.Count)
                {
                    Status = RunStatus.Done;
                    return;
                }
            }
            Status = RunStatus.InfiniteLoop;
        }

        public void NextOp()
        {
            var op = Operations[InstructionPointer];

            switch (op.Type)
            {
                case OperationType.nop:
                    Nop();
                    break;
                case OperationType.acc:
                    Acc(op.Argument);
                    break;
                case OperationType.jmp:
                    Jmp(op.Argument);
                    break;
                default:
                    throw new Exception("Unknown Operation");
            }
            InstructionPointer++;
        }

        #endregion

        #region Operation Methods

        private void Nop()
        {

        }

        private void Acc(int arg)
        {
            Accumulator += arg;
        }

        private void Jmp(int arg)
        {
            InstructionPointer += arg - 1; 
        }

        #endregion

    }
}
