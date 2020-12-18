using System;
namespace Aoc2020.Common
{
    public static class Logging
    {
        /// <summary>
        /// Writes a simple header to the Console
        /// </summary>
        public static void WriteHeader(string text)
        {
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine(text.ToUpperInvariant());
            Console.WriteLine("------------------------------------------------------");
        }
    }
}
