namespace LinuxPackages.Common.Tests.UtilitiesTests
{
    using System;
    using System.IO;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Utilities;
    using System.Runtime.InteropServices;

    [TestClass]
    public class StreamHelperTests
    {
        // P/Invoke is hundred times faster than LINQ-based solutions
        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern int memcmp(byte[] b1, byte[] b2, long count);

        [TestMethod]
        public void ReadFullyShouldReadTheStreamCorrectly()
        {
            var expected = new byte[8];
            var random = new Random();
            random.NextBytes(expected);
            var stream = new MemoryStream(expected);

            byte[] result = StreamHelper.ReadFully(stream, expected.Length);

            Assert.IsTrue(CompareByteArrays(expected, result));
        }

        private static bool CompareByteArrays(byte[] b1, byte[] b2)
        { 
            return b1.Length == b2.Length && memcmp(b1, b2, b1.Length) == 0;
        }
    }
}
