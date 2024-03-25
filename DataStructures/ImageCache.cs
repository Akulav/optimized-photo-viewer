using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace OptimizedPhotoViewer.DataStructures
{
    public static class ImageCache
    {
        public static Dictionary<string, BitmapImage> imageCache = new();
    }
}
