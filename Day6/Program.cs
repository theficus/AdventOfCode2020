using System;
using System.Collections.Generic;
using System.Linq;
using Aoc2020.Common;

namespace Day6
{
    // https://adventofcode.com/2020/day/6
    class Program
    {
        public static void Main(string[] args)
        {
            UnitTest();
            List<string> inputs = FileUtilities.GetFileContents("Day6Input.txt").ToList();
            Console.WriteLine($"Goal1: {Goal1(inputs)}");
            Console.WriteLine($"Goal2: {Goal2(inputs)}");
        }

        /// <summary>
        /// Qualify Goal 1 by testing the sample input from the site
        /// </summary>
        private static void UnitTest()
        {
            // Example
            const string TestText = @"abc

a
b
c

ab
ac

a
a
a
a

b";

            int sample = Goal1(TestText.Split('\n'));
            if (sample != 11)
            {
                throw new InvalidOperationException($"Expected 11, got {sample}");
            }

            sample = Goal2(TestText.Split('\n'));
            if (sample != 6)
            {
                throw new InvalidOperationException($"Expected 6, got {sample}");
            }
        }

        private static int Goal1(IList<string> inputs)
        {
            int totalSum = 0;
            List<char> answers = new List<char>();
            foreach (string s in inputs)
            {
                if (string.IsNullOrWhiteSpace(s) == true)
                {
                    totalSum += CountResults1(answers);
                }
                else
                {
                    answers.AddRange(s.ToCharArray());
                }
            }

            // Count the final set
            totalSum += CountResults1(answers);

            return totalSum;
        }

        private static int Goal2(IList<string> inputs)
        {
            int totalSum = 0;
            Dictionary<char, int> answers = new Dictionary<char, int>();
            int responses = 0;
            foreach (string s in inputs)
            {
                if (string.IsNullOrWhiteSpace(s) == true)
                {
                    totalSum += CountResults2(answers, responses);
                    responses = 0;
                }
                else
                {
                    foreach (char c in s.ToCharArray())
                    {
                        answers.TryGetValue(c, out int v);
                        v++;
                        answers[c] = v;
                    }

                    responses++;
                }
            }

            totalSum += CountResults2(answers, responses);
            return totalSum;
        }

        private static int CountResults2(Dictionary<char, int> answers, int responses)
        {
            ////Console.WriteLine($"Responses: {responses} All Values: {string.Join(" ", answers)}");
            ////Console.WriteLine($"Responses: {responses} Matched Values: {string.Join(" ", answers.Where(a => a.Value == responses))}");
            int totalSum = answers.Where(a => a.Value == responses).Count();
            answers.Clear();
            return totalSum;
        }

        private static int CountResults1(IList<char> answers)
        {
            char[] chars = answers.ToArray();
            ////Console.WriteLine($"Group: {new string(chars)} count: {chars.Length} Distinct group: {new string(chars.Distinct().ToArray())} count: {chars.Distinct().Count()}");
            answers.Clear();

            return chars
                .Where(c => c != ' ' && c != '\n') // Doesn't seem to be a factor with the data set
                .Distinct() // Discard duplicates
                .Count();
        }
    }
}