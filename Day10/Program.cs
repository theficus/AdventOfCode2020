using System;
using System.Collections.Generic;
using System.Linq;
using Aoc2020.Common;

namespace Day10
{
    /// <summary>
    /// Day 10
    /// </summary>
    /// <remarks>
    /// https://adventofcode.com/2020/day/10
    /// </remarks>
    class Program
    {
        private const string Sample1SetSmall = @"16
10
15
5
1
11
7
19
6
12
4";

        private const string Sample1SetLarge = @"28
33
18
42
31
14
46
20
48
47
24
23
49
45
19
38
39
11
1
32
25
35
8
17
7
9
4
2
34
10
3";

        private const long Sample1SetSmallExpected = 7 * 5;
        private const long Sample1SetLargeExpected = 22 * 10;
        private const long BuiltInJoltage = 3;

        public static void Main(string[] args)
        {
            Logging.WriteHeader("Sample 1 (Small)");
            Assert.AreEqual(Sample1SetSmallExpected, Goal1(Sample1SetSmall.Split('\n')));
            Logging.WriteHeader("Sample 1 (Large)");
            Assert.AreEqual(Sample1SetLargeExpected, Goal1(Sample1SetLarge.Split('\n')));

            Logging.WriteHeader("Goal 1");
            Console.WriteLine(Goal1(FileUtilities.GetFileContents("Day10Input.txt")));
        }

        private static long Goal1(IEnumerable<string> data)
        {
            List<long> jolts = data.ToList().ConvertAll<long>(v => long.Parse(v));
            jolts.Sort();
            Dictionary<long, long> counts = new Dictionary<long, long>();
            for (int i = 0; i < jolts.Count - 1; i++)
            {
                if (i == 0)
                {
                    Upsert(counts, jolts[i]);
                }

                long c = jolts[i];

                long n;
                if (i + 1 >= jolts.Count)
                {
                    n = jolts[i] + BuiltInJoltage;
                }
                else
                {
                    n = jolts[i + 1];
                }

                // Ignore duplicates
                if (c == n)
                {
                    continue;
                }

                Upsert(counts, n - c);
            }

            Upsert(counts, jolts.Last()); // last one in the set
            Upsert(counts, BuiltInJoltage); // built-in adapter

            long sum = 1;
            foreach (long k in counts.Values)
            {
                sum *= k;
            }

            return sum;
        }

        private static void Upsert(Dictionary<long, long> counts, long difference)
        {
            if (counts.ContainsKey(difference) == false)
            {
                counts[difference] = 0;
            }

            counts[difference]++;
        }
    }
}