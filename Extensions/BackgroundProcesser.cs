namespace optimizedPhotoViewer.Extensions
{
    public class BackgroundProcesser
    {
        public BackgroundProcesser()
        {

        }

        public void StartFunction()
        {
            System.Threading.Timer timer = new System.Threading.Timer(TimerCallback, null, 0, 100);
        }

        private void TimerCallback(object state)
        {
           // ImageHandler.getImages();
        }
    }
}
