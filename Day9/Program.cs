using System;
using System.Collections.Generic;
using System.Linq;
using Aoc2020.Common;

namespace Day9
{
    /// <summary>
    /// Day 9
    /// </summary>
    /// <remarks>
    /// https://adventofcode.com/2020/day/9
    /// </remarks>
    class Program
    {
        private const long Sample1Expected = 127;
        private const long Sample2Expected = 62;

        private const string SampleSet = @"35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576";

        public static void Main(string[] args)
        {
            IEnumerable<string> set = SampleSet.Split('\n');
            Logging.WriteHeader("Sample 1");
            long v = Validate1(5, set);
            Assert.AreEqual(Sample1Expected, v);

            Logging.WriteHeader("Sample 2");
            v = Validate2(set, v);
            Assert.AreEqual(Sample2Expected, v);

            set = FileUtilities.GetFileContents("Day9Input.txt");
            Logging.WriteHeader("Goal 1");
            v = Validate1(25, set);
            Console.WriteLine(v);

            Logging.WriteHeader("Goal 2");
            v = Validate2(set, v);
            Console.WriteLine(v);
        }

        private static long Validate2(IEnumerable<string> numbers, long target)
        {
            List<long> set = new List<long>();
            numbers.ToList().ForEach(v => set.Add(long.Parse(v)));

            // Get the target index based on the previously bad value
            int targetIdx = set.IndexOf(target);
            for (int i = 0; i < targetIdx; i++)
            {
                // Build a contiguous set and see if it adds up to the bad value
                for (int j = i + 1; j < targetIdx; j++)
                {
                    List<long> range = set.GetRange(i, j - i);
                    long sum = range.Sum(r => r);

                    // We've found our match. Get the min and max value in the range and that's our solution
                    if (sum == target)
                    {
                        return range.Min() + range.Max();
                    }
                }
            }

            return -1;
        }

        private static bool IsValidSet(long expected, long[] numbers)
        {
            if (numbers.Length == 0)
            {
                return true;
            }

            bool ok = false;
            for (int i = 0; i < numbers.Length && ok == false; i++)
            {
                for (int j = 0; j < numbers.Length && ok == false; j++)
                {
                    if (j == i)
                    {
                        continue;
                    }

                    if (numbers[i] + numbers[j] == expected)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static long Validate1(int preambleLength, IEnumerable<string> numbers)
        {
            List<long> set = new List<long>();
            numbers.ToList().ForEach(v => set.Add(long.Parse(v)));
            for (int i = preambleLength; i < set.Count; i++)
            {
                long expected = set[i];
                long[] sums = new long[preambleLength];
                for (int j = 0; j < preambleLength; j++)
                {
                    sums[j] = set[i - preambleLength + j];
                }

                if (IsValidSet(expected, sums) == false)
                {
                    return expected;
                }
            }

            return -1;
        }
    }
}