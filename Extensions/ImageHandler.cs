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

        public static string deleteImages(string path)
        {
            string[] images = getImages(path);
            int index = 0;
            int next;
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

            if (images.Length == 1)
            {
                return "";
            }

            if (index+1 >= images.Length)
            {
                next = 0;
            }

            else
            {
                next = index+1;
            }

            return images[next];
        }
    }
}
