using OptimizedPhotoViewer.DataStructures;

namespace OptimizedPhotoViewer.Extensions
{
    public class BackgroundProcesser
    {
        private System.Threading.Timer timer;
        public BackgroundProcesser()
        {

        }

        public void StartFunction()
        {
            timer = new(TimerCallback, null, 0, 200);
        }

        private void TimerCallback(object state)
        {
            DataProber.GetImages();
            CacheOperator.RemoveMissingKeys(ImageCache.imageCache, TempSettings.AllPaths);
        }
    }
}
