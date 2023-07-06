namespace optimizedPhotoViewer.Extensions
{
    public static class ImageHandler
    {
        public static string[] getImages(string path)
        {
            string directoryPath = Path.GetDirectoryName(path);
            string[] imageExtensions = { ".png", ".jpg", ".webp", "jpeg", ".bmp", ".ico", ".tiff", ".gif" };

            HashSet<string> imageExtensionsSet = new HashSet<string>(imageExtensions, StringComparer.OrdinalIgnoreCase);
            string[] sorted = Directory.GetFiles(directoryPath)
                            .Where(file => imageExtensionsSet.Contains(Path.GetExtension(file)))
                            .ToArray();
            Array.Sort(sorted);

            return sorted;
        }

        public static int getCurrentIndex(string path)
        {
            string[] images = getImages(path);
            int index = Array.IndexOf(images, path);
            return index;
        }

        public static string deleteImages(string path, PictureBox pictureBox, Label info)
        {
            string[] images = getImages(path);
            int index = getCurrentIndex(path);
            int imagesLength = images.Length;
            string newPath;

            if (index + 1 == imagesLength)
            {
                loadImage(images[0], pictureBox, info);
                newPath = images[0];
            }

            else
            {
                loadImage(images[index + 1], pictureBox, info);
                newPath = images[index + 1];
            }

            if (imagesLength == 1)
            {
                pictureBox.Image.Dispose();
                File.Delete(path);
                Application.Exit();
            }

            File.Delete(path);
            return newPath;
        }

        public static string LoadNextImage(PictureBox pictureBox, string path, Label info)
        {
            string[] allPaths = getImages(path);
            int index = getCurrentIndex(path);
            int imagesLength = allPaths.Length;
            int nextIndex = (index + 1) % imagesLength;

            loadImage(allPaths[nextIndex], pictureBox, info);
            return allPaths[nextIndex];
        }

        public static string LoadPreviousImage(PictureBox pictureBox, string path, Label info)
        {
            string[] allPaths = getImages(path);
            int index = getCurrentIndex(path);
            int imagesLength = allPaths.Length;
            int previousIndex = (index - 1 + imagesLength) % imagesLength;

            loadImage(allPaths[previousIndex], pictureBox, info);
            return allPaths[previousIndex];
        }

        public static void loadImage(string path, PictureBox pictureBox, Label info)
        {
            pictureBox.Image.Dispose();
            pictureBox.Image = new Bitmap(path);
            info.Text = Path.GetFileName(path);
        }

        public static void RotateImageClockwise(PictureBox pictureBox, string path)
        {
            pictureBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureBox.Image.Save(path);
            pictureBox.Refresh();
        }
    }
}

