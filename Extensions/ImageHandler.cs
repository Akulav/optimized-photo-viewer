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
            string newPath = images[(index + 1) % imagesLength];

            loadImage(newPath, pictureBox, info);

            if (imagesLength == 1)
            {
                pictureBox.Image.Dispose();
                File.Delete(path);
                Application.Exit();
            }

            File.Delete(path);
            return newPath;
        }

        public static string scrollImage(PictureBox pictureBox, string path, Label info, bool next)
        {
            string[] allPaths = getImages(path);
            int index = getCurrentIndex(path);
            int imagesLength = allPaths.Length;
            int newIndex = next ? (index + 1) % imagesLength : (index - 1 + imagesLength) % imagesLength;

            loadImage(allPaths[newIndex], pictureBox, info);
            return allPaths[newIndex];
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

