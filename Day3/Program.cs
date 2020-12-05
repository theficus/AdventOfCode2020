using System;
using System.Collections;
using System.Collections.Generic;
using Aoc2020.Common;

namespace Day3
{
    class MainClass
    {
        private static List<string> map = new List<string>();
        public static void Main(string[] args)
        {
            // TODO: Handle rotating back to 0 after out of bounds
            foreach (string s in FileUtilities.GetFileContents("Day3Input.txt"))
            {
                map.Add(s);
            }

            // Right 3 down 1
            int rightPos = 3;
            int downPos = 1;
            int hits = 0;
            for (int i = 0; i < map.Count; i++)
            {
                if (i < downPos)
                {
                    continue;
                }

                char c = map[downPos][rightPos];
                if (c == '#')
                {
                    hits++;
                }

                rightPos += rightPos;
                downPos += downPos;
            }

            Console.WriteLine(hits);
        }
    }
}
