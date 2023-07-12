using OptimizedPhotoViewer.DataStructures;
using OptimizedPhotoViewer.Extensions;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OptimizedPhotoViewer
{
    public partial class MainWindow : Window
    {
        public MainWindow(string path)
        {
            InitializeComponent();
            if (File.Exists(path))
            {
                TempSettings.DefaultPath = path;
                ImageHandler.LoadImage(path, pictureBox, infoLabel);
                BackgroundProcesser worker = new();
                worker.StartFunction();
            }

            string fullPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string[] fileExtensions = { ".png", ".jpg", ".jpeg", ".ico", ".tiff", ".bmp" };

            Parallel.ForEach(fileExtensions, extension =>
            {
                FileAssociations.SetAssociation(extension, "optimizedViewer", "Image File", fullPath);
            });
        }

        private void Image_MouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            UICommands.KeySwitcher(e.Key, this, grid, pictureBox, infoLabel);
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UICommands.AddImagesToGrid(grid, 15, pictureBox, infoLabel);
        }

        private void DeleteClickHandler(object sender, MouseButtonEventArgs e)
        {
            ImageHandler.DeleteImages(pictureBox, infoLabel);
            UICommands.AddImagesToGrid(grid, 15, pictureBox, infoLabel);
        }

        private void RotateClickHandler(object sender, MouseButtonEventArgs e)
        {
            ImageHandler.RotateOnDisk();
            ImageHandler.RemoveEntry(TempSettings.CurrentImage);
            ImageHandler.LoadImage(TempSettings.CurrentImage, pictureBox, infoLabel);
            UICommands.AddImagesToGrid(grid, 15, pictureBox, infoLabel);
        }
    }
}
