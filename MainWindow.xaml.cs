using OptimizedPhotoViewer.DataStructures;
using OptimizedPhotoViewer.Extensions;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

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
            }

            string fullPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string[] fileExtensions = { ".png", ".jpg", ".jpeg", ".ico", ".tiff", ".bmp" };

            Parallel.ForEach(fileExtensions, extension =>
            {
                FileAssociations.SetAssociation(extension, "optimizedViewer", "Image File", fullPath);
            });

            UICommands.AddImagesToGrid(grid, 15, pictureBox, infoLabel);
            BackgroundProcesser worker = new();
            worker.StartFunction();

        }

        private void Image_MouseWheel(object sender, MouseWheelEventArgs e)
        {
           
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F11:
                    UICommands.ToggleFullScreen(this, grid, pictureBox, infoLabel);
                    break;
                case Key.D:
                case Key.Right:
                    UICommands.ScrollImage(pictureBox, infoLabel, true);
                    UICommands.AddImagesToGrid(grid, 15, pictureBox, infoLabel);
                    break;
                case Key.A:
                case Key.Left:
                    UICommands.ScrollImage(pictureBox, infoLabel, false);
                    UICommands.AddImagesToGrid(grid, 15, pictureBox, infoLabel);
                    break;
            }
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UICommands.AddImagesToGrid(grid, 15, pictureBox, infoLabel);
        }
    }
}
