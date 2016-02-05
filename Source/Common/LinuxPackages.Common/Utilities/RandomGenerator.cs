namespace LinuxPackages.Common.Utilities
{
    using System;
    using LinuxPackages.Common.Contracts;
    using System.Linq;

    public class RandomGenerator : IRandomGenerator
    {
        private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private readonly static Random random = new Random();

        public int GenerateRandomNumber(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue + 1);
        }

        public string GenerateRandomString(int length)
        {
            var chars = Enumerable
                .Repeat(RandomGenerator.Characters, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray();

            return new string(chars);
        }
    }
}
