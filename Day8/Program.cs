using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private const int SampleExpected = 5;

        public static void Main(string[] args)
        {
            int v = Run(SampleProgram.Split('\n').ToList());
            Logging.WriteHeader("Sample");
            Assert.AreEqual(SampleExpected, v);

            v = Run(FileUtilities.GetFileContents("Day8Input.txt"));
            Logging.WriteHeader("Goal 1");
            Console.WriteLine($"Goal 1: {v}");
        }

        private static int Run(IEnumerable<string> code)
        {
            Machine m = new Machine();
            code.ToList().ForEach(s => m.AddInstruction(s));
            m.Start();
            int lastValue = 0;
            do
            {
                lastValue = m.Accumulator;
                m.Step();
            }
            while (m.LastInstruction.ExecutionCount < 2);

            return lastValue;
        }
    }
}