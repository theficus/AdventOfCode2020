using System;
using System.Collections.Generic;
using System.Linq;
using Aoc2020.Common;

namespace Day4
{
    // https://adventofcode.com/2020/day/4
    public static class Program
    {
        public static void Main(string[] args)
        {
            List<string> data = FileUtilities.GetFileContents("Day4Input.txt").ToList();

            Console.WriteLine($"Goal1: {Goal(data, false)}");
            Console.WriteLine($"Goal2: {Goal(data, true)}");
        }

        private static int Goal(List<string> data, bool strict)
        {
            int index = 0;
            int valid = 0;
            while (index < data.Count)
            {
                Passport p = new Passport(data, ref index, strict);
                if (p.IsValid == true)
                {
                    valid++;
                }
            }

            return valid;
        }
    }
}
