using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Aoc2020.Common;

namespace Day3
{
    // https://adventofcode.com/2020/day/3
    public static class MainClass
    {
        private static List<string> map = new List<string>();
        public static void Main(string[] args)
        {
            foreach (string s in FileUtilities.GetFileContents("Day3Input.txt"))
            {
                map.Add(s);
            }

            int goal1; // We'll need this for Goal2 so don't waste time recalculating
            Console.WriteLine($"Goal1: {goal1 = Goal1()}");
            Console.WriteLine($"Goal2: {Goal2(goal1)}");
        }

        /// <summary>
        /// Goal 1
        /// </summary>
        /// <remarks>
        /// The toboggan can only follow a few specific slopes (you opted for a cheaper model that prefers rational numbers);
        /// start by counting all the trees you would encounter for the slope right 3, down 1
        /// </remarks>
        private static int Goal1()
        {
            // Right 3 down 1
            return GetHits(3, 1);
        }

        /// <summary>
        /// Goal 2
        /// </summary>
        /// <remarks>
        /// Determine the number of trees you would encounter if, for each of the following slopes, you start at the top-left corner and traverse the map all the way to the bottom:
        /// - Right 1, down 1.
        /// - Right 3, down 1. (This is the slope you already checked.)
        /// - Right 5, down 1.
        /// - Right 7, down 1.
        /// - Right 1, down 2.
        /// </remarks>
        private static int Goal2(int goal1)
        {
            int hits = GetHits(1, 1);
            hits *= goal1;
            hits *= GetHits(5, 1);
            hits *= GetHits(7, 1);
            hits *= GetHits(1, 2);
            return hits;
        }

        private static int GetHits(int rightPos, int downPos)
        {
            int wrapPos = map[0].Length;
            int hits = 0;
            int rightStep = rightPos;
            int downStep = downPos;
            for (int i = 0; i < map.Count; i++)
            {
                Debug.WriteLine($"Index: {i} RightPos: {rightPos} DownPos: {downPos} Tile: {map[downPos][rightPos]}");
                if (i < downStep)
                {
                    continue;
                }

                char c = map[downPos][rightPos];
                if (c == '#')
                {
                    hits++;
                }

                rightPos += rightStep;
                downPos += downStep;

                // If we're out of bounds, wrap back to the other side
                if (rightPos >= wrapPos)
                {
                    rightPos -= wrapPos;
                }

                // If we go out of bounds, just bail
                if (downPos >= map.Count)
                {
                    break;
                }
            }

            return hits;
        }
    }
}