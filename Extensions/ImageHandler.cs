using OptimizedPhotoViewer.DataStructures;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace OptimizedPhotoViewer.Extensions
{
    public static class ImageHandler
    {
        public static void LoadImage(string path, Image pictureBox, Label info)
        {
            info.Content = Path.GetFileName(path);
            TempSettings.CurrentImage = path;
            pictureBox.Source = new BitmapImage(new Uri(path));
            GetImages();
            GetCurrentIndex();
        }
        public static void GetImages()
        {
            string directoryPath = Path.GetDirectoryName(TempSettings.DefaultPath);
            string[] imageExtensions = { ".png", ".jpg", ".jpeg", ".bmp", ".ico", ".tiff" };

            HashSet<string> imageExtensionsSet = new(imageExtensions, StringComparer.OrdinalIgnoreCase);
            ConcurrentBag<string> filesBag = new();

            Parallel.ForEach(Directory.EnumerateFiles(directoryPath), file =>
            {
                if (imageExtensionsSet.Contains(Path.GetExtension(file)))
                {
                    filesBag.Add(file);
                }
            });
            string[] sorted = filesBag.OrderBy(file => file).ToArray();
            TempSettings.AllPaths = sorted;
        }

        public static void GetCurrentIndex()
        {
            TempSettings.CurrentIndex = Array.IndexOf(TempSettings.AllPaths, TempSettings.CurrentImage);
        }

        public static List<string> GetStringsInRange()
        {
            List<string> result = new();
            int count = TempSettings.AllPaths.Length;
            int numItems = Math.Min(count, 5);

            int start = TempSettings.CurrentIndex - (numItems / 2) + ((numItems + 1) % 2);
            for (int i = 0; i < numItems; i++)
            {
                int index = (start + i + count) % count;
                result.Add(TempSettings.AllPaths[index]);
            }

            return result;
        }

        public static ConcurrentDictionary<string, System.Drawing.Size> GetImageDimensions(List<string> imagePaths)
        {
            ConcurrentDictionary<string, Size> imageDimensions = new ConcurrentDictionary<string, System.Drawing.Size>();

            Parallel.ForEach(imagePaths, imagePath =>
            {
                BitmapImage originalImage = new BitmapImage();
                originalImage.BeginInit();
                originalImage.CacheOption = BitmapCacheOption.OnLoad;
                originalImage.UriSource = new System.Uri(imagePath);
                originalImage.EndInit();
                originalImage.Freeze();

                System.Drawing.Size imageSize = new System.Drawing.Size(originalImage.PixelWidth, originalImage.PixelHeight);
                imageDimensions.TryAdd(imagePath, imageSize);
            });


            return imageDimensions;
        }
    }
}
