namespace optimizedPhotoViewer
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();
            if (args.Length == 0)
            {
                Application.Run(new MainForm(null));
            }
            else
            {
                Application.Run(new MainForm(args[0]));
            }
        }
    }
}