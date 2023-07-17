using Newtonsoft.Json;
using System.IO;

namespace OptimizedPhotoViewer.Settings
{
    public class AppSettings
    {
        public bool PhotoList { get; set; }
        public int CacheLevel { get; set; }
        public int ListSize { get; set; }
    }

    public class SettingsManager
    {
        private readonly string filePath = @"C:\PhotoViewer\settings.json";

        public SettingsManager()
        {

        }

        public void EnsureSettingsFileExists()
        {
            string folderPath = Path.GetDirectoryName(filePath);

            // Create the folder if it doesn't exist
            Directory.CreateDirectory(folderPath);

            if (!File.Exists(filePath))
            {
                // Create the default settings
                var defaultSettings = new AppSettings
                {
                    CacheLevel = 2,
                    PhotoList = true,
                    ListSize = 10
                };

                // Serialize and save the default settings to the JSON file
                string json = JsonConvert.SerializeObject(defaultSettings);
                File.WriteAllText(filePath, json);
            }
        }

        public void WriteSettings(AppSettings settings)
        {
            string json = JsonConvert.SerializeObject(settings);
            File.WriteAllText(filePath, json);
        }

        public AppSettings ReadSettings()
        {
            if (File.Exists(filePath))
            {
                // Read the settings from the JSON file
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<AppSettings>(json);
            }

            return null;
        }
    }

}
