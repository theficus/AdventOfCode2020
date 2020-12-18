using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using Aoc2020.Common;

namespace Day7
{
    /// <summary>
    /// Day 7
    /// </summary>
    /// <remarks>
    /// https://adventofcode.com/2020/day/7
    /// </remarks>
    class Program
    {
        // Bag object
        private sealed class Bag
        {
            public string Name
            {
                get; set;
            }

            public Collection<Bag> Contains
            {
                get;
            } = new Collection<Bag>();

            public int Count
            {
                get;
                set;
            }

            public override string ToString()
            {
                return $"{Name} count {Count} contains {Contains.Count}";
            }
        }

        // Goal 1
        // bright white contains gold
        // muted yellow contains gold
        // dark orange contains bright white contains gold
        // dark orange contains muted yellow contains gold
        // light red contains bright white contains gold
        private const string Sample1Input = @"light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags.";
        private const int Sample1Expected = 4;

        // Goal 2
        private const string Sample2Input = @"shiny gold bags contain 2 dark red bags.
dark red bags contain 2 dark orange bags.
dark orange bags contain 2 dark yellow bags.
dark yellow bags contain 2 dark green bags.
dark green bags contain 2 dark blue bags.
dark blue bags contain 2 dark violet bags.
dark violet bags contain no other bags.";
        private const int Sample2Expected = 126;

        private const string BagType = "shiny gold";

        public static void Main(string[] args)
        {
            // Sample 1 
            Logging.WriteHeader("Sample 1");
            List<Bag> bags = new List<Bag>();
            Sample1Input.Split('\n').ToList().ForEach(s => TokenizeBag(s, bags));
            List<string> containers = new List<string>();
            FindBags(BagType, bags, containers);
            Console.WriteLine($"Count: {containers.Count}");
            if (containers.Count != Sample1Expected)
            {
                throw new InvalidOperationException($"Invalid result for sample set. Expected: {Sample1Expected} Actual: {containers.Count}");
            }

            // Sample 2
            Logging.WriteHeader("Sample 2");
            bags.Clear();
            Sample2Input.Split('\n').ToList().ForEach(s => TokenizeBag(s, bags));
            Bag root = bags.First(b => b.Name == BagType);
            int count = CountBags(BagType, bags, root);
            Console.WriteLine($"Count: {count}");
            if (count != Sample2Expected)
            {
                throw new InvalidOperationException($"Invalid result for sample set. Expected: {Sample2Expected} Actual: {count}");
            }

            // Goal 1
            Logging.WriteHeader("Goal 1");
            containers.Clear();
            bags.Clear();
            FileUtilities.GetFileContents("Day7Input.txt").ToList().ForEach(s => TokenizeBag(s, bags));
            FindBags(BagType, bags, containers);
            Console.WriteLine($"Goal1: {containers.Count}");

            // Goal2
            Logging.WriteHeader("Goal 2");
            count = 0;
            root = bags.First(b => b.Name == BagType);
            count = CountBags(BagType, bags, root);
            Console.WriteLine($"Count: {count}");
        }

        private static int CountBags(string type, List<Bag> bags, Bag root)
        {
            int c = 0;
            foreach (Bag b in bags)
            {
                if (b.Name == type)
                {
                    foreach (Bag bb in b.Contains)
                    {
                        c += bb.Count;
                        c += CountBags(bb.Name, bags, root) * bb.Count;
                        ////Console.WriteLine($"Counting {b.Name} contains {bb.Name} count: {c}");
                    }
                }

                if (b == root)
                {
                    continue;
                }
            }

            return c;
        }

        private static void FindBags(string type, List<Bag> bags, List<string> containers)
        {
            foreach (Bag b in bags)
            {
                if (b.Name == type)
                {
                    continue;
                }

                foreach (Bag bb in b.Contains)
                {
                    if (bb.Name == type)
                    {
                        ////Console.WriteLine($"p: {b.Name} c: {bb.Name}");
                        if (containers.Contains(b.Name) == true)
                        {
                            continue;
                        }
                        else
                        {
                            containers.Add(b.Name);
                        }

                        FindBags(b.Name, bags, containers);
                    }
                }
            }
        }

        private static void TokenizeBag(string token, List<Bag> map)
        {
            const string containText = " bags contain ";
            int containIdx = token.IndexOf(containText);
            string bagType = token.Substring(0, containIdx);

            Bag bag = new Bag { Name = bagType };

            token.Substring(containIdx + containText.Length).Split(',').ToList().ForEach(b =>
            {
                if (b == "no other bags.")
                {
                    return;
                }

                MatchCollection parts = Regex.Matches(b, @"(\d+)\s+(.*)bag");
                int count = int.Parse(parts[0].Groups[1].Value);
                string name = parts[0].Groups[2].Value.Trim();
                Bag newBag = new Bag
                {
                    Name = name,
                    Count = count
                };
                bag.Contains.Add(newBag);
            });

            map.Add(bag);
        }
    }
}