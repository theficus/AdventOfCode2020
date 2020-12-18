using System;
namespace Aoc2020.Common
{
    public static class Assert
    {
        public static void AreEqual(object expected, object actual)
        {
            if (expected?.Equals(actual) == false)
            {
                throw new InvalidOperationException($"AreEquals failed. Expected: {expected} Actual: {actual}");
            }
        }
    }
}