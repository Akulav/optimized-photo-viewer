namespace optimizedPhotoViewer.Extensions
{
    public class BackgroundProcesser
    {
        public BackgroundProcesser()
        {
        }

        public void StartFunction()
        {
            System.Threading.Timer timer = new(TimerCallback, null, 0, 10);
        }

        private void TimerCallback(object state)
        {
           ImageHandler.GetImages();
        }
    }
}
