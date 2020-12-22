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

        private const long Sample1SetSmallExpected1 = 7 * 5;
        private const long Sample1SetSmallExpected2 = 8;
        private const long Sample1SetLargeExpected1 = 22 * 10;
        private const long Sample1SetLargeExpected2 = 19208;
        private const int DeviceJoltage = 3;

        public static void Main(string[] args)
        {
            IEnumerable<string> data = Sample1SetSmall.Split('\n');
            Logging.WriteHeader("Sample 1 (Small) Goal 1");
            Assert.AreEqual(Sample1SetSmallExpected1, Goal1(data));

            Logging.WriteHeader("Sample 1 (Small) Goal 2");
            Assert.AreEqual(Sample1SetSmallExpected2, Goal2(data));

            data = Sample1SetLarge.Split('\n');
            Logging.WriteHeader("Sample 1 (Large)");
            Assert.AreEqual(Sample1SetLargeExpected1, Goal1(data));

            Logging.WriteHeader("Sample 2 (Small) Goal 2");
            Assert.AreEqual(Sample1SetLargeExpected2, Goal2(data));

            data = FileUtilities.GetFileContents("Day10Input.txt");
            Logging.WriteHeader("Goal 1");
            Console.WriteLine(Goal1(data));

            Logging.WriteHeader("Goal 2");
            Console.WriteLine(Goal2(data));
        }

        private static long Goal2(IEnumerable<string> data)
        {
            int[] jolts = new[] { 0 }.Concat(data.Select(int.Parse).OrderBy(j => j)).ToArray();

            // https://en.wikipedia.org/wiki/Memoization
            Dictionary<int, long> routes = new Dictionary<int, long>();
            routes.Add(jolts.Max() + DeviceJoltage, 1); // Device value (3 jolts)

            // Build a graph of all the possible paths in reverse
            foreach (int j in jolts.Reverse())
            {
                routes.TryGetValue(j + 1, out long c1);
                routes.TryGetValue(j + 2, out long c2);
                routes.TryGetValue(j + 3, out long c3);
                ////Console.WriteLine($"j:{j} c1:{c1} c2:{c2} c3:{c3} route:{c1 + c2 + c3}");
                routes[j] = c1 + c2 + c3;
            }

            // Path to 0 will be the value we want
            return routes[0];
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
                    n = jolts[i] + DeviceJoltage;
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

                long v = n - c;
                if (v > 3)
                {
                    return -1;
                }

                Upsert(counts, v);
            }

            Upsert(counts, jolts.Last()); // last one in the set
            Upsert(counts, DeviceJoltage); // built-in adapter

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