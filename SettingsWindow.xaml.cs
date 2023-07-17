using OptimizedPhotoViewer.DataStructures;
using OptimizedPhotoViewer.Settings;
using System.Windows;

namespace OptimizedPhotoViewer
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private AppSettings settings = new AppSettings();
        private SettingsManager settingsManager = new();
        private Window mainWindow;
        public SettingsWindow(Window mainWindow)
        {
            InitializeComponent();
            Topmost = true;
            this.mainWindow = mainWindow;
            settings = settingsManager.ReadSettings();
        }

        private void EnablePhotoListHandler(object sender, RoutedEventArgs e)
        {
            settings.PhotoList = true;
        }

        private void SaveHandler(object sender, RoutedEventArgs e)
        {
            settingsManager.WriteSettings(settings);
            TempSettings.settings = settings;
            mainWindow.IsEnabled = true;
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.IsEnabled = true;
        }

        private void DisablePhotoListHandler(object sender, RoutedEventArgs e)
        {
            settings.PhotoList = false;
        }

        private void CacheLevelZeroHandler(object sender, RoutedEventArgs e)
        {
            settings.CacheLevel = 0;
        }

        private void CacheLevelLimitedHandler(object sender, RoutedEventArgs e)
        {
            settings.CacheLevel = 1;
        }

        private void CacheLevelUnlimitedHandler(object sender, RoutedEventArgs e)
        {
            settings.CacheLevel = 2;
        }

        private void PhotoListSizeSmallHandler(object sender, RoutedEventArgs e)
        {
            settings.ListSize = 3;
        }

        private void PhotoListSizeMediumHandler(object sender, RoutedEventArgs e)
        {
            settings.ListSize = 5;
        }

        private void PhotoListSizeLargeHandler(object sender, RoutedEventArgs e)
        {
            settings.ListSize = 7;
        }

    }


}
