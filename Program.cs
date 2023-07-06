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
                Application.Run(new MainForm("C:\\Users\\akula\\Pictures\\Screenshots\\Screenshot 2023-06-12 210638.png"));
            }
            else
            {
                Application.Run(new MainForm(args[0]));
            }
        }
    }
}