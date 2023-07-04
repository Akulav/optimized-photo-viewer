using optimizedPhotoViewer.Extensions;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Drawing;
using System;

namespace optimizedPhotoViewer
{
    public partial class MainForm : Form
    {
        private string[] imagePaths;
        private int currentIndex;

        public MainForm(string args)
        {
            InitializeComponent();

            if (args != null && System.IO.File.Exists(args))
            {
                pictureBox.Image = new Bitmap(args);
                imagePaths = FileHandler.LoadImagePaths(args);
                currentIndex = Array.IndexOf(imagePaths, args);
            }
            string fullPath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
          
            FileAssociations.SetAssociation(".png", "optimizedViewer", "Image File", fullPath);
            FileAssociations.SetAssociation(".jpeg", "optimizedViewer", "Image File", fullPath);
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            FileHandler.DeleteCurrentImage(imagePaths, currentIndex, pictureBox);

            if (imagePaths.Length > 0)
            {
                if(currentIndex+1 >= imagePaths.Length)
                {
                    currentIndex = 0;
                }
                else
                {
                    currentIndex = currentIndex + 1;
                }
                pictureBox.Image = new Bitmap(imagePaths[currentIndex]);
            }
            else
            {
                pictureBox.Image = null;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
