using OptimizedPhotoViewer.DataStructures;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;

namespace OptimizedPhotoViewer.Extensions
{
    public static class CacheOperator
    {
        public static void RemoveEntry(string key)
        {
            ImageCache.imageCache.Remove(key);
        }

        public static void RemoveMissingKeys(Dictionary<string, BitmapImage> dictionary, string[] array)
        {
            List<string> keysToRemove = new();

            foreach (string key in dictionary.Keys.ToList())
            {
                if (!array.Contains(key))
                {
                    keysToRemove.Add(key);
                }
            }

            foreach (string key in keysToRemove)
            {
                RemoveEntry(key);
            }
        }
    }
}