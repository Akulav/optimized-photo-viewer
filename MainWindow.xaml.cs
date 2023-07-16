using OptimizedPhotoViewer.DataStructures;
using OptimizedPhotoViewer.Extensions;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            string[] fileExtensions = { ".png", ".jpg", ".jpeg", ".ico", ".tiff", ".bmp", ".webp" };

            Parallel.ForEach(fileExtensions, extension =>
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
            Image image = (Image)sender;
            double zoomFactor = 1.0;
            double zoomStep = 0.1;
            double minZoom = 0.5;
            double maxZoom = 2.0;
            int previousDelta = 0;

            // Get the mouse position relative to the image
            Point mousePosition = e.GetPosition(image);

            // Calculate the zoom factor based on the mouse wheel delta
            if (previousDelta * e.Delta < 0)
            {
                // Reverse the zoom direction if the mouse wheel direction changes
                zoomFactor = 1.0 + zoomStep * Math.Sign(e.Delta);
            }
            else
            {
                // Continue zooming in the same direction
                zoomFactor += zoomStep * Math.Sign(e.Delta);
            }

            // Constrain the zoom factor within the specified range
            zoomFactor = Math.Clamp(zoomFactor, minZoom, maxZoom);

            // Calculate the new image size
            double newImageWidth = image.ActualWidth * zoomFactor;
            double newImageHeight = image.ActualHeight * zoomFactor;

            // Calculate the new margin to keep the mouse position centered
            double newLeftMargin = image.Margin.Left - (mousePosition.X * (newImageWidth - image.ActualWidth) / image.ActualWidth);
            double newTopMargin = image.Margin.Top - (mousePosition.Y * (newImageHeight - image.ActualHeight) / image.ActualHeight);

            // Constrain the image movement within the row
            double rowWidth = grid.ActualWidth;
            double rowHeight = grid.RowDefinitions[0].ActualHeight;
            double maxLeftMargin = rowWidth - newImageWidth;
            double maxTopMargin = rowHeight - newImageHeight;
            newLeftMargin = Math.Max(0, Math.Min(maxLeftMargin, newLeftMargin));
            newTopMargin = Math.Max(0, Math.Min(maxTopMargin, newTopMargin));

            // Update the image's size and margin
            image.Width = newImageWidth;
            image.Height = newImageHeight;
            image.Margin = new Thickness(newLeftMargin, newTopMargin, 0, 0);

            // Store the current mouse wheel delta for the next iteration
            previousDelta = e.Delta;
        }


        private void FavoriteClickHandler(object sender, MouseButtonEventArgs e)
        {

        }

        private void FocusClickHandler(object sender, MouseButtonEventArgs e)
        {
            UICommands.resetImage(pictureBox, initialWidth, initialHeight, initialMargin);
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
            UICommands.moveButtons(images, grid.ActualWidth);
        }

        private void DeleteClickHandler(object sender, MouseButtonEventArgs e)
        {
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
