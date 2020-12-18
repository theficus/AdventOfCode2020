using System;
using System.Collections.Generic;
using System.Linq;
using Aoc2020.Common;

namespace Day5
{
    // https://adventofcode.com/2020/day/5
    class Program
    {
        public static void Main(string[] args)
        {
            List<int> ids = new List<int>();
            int max = 0;
            foreach (string s in FileUtilities.GetFileContents("Day5Input.txt"))
            {
                int id = GetSeatId(s);
                if (id > max)
                {
                    max = id;
                }

                ids.Add(id);
            }

            Console.WriteLine($"Goal 1: {max}");
            Console.WriteLine($"Goal 2: {Goal2(ids)}");
        }

        private static int Goal2(List<int> ids)
        {
            int[] ordered = ids.OrderBy(i => i).ToArray();
            for (int i = 1; i < ordered.Length - 1; i++)
            {
                if (ordered[i + 1] - 1 != ordered[i])
                {
                    return ordered[i] + 1;
                }
            }

            return -1;
        }

        public static int GetSeatId(string input)
        {
            // Convert the text to binary
            char[] tokens = input.ToCharArray();
            for (int i = 0; i < tokens.Length; i++)
            {
                switch (tokens[i])
                {
                    case 'L':
                    case 'F':
                        tokens[i] = '0';
                        break;
                    case 'R':
                    case 'B':
                        tokens[i] = '1';
                        break;
                }
            }

            string binaryInput = new string(tokens);
            int bin = Convert.ToInt32(binaryInput, 2);
            //Console.WriteLine($"{input} => {binaryInput} => {bin} (0x{bin:x8})");
            return bin;
        }
    }
}