using System;
using System.Collections.Generic;
using System.IO;

namespace AoC2020Comon
{
    public static class FileUtilities
    {
        public static IEnumerable<string> GetFileContents(string fileName)
        {
            foreach (string s in File.ReadAllLines($"Content/{fileName}"))
            {
                yield return s;
            }
        }

    }
}
