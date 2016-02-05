namespace LinuxPackages.Common.Utilities
{
    using System.IO;

    public static class StreamHelper
    {
        public static byte[] ReadFully(Stream input, int length)
        {
            byte[] buffer = new byte[length];

            using (MemoryStream memoryStream = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    memoryStream.Write(buffer, 0, read);
                }

                return memoryStream.ToArray();
            }
        }
    }
}
