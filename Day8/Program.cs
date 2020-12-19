using System;
using System.Collections.Generic;
using System.Linq;
using Aoc2020.Common;

namespace Day8
{
    /// <summary>
    /// Day 8
    /// </summary>
    /// <remarks>
    /// https://adventofcode.com/2020/day/8
    /// </remarks>
    class Program
    {
        private const string SampleProgram = @"nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6";

        private const int Sample1Expected = 5;
        private const int Sample2Expected = 8;

        public static void Main(string[] args)
        {
            Logging.WriteHeader("Sample 1");
            Machine m = SetupMachine(SampleProgram.Split('\n').ToList());
            m.Debug = true;
            m.Run();
            Assert.AreEqual(Sample1Expected, m.Registers.Accumulator);

            Logging.WriteHeader("Sample 2");
            m.Instructions[m.Instructions.Count - 2].Op = "nop";
            m.Run();
            Assert.AreEqual(Sample2Expected, m.Registers.Accumulator);

            Logging.WriteHeader("Goal 1");
            FileUtilities.GetFileContents("Day8Input.txt");
            m = SetupMachine(FileUtilities.GetFileContents("Day8Input.txt"));
            m.Run();
            Console.WriteLine($"Goal 1: {m.Registers.Accumulator}");

            Logging.WriteHeader("Goal 2");
            for (int i = 0; i < m.Instructions.Count; i++)
            {
                Instruction oi = m.Instructions[i];
                Instruction ni = null;
                if (oi.Op == "nop")
                {
                    ni = (Instruction)oi.Clone();
                    ni.Op = "jmp";
                }
                else if (oi.Op == "jmp")
                {
                    ni = (Instruction)oi.Clone();
                    ni.Op = "nop";
                }
                else
                {
                    continue;
                }

                try
                {
                    m.Instructions[i] = ni;

                    if (m.Run() == true)
                    {
                        Console.WriteLine($"Goal 2: {m.Registers.Accumulator}");
                        break;
                    }
                }
                finally
                {
                    // Change back
                    m.Instructions[i] = oi;
                }
            }
        }

        private static Machine SetupMachine(IEnumerable<string> code)
        {
            Machine m = new Machine();
            code.ToList().ForEach(s => m.AddInstruction(s));
            return m;
        }
    }
}