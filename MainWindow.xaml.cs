using OptimizedPhotoViewer.DataStructures;
using OptimizedPhotoViewer.Extensions;
using OptimizedPhotoViewer.Settings;
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
                ImageLoader.LoadImage(path, pictureBox, infoText);
                BackgroundProcesser worker = new();
                worker.StartFunction();
                SettingsManager settingsManager = new SettingsManager();
                settingsManager.EnsureSettingsFileExists();
                TempSettings.settings = settingsManager.ReadSettings();
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

        private void SettingsClickHandler(object sender, MouseEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow(this);

            settingsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            // Set the owner of the new window as the old window
            settingsWindow.Owner = this;

            // Show the new window as a dialog
            settingsWindow.ShowDialog();

            // Re-enable the old window when the new window is closed
            this.IsEnabled = true;
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
            UICommands.KeySwitcher(e.Key, this, grid, pictureBox, infoText);
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UICommands.AddImagesToGrid(grid, 15, pictureBox, infoText);
            Image[] images = { rotateBtn, deleteBtn, favoriteBtn, focusBtn, galleryBtn, SettingsBtn };
            UICommands.MoveButtons(images, grid.ActualWidth);
        }

        private void DeleteClickHandler(object sender, MouseButtonEventArgs e)
        {
            CacheOperator.RemoveEntry(TempSettings.CurrentImage);
            ImageDeleter.DeleteImages(pictureBox, infoText);
            UICommands.AddImagesToGrid(grid, 15, pictureBox, infoText);
        }

        private void FolderClickHandler(object sender, MouseButtonEventArgs e)
        {
            UICommands.OpenFolder();
        }

        private void RotateClickHandler(object sender, MouseButtonEventArgs e)
        {
            ImageRotater.RotateOnDisk();
            CacheOperator.RemoveEntry(TempSettings.CurrentImage);
            ImageLoader.LoadImage(TempSettings.CurrentImage, pictureBox, infoText);
            UICommands.AddImagesToGrid(grid, 15, pictureBox, infoText);
        }
    }
}
