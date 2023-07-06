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

            if (index + 1 >= imagesLength)
            {
                loadImage(allPaths[0], pictureBox, info);
                return allPaths[0];
            }

            else
            {
                loadImage(allPaths[index + 1], pictureBox, info);
                return allPaths[index + 1];
            }
        }

        public static string LoadPreviousImage(PictureBox pictureBox, string path, Label info)
        {
            string[] allPaths = getImages(path);
            int index = getCurrentIndex(path);
            int imagesLength = allPaths.Length;

            if (index - 1 < 0)
            {
                loadImage(allPaths[imagesLength - 1], pictureBox, info);
                return allPaths[imagesLength - 1];
            }

            else
            {
                loadImage(allPaths[index - 1], pictureBox, info);
                return allPaths[index - 1];
            }
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

