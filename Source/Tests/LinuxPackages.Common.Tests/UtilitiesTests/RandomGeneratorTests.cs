namespace LinuxPackages.Common.Tests.UtilitiesTests
{
    using Utilities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RandomGeneratorTests
    {
        [TestMethod]
        public void RandomGeneratorShouldGenerateNumbersInRangeInclusive()
        {
            var random = new RandomGenerator();

            for (int i = 0; i < 50; i++)
            {
                int number = random.GenerateRandomNumber(1, 3);

                Assert.IsTrue(number >= 1 && number <= 3);
            }
        }

        [TestMethod]
        public void RandomGeneratorShouldGenerateStringWithExactlySpecifiedLength()
        {
            var random = new RandomGenerator();

            for (int i = 0; i <= 50; i++)
            {
                string result = random.GenerateRandomString(i);

                Assert.AreEqual(result.Length, i);
            }
        }
    }
}
