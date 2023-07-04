using Microsoft.Win32;
using optimizedPhotoViewer.Extensions;
using System.Runtime.InteropServices;

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
            string fullPath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
          
            FileAssociations.SetAssociation(".png", "optimizedViewer", "Image File", fullPath);
            FileAssociations.SetAssociation(".jpeg", "optimizedViewer", "Image File", fullPath);
        }
    }
}