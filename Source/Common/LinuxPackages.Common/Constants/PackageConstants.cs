namespace LinuxPackages.Common.Constants
{
    public class PackageConstants
    {
        public const int MinNameLength = 5;
        public const int MaxNameLength = 100;

        public const int MinDescriptionLength = 100;
        public const int MaxDescriptionLength = 20000;

        public const int MinCommentContentLength = 20;
        public const int MaxCommentContentLength = 10000;

        public const int MinRating = 1;
        public const int MaxRating = 5;

        public const int MinRepositoryNameLength = 4;
        public const int MaxRepositoryNameLength = 20;

        public const string PackagesPath = "~/App_Data/packages";
        public const int PackagesPerDirectory = 1000;

        public const string ScreenshotsFolderName = "screenshots";
        public const int ScreenshotThumbnailHeight = 500;
        public const int ScreenshotThumbnailWidth = 500;
        public const string ScreenshotSizeUrlPattern = @"(\d+)x(\d+)";
    }
}