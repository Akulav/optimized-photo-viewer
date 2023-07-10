using System.Collections.Concurrent;

namespace optimizedPhotoViewer.Extensions
{
    public static class ImageHandler
    {
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
            int numItems = Math.Min(count, 5);

            int start = TempSettings.CurrentIndex - (numItems / 2) + ((numItems + 1) % 2);
            for (int i = 0; i < numItems; i++)
            {
                int index = (start + i + count) % count;
                result.Add(TempSettings.AllPaths[index]);
            }

            return result;
        }

        public static void DeleteImages(SQPhoto.SQPhoto pictureBox, Label info)
        {
            int imagesLength = TempSettings.AllPaths.Length;
            string newPath = TempSettings.AllPaths[(TempSettings.CurrentIndex + 1) % imagesLength];

            File.Delete(TempSettings.CurrentImage);
            LoadImage(newPath, pictureBox, info);

            if (imagesLength == 1)
            {
                pictureBox.Image.Dispose();
                Application.Exit();
            }
            TempSettings.CurrentImage = newPath;
        }

        public static void LoadImage(string path, SQPhoto.SQPhoto pictureBox, Label info)
        {
            using (FileStream fs = new(path, FileMode.Open, FileAccess.Read))
            {
                using MemoryStream ms = new();
                fs.CopyTo(ms);
                Image image = Image.FromStream(ms);
                pictureBox.Image = image;
            }

            info.Text = Path.GetFileName(path);
            TempSettings.CurrentImage = path;
            GetImages();
            GetCurrentIndex();
        }

        public static void RotateImageClockwise(SQPhoto.SQPhoto pictureBox)
        {
            pictureBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureBox.Image.Save(TempSettings.CurrentImage);
            pictureBox.Refresh();
        }
    }
}