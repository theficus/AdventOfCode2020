using System;
using System.Collections.Generic;
using Aoc2020.Common;

/// <summary>
/// Not an elegant solution but a working one.
/// </summary>
namespace Day1
{
    // https://adventofcode.com/2020/day/1
    public static class Program
    {
        private static List<int> numbers = new List<int>();

        public static void Main(string[] args)
        {
            foreach (string s in FileUtilities.GetFileContents("Day1Input.txt"))
            {
                numbers.Add(int.Parse(s));
            }

            Console.WriteLine($"Goal1: {Goal1()}");
            Console.WriteLine($"Goal2: {Goal2()}");
        }

        // Goal1: Find two entries that sum to 2020
        public static int Goal1()
        {
            int total = numbers.Count;
            for (int i = 0; i < total; i++)
            {
                int num1 = numbers[i];
                for (int j = 0; j < total; j++)
                {
                    if (j == i)
                    {
                        continue;
                    }

                    int num2 = numbers[j];
                    if ((num1 + num2) == 2020)
                    {
                        return num1 * num2;
                    }
                }
            }

            throw new InvalidOperationException("Could not find matching sequence.");
        }

        // Goal2: Find three entries that sum to 2020
        public static int Goal2()
        {
            int total = numbers.Count;
            for (int i = 0; i < total; i++)
            {
                int num1 = numbers[i];
                for (int j = 0; j < total; j++)
                {
                    if (j == i)
                    {
                        continue;
                    }

                    int num2 = numbers[j];

                    for (int k = 0; k < total; k++)
                    {
                        int num3 = numbers[k];

                        if (k == j || k == i)
                        {
                            continue;
                        }

                        if ((num1 + num2 + num3) == 2020)
                        {
                            return num1 * num2 * num3;
                        }
                    }
                }
            }

            throw new InvalidOperationException("Could not find matching sequence.");
        }
    }
}
