namespace optimizedPhotoViewer.Extensions
{
    public static class ImageHandler
    {
        public static string[] getImages(string path)
        {
            string directoryPath = Path.GetDirectoryName(path);
            string[] imageExtensions = { ".png", ".jpg", ".webp", "jpeg", ".bmp", ".ico", ".tiff" };
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

        public static int deleteImages(string path, PictureBox pictureBox, Label info)
        {
            string[] images = getImages(path);
            int index = getCurrentIndex(path);
            string newPath;

            if (index + 1 == images.Length)
            {
                loadImage(images[0],pictureBox, info);
                newPath = images[0];
            }

            else
            {
                loadImage(images[index + 1], pictureBox, info);
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

        public static int LoadNextImage(int index, PictureBox pictureBox, string path, Label info)
        {
            string[] allPaths = getImages(path);
            if (index + 1 >= allPaths.Length)
            {
                loadImage(allPaths[0], pictureBox, info);
                return getCurrentIndex(allPaths[0]);
            }

            else
            {

                loadImage(allPaths[index + 1], pictureBox, info);
                return getCurrentIndex(allPaths[index + 1]);
            }
        }

        public static void loadImage(string path, PictureBox pictureBox, Label info)
        {
            pictureBox.Image.Dispose();
            pictureBox.Image = new Bitmap(path);
            info.Text = Path.GetFileName(path);
        }

        public static int LoadPreviousImage(int index, PictureBox pictureBox, string path, Label info)
        {
            string[] allPaths = getImages(path);
            if (index - 1 < 0)
            {
                loadImage(allPaths[allPaths.Length - 1], pictureBox, info);
                return getCurrentIndex(allPaths[allPaths.Length - 1]);
            }

            else
            {
                loadImage(allPaths[index - 1],pictureBox, info);
                return getCurrentIndex(allPaths[index - 1]);
            }
        }

        public static void RotateImageClockwise(PictureBox pictureBox, int index, string path)
        {
            pictureBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureBox.Image.Save(getImages(path)[index]);
            pictureBox.Refresh();
        }
    }
}

