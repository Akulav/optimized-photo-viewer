using OptimizedPhotoViewer.DataStructures;
using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace OptimizedPhotoViewer.Extensions
{
    public static class ImageLoader
    {
        public static void LoadImage(string path, Image pictureBox, TextBlock info)
        {
            if (info != null)
            {
                info.Text = Path.GetFileName(path);
                TempSettings.CurrentImage = path;
                DataProber.GetImages();
                DataProber.GetCurrentIndex();
            }

            // Check if the image is already in the cache
            if (ImageCache.imageCache.TryGetValue(path, out BitmapImage cachedImage))
            {
                pictureBox.Source = cachedImage;
                return;
            }

            try
            {
                byte[] imageData;
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    imageData = new byte[stream.Length];
                    stream.Read(imageData, 0, (int)stream.Length);
                }

                BitmapImage bitmapImage = CreateBitmapImageFromData(imageData);

                // Add the image to the cache
                if (TempSettings.settings.CacheLevel != 0)
                {
                    CacheImage(path, bitmapImage);
                }

                pictureBox.Source = bitmapImage;
            }
            catch (Exception ex)
            {
                // Handle or log the exception
            }
        }

        private static BitmapImage CreateBitmapImageFromData(byte[] imageData)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = new MemoryStream(imageData);
            bitmapImage.EndInit();
            return bitmapImage;
        }

        private static void CacheImage(string path, BitmapImage bitmapImage)
        {
            ImageCache.imageCache[path] = bitmapImage;
        }

    }
}
