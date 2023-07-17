using OptimizedPhotoViewer.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;

namespace OptimizedPhotoViewer.Extensions
{
    public static class UICommands
    {
        public static void KeySwitcher(Key Key, Window window, Grid grid, Image pictureBox, Label infoLabel)
        {
            switch (Key)
            {
                case Key.F11:
                    ToggleFullScreen(window, grid, pictureBox, infoLabel);
                    break;
                case Key.D:
                case Key.Right:
                    ScrollImage(pictureBox, infoLabel, true);
                    AddImagesToGrid(grid, 15, pictureBox, infoLabel);
                    break;
                case Key.A:
                case Key.Left:
                    ScrollImage(pictureBox, infoLabel, false);
                    AddImagesToGrid(grid, 15, pictureBox, infoLabel);
                    break;
            }
        }

        public static void zoom(object sender, MouseWheelEventArgs e, Grid grid)
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

        public static void ToggleFullScreen(Window window, Grid grid, Image pictureBox, Label infoLabel)
        {
            RowDefinitionCollection rowDefinitions = grid.RowDefinitions;
            if (TempSettings.IsFullscreen)
            {
                // Set the window state to Maximized
                window.WindowState = WindowState.Maximized;

                // Remove the window style that includes the taskbar and top bar
                var hwnd = new WindowInteropHelper(window).Handle;
                var style = NativeMethods.GetWindowLong(hwnd, NativeMethods.GWL_STYLE);
                style &= ~(NativeMethods.WS_CAPTION | NativeMethods.WS_SYSMENU);
                NativeMethods.SetWindowLong(hwnd, NativeMethods.GWL_STYLE, style);

                // Make the window cover the entire screen
                window.Topmost = true;
                window.WindowState = WindowState.Normal;
                window.WindowState = WindowState.Maximized;

                rowDefinitions[0].Height = new GridLength(45);

                // Modify the height of the second row to 100% (star sizing)
                rowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);

                // Modify the height of the third row to 0
                rowDefinitions[2].Height = new GridLength(0);
            }
            else
            {

                // Restore the window state to Normal
                window.WindowState = WindowState.Normal;

                // Restore the window style with taskbar and top bar
                var hwnd = new WindowInteropHelper(window).Handle;
                var style = NativeMethods.GetWindowLong(hwnd, NativeMethods.GWL_STYLE);
                style |= (NativeMethods.WS_CAPTION | NativeMethods.WS_SYSMENU);
                NativeMethods.SetWindowLong(hwnd, NativeMethods.GWL_STYLE, style);

                // Refresh the window to update the style
                NativeMethods.SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0,
                                           NativeMethods.SWP_FRAMECHANGED | NativeMethods.SWP_NOZORDER |
                                           NativeMethods.SWP_NOMOVE | NativeMethods.SWP_NOSIZE);



                rowDefinitions[0].Height = new GridLength(45);

                // Modify the height of the second row to 100% (star sizing)
                rowDefinitions[1].Height = new GridLength(85, GridUnitType.Star);

                // Modify the height of the third row to 0
                rowDefinitions[2].Height = new GridLength(15, GridUnitType.Star);

                AddImagesToGrid(grid, 15, pictureBox, infoLabel);
            }

            TempSettings.IsFullscreen = !TempSettings.IsFullscreen;
        }

        public static void ScrollImage(Image pictureBox, Label info, bool next)
        {
            DataProber.GetImages();
            int imagesLength = TempSettings.AllPaths.Length;
            TempSettings.CurrentIndex = next ? (TempSettings.CurrentIndex + 1) % imagesLength : (TempSettings.CurrentIndex - 1 + imagesLength) % imagesLength;
            ImageLoader.LoadImage(TempSettings.AllPaths[TempSettings.CurrentIndex], pictureBox, info);
            TempSettings.CurrentImage = TempSettings.AllPaths[TempSettings.CurrentIndex];
        }

        public static void AddImagesToGrid(Grid grid, double spacing, Image mainPictureBox, Label infoLabel)
        {
            List<string> imagePaths = DataProber.GetStringsInRange();
            int imageCount = imagePaths.Count;

            // Remove existing content in the third row
            var elementsToRemove = grid.Children.Cast<UIElement>()
                                               .Where(element => Grid.GetRow(element) == 2)
                                               .ToList();

            foreach (var element in elementsToRemove)
            {
                grid.Children.Remove(element);
            }

            double availableHeight = grid.RowDefinitions[2].ActualHeight;

            double totalImagesWidth = 0;
            double maxHeight = 0;

            foreach (string imagePath in imagePaths)
            {
                BitmapImage bitmapImage = new(new Uri(imagePath));
                double imageHeight = availableHeight;
                double imageWidth = bitmapImage.PixelWidth / (bitmapImage.PixelHeight / imageHeight);

                totalImagesWidth += imageWidth;
                maxHeight = Math.Max(maxHeight, imageHeight);
            }

            double totalSpacingWidth = spacing * (imageCount - 1);
            double startX = (grid.ActualWidth - totalImagesWidth - totalSpacingWidth) / 2;

            for (int i = 0; i < imageCount; i++)
            {
                string imagePath = imagePaths[i];

                double imageHeight = availableHeight;
                double imageWidth = new BitmapImage(new Uri(imagePath)).PixelWidth / (new BitmapImage(new Uri(imagePath)).PixelHeight / imageHeight);

                Image image = CreateImage(imagePath, imageWidth, imageHeight, startX, grid);

                image.MouseUp += (sender, e) =>
                {
                    ImageLoader.LoadImage(image.Tag.ToString(), mainPictureBox, infoLabel);
                    AddImagesToGrid(grid, spacing, mainPictureBox, infoLabel);
                };

                grid.Children.Add(image);
                startX += imageWidth + spacing;
            }

            GC.Collect();
        }

        private static Image CreateImage(string imagePath, double imageWidth, double imageHeight, double startX, Grid grid)
        {
            var image = new Image
            {
                Stretch = Stretch.UniformToFill,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = imageWidth,
                Height = imageHeight,
                Margin = new Thickness(startX, 0, 0, 0),
                Tag = imagePath
            };

            ImageLoader.LoadImage(imagePath, image, null);

            Grid.SetRow(image, 2);
            Grid.SetColumn(image, grid.Children.Count);

            // Set attached properties to anchor the image within the grid
            Grid.SetColumnSpan(image, grid.Children.Count);
            Grid.SetZIndex(image, int.MinValue);

            return image;
        }

        public static void ResetImage(Image pictureBox, double initialWidth, double initialHeight, Thickness initialMargin)
        {
            pictureBox.Width = initialWidth;
            pictureBox.Height = initialHeight;
            pictureBox.Margin = initialMargin;
        }

        public static void MoveButtons(Image[] images, double availableWidth)
        {
            double totalImagesWidth = (images.Length * 30) + ((images.Length - 1) * 10); // Calculate the total width of the images and spacing
            double startX = (availableWidth - totalImagesWidth) / 2; // Calculate the starting X position

            for (int i = 0; i < images.Length; i++)
            {
                Image image = images[i];
                image.Margin = new Thickness(startX, 0, 0, 0); // Set the margin to position the image

                startX += 40; // Increment startX by 40 (30 for the image width + 10 for the spacing)
            }
        }

    }
    internal static class NativeMethods
    {
        public const int GWL_STYLE = -16;
        public const int WS_CAPTION = 0x00C00000;
        public const int WS_SYSMENU = 0x00080000;
        public const int SWP_FRAMECHANGED = 0x0020;
        public const int SWP_NOZORDER = 0x0004;
        public const int SWP_NOMOVE = 0x0002;
        public const int SWP_NOSIZE = 0x0001;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hwnd, int index);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter,
                                               int x, int y, int width, int height, uint flags);
    }
}
