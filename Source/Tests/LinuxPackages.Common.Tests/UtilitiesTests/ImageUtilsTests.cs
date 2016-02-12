namespace LinuxPackages.Common.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Utilities;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using Constants;

    [TestClass]
    public class ImageUtilsTests
    {

        [TestMethod]
        public void ImageScaleShouldNotScaleWhenDimensionsEqualDefault()
        {
            Image sampleImage = new Bitmap(PackageConstants.ScreenshotThumbnailWidth, PackageConstants.ScreenshotThumbnailHeight);
            using (Graphics graph = Graphics.FromImage(sampleImage))
            {
                Rectangle rect = new Rectangle(0, 0, sampleImage.Width, sampleImage.Height);
                graph.FillRectangle(Brushes.White, rect);
            }

            var result = ImageUtils.ScaleImage(sampleImage, PackageConstants.ScreenshotThumbnailWidth, PackageConstants.ScreenshotThumbnailHeight);

            Assert.AreEqual(sampleImage.Width, result.Width);
            Assert.AreEqual(sampleImage.Height, result.Height);
        }

        [TestMethod]
        public void ImageScaleShouldNotScaleWhenBothDimensionsAreLessThanDefault()
        {
            Image sampleImage = new Bitmap(PackageConstants.ScreenshotThumbnailWidth - 50, PackageConstants.ScreenshotThumbnailHeight - 100);
            using (Graphics graph = Graphics.FromImage(sampleImage))
            {
                Rectangle rect = new Rectangle(0, 0, sampleImage.Width, sampleImage.Height);
                graph.FillRectangle(Brushes.White, rect);
            }

            var result = ImageUtils.ScaleImage(sampleImage, PackageConstants.ScreenshotThumbnailWidth, PackageConstants.ScreenshotThumbnailHeight);

            Assert.AreEqual(sampleImage.Width, result.Width);
            Assert.AreEqual(sampleImage.Height, result.Height);
        }

        [TestMethod]
        public void ImageScaleShouldScaleImagesCorrectly()
        {
            Image sampleImage = new Bitmap(600, 200);
            using (Graphics graph = Graphics.FromImage(sampleImage))
            {
                Rectangle rect = new Rectangle(0, 0, sampleImage.Width, sampleImage.Height);
                graph.FillRectangle(Brushes.White, rect);
            }

            var result = ImageUtils.ScaleImage(sampleImage, PackageConstants.ScreenshotThumbnailWidth, PackageConstants.ScreenshotThumbnailHeight);

            Assert.AreEqual(500, result.Width);
            Assert.AreEqual(166, result.Height);
        }
    }
}
