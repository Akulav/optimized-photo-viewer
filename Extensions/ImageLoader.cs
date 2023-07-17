using OptimizedPhotoViewer.DataStructures;
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

            try
            {
                // Check if the image is already in the cache
                if (ImageCache.imageCache.ContainsKey(path))
                {
                    pictureBox.Source = ImageCache.imageCache[path];
                    return;
                }

                BitmapImage bitmapImage = new BitmapImage();

                // Read the file into a byte array
                byte[] imageData;
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    imageData = new byte[stream.Length];
                    stream.Read(imageData, 0, (int)stream.Length);
                }

                // Create a BitmapImage and set the source using a MemoryStream

                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = new MemoryStream(imageData);
                bitmapImage.EndInit();

                // Add the image to the cache
                ImageCache.imageCache[path] = bitmapImage;

                // Set the BitmapImage as the source of the Image control
                pictureBox.Source = bitmapImage;
            }
            catch (IOException ex)
            {
                // Handle file access error
            }

        }
    }
}
