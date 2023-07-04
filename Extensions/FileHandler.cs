using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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
                .ToArray();
            Array.Sort(output);
            return output;
        }
    }
}
