namespace OptimizedPhotoViewer.Extensions
{
    public class BackgroundProcesser
    {
        public BackgroundProcesser()
        {

        }

        public void StartFunction()
        {
            System.Threading.Timer timer = new(TimerCallback, null, 0, 100);
        }

        private void TimerCallback(object state)
        {
            DataProber.GetImages();
        }
    }
}
