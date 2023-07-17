using OptimizedPhotoViewer.DataStructures;
using OptimizedPhotoViewer.Extensions;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace OptimizedPhotoViewer
{
    public partial class MainWindow : Window
    {
        private double initialWidth;
        private double initialHeight;
        private Thickness initialMargin;
        public MainWindow(string path)
        {
            InitializeComponent();
            if (File.Exists(path))
            {
                TempSettings.DefaultPath = path;
                ImageLoader.LoadImage(path, pictureBox, infoLabel);
                BackgroundProcesser worker = new();
                worker.StartFunction();
            }

            string fullPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            Parallel.ForEach(ImageExtensions.extension_list, extension =>
            {
                FileAssociations.SetAssociation(extension, "optimizedViewer", "Image File", fullPath);
            });

            initialWidth = pictureBox.Width;
            initialHeight = pictureBox.Height;
            initialMargin = pictureBox.Margin;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Image_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            UICommands.zoom(sender, e, grid);
        }


        private void FavoriteClickHandler(object sender, MouseButtonEventArgs e)
        {

        }

        private void FocusClickHandler(object sender, MouseButtonEventArgs e)
        {
            UICommands.ResetImage(pictureBox, initialWidth, initialHeight, initialMargin);
        }

        private void GalleryClickHandler(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            UICommands.KeySwitcher(e.Key, this, grid, pictureBox, infoLabel);
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UICommands.AddImagesToGrid(grid, 15, pictureBox, infoLabel);
            Image[] images = { rotateBtn, deleteBtn, favoriteBtn, focusBtn, galleryBtn };
            UICommands.MoveButtons(images, grid.ActualWidth);
        }

        private void DeleteClickHandler(object sender, MouseButtonEventArgs e)
        {
            CacheOperator.RemoveEntry(TempSettings.CurrentImage);
            ImageDeleter.DeleteImages(pictureBox, infoLabel);
            UICommands.AddImagesToGrid(grid, 15, pictureBox, infoLabel);
        }

        private void RotateClickHandler(object sender, MouseButtonEventArgs e)
        {
            ImageRotater.RotateOnDisk();
            CacheOperator.RemoveEntry(TempSettings.CurrentImage);
            ImageLoader.LoadImage(TempSettings.CurrentImage, pictureBox, infoLabel);
            UICommands.AddImagesToGrid(grid, 15, pictureBox, infoLabel);
        }
    }
}
