
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.IO;
using System;

namespace optimizedPhotoViewer.Extensions
{
    public static class FileHandler
    {
        public static void DeleteCurrentImage(string[] imagePaths, int currentIndex, PictureBox pictureBox)
        {
            if (imagePaths == null || imagePaths.Length == 0)
                return;

            string currentImagePath = imagePaths[currentIndex];

            pictureBox.Image?.Dispose();
            pictureBox.Image = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();

            try
            {
                DeleteFile(currentImagePath);

                imagePaths = imagePaths.Where(path => path != currentImagePath).ToArray();
            }
            catch (IOException)
            {
                MessageBox.Show("The file is currently in use and cannot be deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }

        public static string[] LoadImagePaths(string filePath)
        {
            string directoryPath = Path.GetDirectoryName(filePath);

            string[] output = Directory.GetFiles(directoryPath, "*.jpeg")
                .Concat(Directory.GetFiles(directoryPath, "*.png"))
                .Concat(Directory.GetFiles(directoryPath, "*.jpg"))
                .Concat(Directory.GetFiles(directoryPath, "*.gif"))
                .Concat(Directory.GetFiles(directoryPath, "*.ico"))
                .ToArray();
            Array.Sort(output);
            return output;
        }

        public static void RotateImageClockwise(PictureBox pictureBox)
        {
            pictureBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureBox.Refresh();
        }

        public static void LoadNextImage(string[] imagePaths, ref int currentIndex, Action<string> loadImage, PictureBox pictureBox)
        {
            if (imagePaths == null || imagePaths.Length == 0)
                return;
            currentIndex++;
            currentIndex %= imagePaths.Length;
            loadImage(imagePaths[currentIndex]);
            UpdatePictureBox(imagePaths[currentIndex], pictureBox);
        }

        public static void LoadPreviousImage(string[] imagePaths, ref int currentIndex, Action<string> loadImage, PictureBox pictureBox)
        {
            if (imagePaths == null || imagePaths.Length == 0)
                return;
            currentIndex--;
            if (currentIndex < 0)
                currentIndex = imagePaths.Length - 1;
            loadImage(imagePaths[currentIndex]);
            UpdatePictureBox(imagePaths[currentIndex], pictureBox);
        }

        private static void UpdatePictureBox(string imagePath, PictureBox pictureBox)
        {
            pictureBox.Image?.Dispose();
            pictureBox.Image = new Bitmap(imagePath);
        }
    }
}