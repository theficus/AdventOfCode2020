using System;
namespace Aoc2020.Common
{
    public static class Assert
    {
        public static void AreEqual(object expected, object actual)
        {
            bool? isEqual = expected?.Equals(actual);
            Console.WriteLine($"[ASSERT] Check is {expected} [{expected?.GetType()}] == {actual} [{actual?.GetType()}]: {isEqual}");
            if (isEqual == false)
            {
                throw new InvalidOperationException($"AreEquals failed. Expected: {expected} Actual: {actual}");
            }
        }
    }
}