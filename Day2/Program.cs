using System;
using System.Collections.Generic;
using Aoc2020.Common;

namespace Day2
{
    class Program
    {
        public static void Main(string[] args)
        {
            var r = Goal();
            Console.WriteLine($"Goal1: {r.Item1}");
            Console.WriteLine($"Goal2: {r.Item2}");
        }

        private static Tuple<int, int> Goal()
        {

            int valid1 = 0;
            int valid2 = 0;
            foreach (string s in FileUtilities.GetFileContents("Day2Input.txt"))
            {
                PasswordEntry pe = new PasswordEntry(s);
                if (pe.IsValidGoal1 == true)
                {
                    valid1++;
                }

                if (pe.IsValidGoal2 == true)
                {
                    valid2++;
                }
            }

            return new Tuple<int, int>(valid1, valid2);
        }


    }
}
