using optimizedPhotoViewer.Extensions;
using System.Runtime.InteropServices;

namespace optimizedPhotoViewer
{
    public partial class MainForm : Form
    {
        private bool isFullscreen;
        private bool maximized;
        private string currentImage;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        public MainForm(string args)
        {
            InitializeComponent();
            DoubleBuffered = true;
            if (args != null && File.Exists(args))
            {
                ImageHandler.loadImage(args, pictureBox, infoLabel);
                int currentIndex = ImageHandler.getCurrentIndex(args);
                currentImage = ImageHandler.getImages(args)[currentIndex];

                //
                /*
                List<Bitmap> images = new List<Bitmap>();

                foreach (string filePath in imagePaths)
                {
                    Bitmap image = new Bitmap(filePath);
                    images.Add(image);
                }

                int pictureBoxWidth = 100;
                int pictureBoxHeight = 100;
                int pictureBoxSpacing = 10;

                int x = 0;
                int y = 0;

                foreach (Bitmap image in images)
                {
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Image = image;
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox.Size = new Size(pictureBoxWidth, pictureBoxHeight);
                    pictureBox.Location = new Point(x, y);

                    imageList.Controls.Add(pictureBox);

                    x += pictureBoxWidth + pictureBoxSpacing;

                    // If the PictureBox exceeds the Panel's width, move to the next row
                    if (x + pictureBoxWidth > imageList.Width)
                    {
                        x = 0;
                        y += pictureBoxHeight + pictureBoxSpacing;
                    }
                }
                */

            }
            string fullPath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;

            FileAssociations.SetAssociation(".png", "optimizedViewer", "Image File", fullPath);
            FileAssociations.SetAssociation(".jpg", "optimizedViewer", "Image File", fullPath);
            FileAssociations.SetAssociation(".jpeg", "optimizedViewer", "Image File", fullPath);
            FileAssociations.SetAssociation(".gif", "optimizedViewer", "Image File", fullPath);
            FileAssociations.SetAssociation(".ico", "optimizedViewer", "Image File", fullPath);
            FileAssociations.SetAssociation(".webp", "optimizedViewer", "Image File", fullPath);
            FileAssociations.SetAssociation(".tiff", "optimizedViewer", "Image File", fullPath);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F11:
                    isFullscreen = UICommands.toggleFullscreen(this, isFullscreen, MainTable);
                    break;
                case Keys.D:
                    currentImage = ImageHandler.LoadNextImage(pictureBox, currentImage, infoLabel);
                    break;
                case Keys.Right:
                    currentImage = ImageHandler.LoadNextImage(pictureBox, currentImage, infoLabel);
                    break;
                case Keys.A:
                    currentImage = ImageHandler.LoadPreviousImage(pictureBox, currentImage, infoLabel);
                    break;
                case Keys.Left:
                    currentImage = ImageHandler.LoadPreviousImage(pictureBox, currentImage, infoLabel);
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void topPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void exitBox_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void maximizeBox_Click(object sender, EventArgs e)
        {
            if (!maximized)
            {
                maximized = true;
                WindowState = FormWindowState.Maximized;
            }

            else
            {
                maximized = false;
                WindowState = FormWindowState.Normal;
            }
        }

        private void deleteBox_Click(object sender, EventArgs e)
        {
            currentImage = ImageHandler.deleteImages(currentImage, pictureBox, infoLabel);
        }

        private void rotateBox_Click(object sender, EventArgs e)
        {
            ImageHandler.RotateImageClockwise(pictureBox, currentImage);
        }

        private void favBox_Click(object sender, EventArgs e)
        {

        }

        private void exitBox_MouseEnter(object sender, EventArgs e)
        {
            exitBox.BorderStyle = BorderStyle.FixedSingle;
        }

        private void exitBox_MouseLeave(object sender, EventArgs e)
        {
            exitBox.BorderStyle = BorderStyle.None;
        }

        private void minimizeBox_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}