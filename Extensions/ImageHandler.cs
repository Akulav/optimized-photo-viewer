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
            return Array.IndexOf(images, path);
        }

        public static List<string> GetStringsInRange(string path)
        {
            string[] strings = getImages(path);
            int currentIndex = getCurrentIndex(path);
            List<string> result = new List<string>();
            int count = strings.Length;
            int numItems = Math.Min(count, 5);

            int start = currentIndex - (numItems / 2) + ((numItems + 1) % 2);
            for (int i = 0; i < numItems; i++)
            {
                int index = (start + i + count) % count;
                result.Add(strings[index]);
            }

            return result;
        }



        public static string deleteImages(string path, SQPhoto.SQPhoto pictureBox, Label info)
        {
            string[] images = getImages(path);
            int index = getCurrentIndex(path);
            int imagesLength = images.Length;
            string newPath = images[(index + 1) % imagesLength];

            loadImage(newPath, pictureBox, info);
            File.Delete(path);

            if (imagesLength == 1)
            {
                pictureBox.Image.Dispose();
                Application.Exit();
            }

            return newPath;
        }

        public static string scrollImage(SQPhoto.SQPhoto pictureBox, string path, Label info, bool next)
        {
            string[] allPaths = getImages(path);
            int index = getCurrentIndex(path);
            int imagesLength = allPaths.Length;
            int newIndex = next ? (index + 1) % imagesLength : (index - 1 + imagesLength) % imagesLength;

            loadImage(allPaths[newIndex], pictureBox, info);
            return allPaths[newIndex];
        }

        public static void loadImage(string path, SQPhoto.SQPhoto pictureBox, Label info)
        {
            pictureBox.Image.Dispose();
            pictureBox.Image = new Bitmap(path);
            info.Text = Path.GetFileName(path);
        }

        public static void RotateImageClockwise(SQPhoto.SQPhoto pictureBox, string path)
        {
            pictureBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureBox.Image.Save(path);
            pictureBox.Refresh();
        }
    }
}