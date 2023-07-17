using OptimizedPhotoViewer.DataStructures;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizedPhotoViewer.Extensions
{
    public static class DataProber
    {
        public static void GetImages()
        {
            string directoryPath = Path.GetDirectoryName(TempSettings.DefaultPath);

            var imageExtensionsSet = new HashSet<string>(ImageExtensions.extension_list, StringComparer.OrdinalIgnoreCase);
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
            int numItems = Math.Min(count, TempSettings.settings.ListSize);

            int start = TempSettings.CurrentIndex - (numItems / 2) + ((numItems + 1) % 2);
            for (int i = 0; i < numItems; i++)
            {
                int index = (start + i + count) % count;
                result.Add(TempSettings.AllPaths[index]);
            }

            return result;
        }
    }
}
