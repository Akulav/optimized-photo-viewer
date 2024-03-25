using OptimizedPhotoViewer.DataStructures;
using System.Collections.Generic;
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
            List<string> keysToRemove = GetKeysToRemove(dictionary, array);
            RemoveKeys(keysToRemove);
        }

        private static List<string> GetKeysToRemove(Dictionary<string, BitmapImage> dictionary, string[] array)
        {
            HashSet<string> keySet = new HashSet<string>(array);
            List<string> keysToRemove = new List<string>();

            foreach (var kvp in dictionary)
            {
                if (!keySet.Contains(kvp.Key))
                {
                    keysToRemove.Add(kvp.Key);
                }
            }

            return keysToRemove;
        }

        private static void RemoveKeys(List<string> keysToRemove)
        {
            foreach (string key in keysToRemove)
            {
                RemoveEntry(key);
            }
        }

    }
}