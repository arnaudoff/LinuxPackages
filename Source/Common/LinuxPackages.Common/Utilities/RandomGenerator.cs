namespace LinuxPackages.Common.Utilities
{
    using System;
    using System.Linq;
    using LinuxPackages.Common.Contracts;

    public class RandomGenerator : IRandomGenerator
    {
        private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly Random Random = new Random();

        public int GenerateRandomNumber(int minValue, int maxValue)
        {
            return Random.Next(minValue, maxValue + 1);
        }

        public string GenerateRandomString(int length)
        {
            var chars = Enumerable
                .Repeat(RandomGenerator.Characters, length)
                .Select(s => s[Random.Next(s.Length)])
                .ToArray();

            return new string(chars);
        }
    }
}