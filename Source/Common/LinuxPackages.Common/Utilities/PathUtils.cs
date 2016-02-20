namespace LinuxPackages.Common.Utilities
{
    using System.IO;
    using System.Linq;

    public static class PathUtils
    {
        public static string CleanFileName(string fileName)
        {
            return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
        }
    }
}
