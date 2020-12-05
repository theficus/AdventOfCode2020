using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Aoc2020.Common
{
    public static class FileUtilities
    {
        public static IEnumerable<string> GetFileContents(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) == true)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            fileName = $"Content/{fileName}";
            Trace.TraceInformation($"Opening file {fileName}");

            int i = 0;
            foreach (string s in File.ReadAllLines(fileName))
            {
                ++i;
                yield return s;
            }

            Trace.TraceInformation($"Read {i} lines from file {fileName}");
        }

    }
}
