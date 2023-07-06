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
            }
            string fullPath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;

            string[] fileExtensions = { ".png", ".jpg", ".jpeg", ".gif", ".ico", ".webp", ".tiff" };
            foreach (string extension in fileExtensions)
            {
                FileAssociations.SetAssociation(extension, "optimizedViewer", "Image File", fullPath);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F11:
                    isFullscreen = UICommands.toggleFullscreen(this, isFullscreen, MainTable);
                    break;
                case Keys.D:
                case Keys.Right:
                    currentImage = ImageHandler.scrollImage(pictureBox, currentImage, infoLabel, true);
                    break;
                case Keys.A:
                case Keys.Left:
                    currentImage = ImageHandler.scrollImage(pictureBox, currentImage, infoLabel, false);
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