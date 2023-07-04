namespace optimizedPhotoViewer
{
    public partial class MainForm : Form
    {
        public MainForm(string args)
        {
            InitializeComponent();
            if (args != null)
            {
                pictureBox.Image = new Bitmap(args);
            }
        }
    }
}