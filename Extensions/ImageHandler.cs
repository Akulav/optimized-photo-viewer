namespace optimizedPhotoViewer.Extensions
{
    public static class ImageHandler
    {
        public static void getImages()
        {
            string directoryPath = Path.GetDirectoryName(TempSettings.defaultPath);
            string[] imageExtensions = { ".png", ".jpg", ".jpeg", ".bmp", ".ico", ".tiff", ".gif" };

            HashSet<string> imageExtensionsSet = new(imageExtensions, StringComparer.OrdinalIgnoreCase);
            string[] sorted = Directory.GetFiles(directoryPath)
                            .Where(file => imageExtensionsSet.Contains(Path.GetExtension(file)))
                            .ToArray();
            Array.Sort(sorted);
            TempSettings.allPaths = sorted;
        }

        public static void getCurrentIndex()
        {
            TempSettings.currentIndex = Array.IndexOf(TempSettings.allPaths, TempSettings.currentImage);
        }

        public static List<string> GetStringsInRange()
        {
            List<string> result = new();
            int count = TempSettings.allPaths.Length;
            int numItems = Math.Min(count, 5);

            int start = TempSettings.currentIndex - (numItems / 2) + ((numItems + 1) % 2);
            for (int i = 0; i < numItems; i++)
            {
                int index = (start + i + count) % count;
                result.Add(TempSettings.allPaths[index]);
            }

            return result;
        }

        public static void deleteImages(SQPhoto.SQPhoto pictureBox, Label info)
        {
            int imagesLength = TempSettings.allPaths.Length;
            string newPath = TempSettings.allPaths[(TempSettings.currentIndex + 1) % imagesLength];

            File.Delete(TempSettings.currentImage);
            loadImage(newPath, pictureBox, info);

            if (imagesLength == 1)
            {
                pictureBox.Image.Dispose();
                Application.Exit();
            }
            TempSettings.currentImage = newPath;
        }

        public static void scrollImage(SQPhoto.SQPhoto pictureBox, Label info, bool next)
        {
            int imagesLength = TempSettings.allPaths.Length;
            TempSettings.currentIndex = next ? (TempSettings.currentIndex + 1) % imagesLength : (TempSettings.currentIndex - 1 + imagesLength) % imagesLength;
            loadImage(TempSettings.allPaths[TempSettings.currentIndex], pictureBox, info);
            TempSettings.currentImage = TempSettings.allPaths[TempSettings.currentIndex];
        }

        public static void loadImage(string path, SQPhoto.SQPhoto pictureBox, Label info)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    Image image = Image.FromStream(ms);
                    pictureBox.Image = image;
                }
            }
            info.Text = Path.GetFileName(path);

            TempSettings.currentImage = path;
            getImages();
            getCurrentIndex();
        }

        public static void RotateImageClockwise(SQPhoto.SQPhoto pictureBox)
        {
            pictureBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureBox.Image.Save(TempSettings.currentImage);
            pictureBox.Refresh();
        }
    }
}