using OptimizedPhotoViewer.DataStructures;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;

namespace OptimizedPhotoViewer.Extensions
{
    public static class ImageHandler
    {
        public static Dictionary<string, BitmapImage> imageCache = new Dictionary<string, BitmapImage>();


        public static void RemoveEntry(string key)
        {
            if (imageCache.ContainsKey(key))
            {
                imageCache.Remove(key);
            }
        }

        public static void RotateOnDisk()
        {
            // Load the image from disk
            using (Bitmap originalImage = new Bitmap(TempSettings.CurrentImage))
            {
                // Rotate the image by 90 degrees
                originalImage.RotateFlip(RotateFlipType.Rotate90FlipNone);

                // Save the rotated image by replacing the old one
                using (var encoderParameters = new EncoderParameters(1))
                {
                    encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L); // Set the desired image quality (100% in this case)
                    originalImage.Save(TempSettings.CurrentImage, GetImageCodecInfo(ImageFormat.Jpeg), encoderParameters);
                }
            }


        }

        private static ImageCodecInfo GetImageCodecInfo(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public static void LoadImage(string path, Image pictureBox, Label info)
        {
            info.Content = Path.GetFileName(path);
            TempSettings.CurrentImage = path;
            GetImages();
            GetCurrentIndex();
            try
            {
                // Check if the image is already in the cache
                if (imageCache.ContainsKey(path))
                {
                    pictureBox.Source = imageCache[path];
                    return;
                }

                // Read the file into a byte array
                byte[] imageData;
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    imageData = new byte[stream.Length];
                    stream.Read(imageData, 0, (int)stream.Length);
                }

                // Create a BitmapImage and set the source using a MemoryStream
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = new MemoryStream(imageData);
                bitmapImage.EndInit();

                // Add the image to the cache
                imageCache[path] = bitmapImage;

                // Set the BitmapImage as the source of the Image control
                pictureBox.Source = bitmapImage;
            }
            catch (IOException ex)
            {
                // Handle file access error
            }
            
        }

        public static void quickLoadImage(string path, Image pictureBox)
        {
            try
            {
                // Check if the image is already in the cache
                if (imageCache.ContainsKey(path))
                {
                    pictureBox.Source = imageCache[path];
                    return;
                }

                // Read the file into a byte array
                byte[] imageData;
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    imageData = new byte[stream.Length];
                    stream.Read(imageData, 0, (int)stream.Length);
                }

                // Create a BitmapImage and set the source using a MemoryStream
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = new MemoryStream(imageData);
                bitmapImage.EndInit();

                // Add the image to the cache
                imageCache[path] = bitmapImage;

                // Set the BitmapImage as the source of the Image control
                pictureBox.Source = bitmapImage;
            }
            catch (IOException ex)
            {
                // Handle file access error
            }
        }


        public static void DeleteImages(Image pictureBox, Label info)
        {
            int imagesLength = TempSettings.AllPaths.Length;
            string newPath = TempSettings.AllPaths[(TempSettings.CurrentIndex + 1) % imagesLength];

            File.Delete(TempSettings.CurrentImage);
            LoadImage(newPath, pictureBox, info);

            if (imagesLength == 1)
            {
                Application.Current.Shutdown();
            }
            TempSettings.CurrentImage = newPath;
        }

        public static void Dispose(Image pictureBox, Grid grid)
        {
            var width = 1;
            var height = 1;

            var pixelFormat = PixelFormats.Bgra32;
            var stride = (width * pixelFormat.BitsPerPixel + 7) / 8;
            var pixels = new byte[height * stride];

            // Set the single pixel value to black
            int index = 0;
            pixels[index] = 0;       // Blue
            pixels[index + 1] = 0;   // Green
            pixels[index + 2] = 0;   // Red
            pixels[index + 3] = 255; // Alpha

            var bitmap = BitmapSource.Create(width, height, 96, 96, pixelFormat, null, pixels, stride);
            pictureBox.Source = null;

            for (int i = grid.Children.Count - 1; i >= 0; i--)
            {
                UIElement element = grid.Children[i];
                if (Grid.GetRow(element) == 2)
                {
                    grid.Children.Remove(element);
                }
            }


            GC.Collect();
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
            int numItems = Math.Min(count, 9);

            int start = TempSettings.CurrentIndex - (numItems / 2) + ((numItems + 1) % 2);
            for (int i = 0; i < numItems; i++)
            {
                int index = (start + i + count) % count;
                result.Add(TempSettings.AllPaths[index]);
            }

            return result;
        }
    }
}
