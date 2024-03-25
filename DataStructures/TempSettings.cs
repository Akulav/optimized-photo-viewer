using OptimizedPhotoViewer.Settings;

namespace OptimizedPhotoViewer.DataStructures
{
    public static class TempSettings
    {
        public static bool IsFullscreen { get; set; }
        public static string CurrentImage { get; set; }
        public static string[] AllPaths { get; set; }
        public static int CurrentIndex { get; set; }
        public static string DefaultPath { get; set; }
        public static AppSettings settings { get; set; }
    }
}
