namespace optimizedPhotoViewer.Extensions
{
    public static class ImageHandler
    {
        public static string[] getImages(string path)
        {
            string directoryPath = Path.GetDirectoryName(path);
            string[] imageExtensions = { ".png", ".jpg" };
            return Directory.GetFiles(directoryPath)
                .Where(file => imageExtensions.Contains(Path.GetExtension(file), StringComparer.OrdinalIgnoreCase))
                .ToArray();
        }

        public static int getCurrentIndex(string path)
        {
            int index = 0;
            string[] images = getImages(path);
            foreach (string image in images)
            {
                if (path == image)
                {
                    break;
                }

                else
                {
                    index++;
                }
            }

            return index;
        }

        public static int deleteImages(string path, PictureBox pictureBox)
        {
            string[] images = getImages(path);
            int index = getCurrentIndex(path);
            string newPath;

            pictureBox.Image.Dispose();

            if (index + 1 >= images.Length)
            {
                pictureBox.Image.Dispose();
                pictureBox.Image = new Bitmap(images[0]);
                newPath = images[0];
            }

            else
            {
                pictureBox.Image.Dispose();
                pictureBox.Image = new Bitmap(images[index + 1]);
                newPath = images[index + 1];
            }

            if (images.Length == 1)
            {
                pictureBox.Image.Dispose();
                File.Delete(path);
                Application.Exit();
            }

            File.Delete(path);
            return getCurrentIndex(newPath);
        }

        public static int LoadNextImage(int index, PictureBox pictureBox, string path)
        {
            string[] allPaths = getImages(path);
            if (index + 1 >= allPaths.Length)
            {
                pictureBox.Image.Dispose();
                pictureBox.Image = new Bitmap(allPaths[0]);
                return getCurrentIndex(allPaths[0]);
            }

            else
            {
                pictureBox.Image.Dispose();
                pictureBox.Image = new Bitmap(allPaths[index + 1]);
                return getCurrentIndex(allPaths[index + 1]);
            }
        }

        public static int LoadPreviousImage(int index, PictureBox pictureBox, string path)
        {
            string[] allPaths = getImages(path);
            if (index - 1 < 0)
            {
                pictureBox.Image.Dispose();
                pictureBox.Image = new Bitmap(allPaths[allPaths.Length-1]);
                return getCurrentIndex(allPaths[allPaths.Length-1]);
            }

            else
            {
                pictureBox.Image.Dispose();
                pictureBox.Image = new Bitmap(allPaths[index - 1]);
                return getCurrentIndex(allPaths[index - 1]);
            }
        }

        public static void RotateImageClockwise(PictureBox pictureBox)
        {
            pictureBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureBox.Refresh();
        }

    }
}

