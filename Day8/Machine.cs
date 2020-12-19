using System;
using System.Collections.Generic;
using System.Linq;

namespace Day8
{
    /// <summary>
    /// Simple virtual machine
    /// </summary>
    public class Machine
    {
        private readonly Dictionary<int, Instruction> instructions = new Dictionary<int, Instruction>();

        /// <summary>
        /// Gets the machine registers
        /// </summary>
        public Registers Registers
        {
            get;
        } = new Registers();

        /// <summary>
        /// Gets or sets a value that indicates the maximum times an instruction can be used
        /// </summary>
        public int MaxExecutions
        {
            get;
            set;
        } = 1;

        /// <summary>
        /// Gets or sets a value that indicates whether or not debug output should be printed
        /// </summary>
        public bool Debug
        {
            get;
            set;
        }

        /// <summary>
        /// Gets all of the instructions on the stack
        /// </summary>
        public Dictionary<int, Instruction> Instructions
        {
            get => this.instructions;
        }

        /// <summary>
        /// Resets machine state
        /// </summary>
        public void Reset()
        {
            this.instructions.Clear();
            this.Begin();
        }

        /// <summary>
        /// Resets stack pointer to the beginning and clears the accumulator
        /// </summary>
        public void Begin()
        {
            this.Registers.InstructionPtr = 0;
            this.Registers.Accumulator = 0;
            this.instructions.Values.ToList().ForEach(v => v.ExecutionCount = 0);
        }

        /// <summary>
        /// Adds an instruction to the end of the stack
        /// </summary>
        public void AddInstruction(string s)
        {
            instructions[this.Registers.InstructionPtr] = new Instruction(s);
            this.Registers.InstructionPtr++;
        }

        /// <summary>
        /// Adds an instruction to the end of the stack
        /// </summary>
        public void AddInstruction(string op, int value)
        {
            instructions[this.Registers.InstructionPtr] = new Instruction
            {
                Op = op,
                Value = value
            };
            this.Registers.InstructionPtr++;
        }

        /// <summary>
        /// Steps to the next instruction
        /// </summary>
        public bool Step()
        {
            return this.Interpret(this.instructions[this.Registers.InstructionPtr]);
        }

        /// <summary>
        /// Run from the beginning until end
        /// </summary>
        public bool Run()
        {
            return this.Run(true);
        }

        /// <summary>
        /// Run the program until it ends
        /// </summary>
        /// <param name="restart">If true, starts from beginning. If false runs from the current pointer.</param>
        /// <returns>True if program executed successfully, false if it looped</returns>
        public bool Run(bool restart)
        {
            if (restart == true)
            {
                this.Begin();
            }

            this.WriteDebug($"Starting from step {this.Registers.InstructionPtr}");

            while (this.Registers.InstructionPtr < this.instructions.Count)
            {
                if (this.Step() == true)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Writes debug text to console if <see cref="this.Debug"/>
        /// </summary>
        private void WriteDebug(string text)
        {
            if (this.Debug == false)
            {
                return;
            }

            Console.WriteLine($"[DEBUG] {text}");
        }

        /// <summary>
        /// Interprets the instruction
        /// </summary>
        /// <returns>True if exeuction is terminated, false if it can continue</returns>
        private bool Interpret(Instruction i)
        {
            this.WriteDebug($"[START] [{this.Registers}] Interpreting: {i} (original: {i.RawOriginalValue}) Instruction count: {i.ExecutionCount}");
            try
            {
                if (i.ExecutionCount == this.MaxExecutions)
                {
                    return true;
                }

                switch (i.Op)
                {
                    case "nop":
                        this.Registers.InstructionPtr++;
                        break;
                    case "jmp":
                        this.Registers.InstructionPtr += i.Value;
                        break;
                    case "acc":
                        this.Registers.Accumulator += i.Value;
                        this.Registers.InstructionPtr++;
                        break;
                    default:
                        throw new InvalidOperationException($"Unrecognized instruction {i.Op}");
                }

                i.ExecutionCount++;
                return false;
            }
            finally
            {
                this.WriteDebug($"[FINISH] [{this.Registers}] Interpreting: {i} (original: {i.RawOriginalValue}) Instruction count: {i.ExecutionCount}");
            }
        }
    }
}