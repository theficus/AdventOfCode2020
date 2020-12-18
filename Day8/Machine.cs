using System;
using System.Collections.Generic;

namespace Day8
{
    public class Machine
    {
        private readonly Dictionary<int, Instruction> instructions = new Dictionary<int, Instruction>();
        private int ptr;

        public int Accumulator
        {
            get;
            private set;
        }

        public Instruction LastInstruction
        {
            get;
            private set;
        }

        public void Reset()
        {
            this.instructions.Clear();
            this.LastInstruction = null;
            this.ptr = 0;
        }

        public void Start()
        {
            this.ptr = 0;
        }

        public void AddInstruction(string s)
        {
            instructions[ptr] = new Instruction(s);
            ptr++;
        }

        public void AddInstruction(string op, int value)
        {
            instructions[ptr] = new Instruction
            {
                Op = op,
                Value = value
            };
            ptr++;
        }

        public int Step()
        {
            Console.WriteLine($"Executing instruction {this.ptr}");
            this.Interpret(this.instructions[this.ptr]);
            return this.ptr;
        }

        public void Run()
        {
            while (this.ptr < this.instructions.Count)
            {
                this.Step();
            }
        }

        private void Interpret(Instruction i)
        {
            i.ExecutionCount++;
            Console.WriteLine($"[{this.ptr}] Interpreting: {i.ToString()} Execution count: {i.ExecutionCount}");
            switch (i.Op)
            {
                case "nop":
                    Console.WriteLine($"Nop");
                    ptr++;
                    break;
                case "jmp":
                    Console.WriteLine($"Moving ptr from {ptr} to {ptr + i.Value}");
                    ptr += i.Value;
                    break;
                case "acc":
                    Console.WriteLine($"Accumulator {this.Accumulator} + {i.Value} = {this.Accumulator + i.Value}");
                    this.Accumulator += i.Value;
                    ptr++;
                    break;
                default:
                    throw new InvalidOperationException($"Unrecognized instruction {i.Op}");
            }

            this.LastInstruction = i;
        }
    }
}
