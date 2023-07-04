using System;
using System.Drawing;
using System.Windows.Forms;
using optimizedPhotoViewer.Extensions;

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
