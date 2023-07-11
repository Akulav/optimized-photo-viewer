﻿using System.Windows;

namespace OptimizedPhotoViewer
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            if (e.Args.Length > 0)
            {
                string filePath = e.Args[0];
                MainWindow mainWindow = new MainWindow(filePath);
                mainWindow.Show();
            }

            else
            {
                MainWindow mainWindow = new MainWindow("C:\\Users\\akula\\Desktop\\TEST - Copy\\def.jpg");
                mainWindow.Show();
            }
        }
    }
}
