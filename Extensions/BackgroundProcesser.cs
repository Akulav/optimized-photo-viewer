namespace optimizedPhotoViewer.Extensions
{
    public class BackgroundProcesser
    {
        public BackgroundProcesser()
        {
        }

        public void StartFunction()
        {
            System.Threading.Timer timer = new(TimerCallback, null, 0, 200);
        }

        private void TimerCallback(object state)
        {
           ImageHandler.GetImages();
        }
    }
}
