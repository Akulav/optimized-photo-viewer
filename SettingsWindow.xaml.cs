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
            settingsManager.WriteSettings(settings);
            TempSettings.settings = settings;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.IsEnabled = true;
            settingsManager.WriteSettings(settings);
            TempSettings.settings = settings;
        }

        private void DisablePhotoListHandler(object sender, RoutedEventArgs e)
        {
            settings.PhotoList = false;
            settingsManager.WriteSettings(settings);
            TempSettings.settings = settings;
        }

        private void CacheLevelZeroHandler(object sender, RoutedEventArgs e)
        {
            settings.CacheLevel = 0;
            settingsManager.WriteSettings(settings);
            TempSettings.settings = settings;
        }

        private void CacheLevelLimitedHandler(object sender, RoutedEventArgs e)
        {
            settings.CacheLevel = 1;
            settingsManager.WriteSettings(settings);
            TempSettings.settings = settings;
        }

        private void CacheLevelUnlimitedHandler(object sender, RoutedEventArgs e)
        {
            settings.CacheLevel = 2;
            settingsManager.WriteSettings(settings);
            TempSettings.settings = settings;
        }

        private void PhotoListSizeSmallHandler(object sender, RoutedEventArgs e)
        {
            settings.ListSize = 3;
            settingsManager.WriteSettings(settings);
            TempSettings.settings = settings;
        }

        private void PhotoListSizeMediumHandler(object sender, RoutedEventArgs e)
        {
            settings.ListSize = 5;
            settingsManager.WriteSettings(settings);
            TempSettings.settings = settings;
        }

        private void PhotoListSizeLargeHandler(object sender, RoutedEventArgs e)
        {
            settings.ListSize = 7;
            settingsManager.WriteSettings(settings);
            TempSettings.settings = settings;
        }

    }
}
