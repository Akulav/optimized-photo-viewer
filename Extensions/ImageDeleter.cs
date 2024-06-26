﻿using OptimizedPhotoViewer.DataStructures;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace OptimizedPhotoViewer.Extensions
{
    public static class ImageDeleter
    {
        public static void DeleteImages(Image pictureBox, TextBlock info)
        {
            int imagesLength = TempSettings.AllPaths.Length;
            string newPath = TempSettings.AllPaths[(TempSettings.CurrentIndex + 1) % imagesLength];

            File.Delete(TempSettings.CurrentImage);
            ImageLoader.LoadImage(newPath, pictureBox, info);

            if (TempSettings.AllPaths.Length == 1)
            {
                Application.Current.Shutdown();
            }

            TempSettings.CurrentImage = newPath;
        }
    }
}
