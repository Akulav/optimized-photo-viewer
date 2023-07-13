using OptimizedPhotoViewer.DataStructures;

namespace OptimizedPhotoViewer.Extensions
{
    public static class CacheOperator
    {
        public static void RemoveEntry(string key)
        {
            ImageCache.imageCache.Remove(key);
        }
    }
}
