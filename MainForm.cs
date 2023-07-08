using optimizedPhotoViewer.Extensions;
using System.Runtime.InteropServices;
using Timer = System.Windows.Forms.Timer;

namespace optimizedPhotoViewer
{
    public partial class MainForm : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        private Timer resizeTimer;

        public MainForm(string args)
        {
            InitializeComponent();
            if (args != null && File.Exists(args))
            {
                TempSettings.defaultPath = args;
                ImageHandler.loadImage(args, pictureBox, infoLabel);
                ImageHandler.getImages();
                ImageHandler.getCurrentIndex();
            }
            string fullPath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;

            string[] fileExtensions = { ".png", ".jpg", ".jpeg", ".gif", ".ico", ".tiff", ".bmp" };
            foreach (string extension in fileExtensions)
            {
                FileAssociations.SetAssociation(extension, "optimizedViewer", "Image File", fullPath);
            }

            UICommands.DisplayImages(lowerPanel, pictureBox, infoLabel);
            BackgroundProcesser worker = new BackgroundProcesser();
            worker.StartFunction();

            resizeTimer = new Timer();
            resizeTimer.Interval = 200; // Set the interval according to your needs
            resizeTimer.Tick += ResizeTimer_Tick;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F11:
                    UICommands.ToggleFullscreen(this, MainTable, lowerPanel, pictureBox, infoLabel);
                    break;
                case Keys.D:
                case Keys.Right:
                    ImageHandler.scrollImage(pictureBox, infoLabel, true);
                    UICommands.DisplayImages(lowerPanel, pictureBox, infoLabel);
                    break;
                case Keys.A:
                case Keys.Left:
                    ImageHandler.scrollImage(pictureBox, infoLabel, false);
                    UICommands.DisplayImages(lowerPanel, pictureBox, infoLabel);
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
            UICommands.ToggleFullscreen(this, MainTable, lowerPanel, pictureBox, infoLabel);
        }

        private void deleteBox_Click(object sender, EventArgs e)
        {
            ImageHandler.deleteImages(pictureBox, infoLabel);
            UICommands.DisplayImages(lowerPanel, pictureBox, infoLabel);
        }

        private void rotateBox_Click(object sender, EventArgs e)
        {
            ImageHandler.RotateImageClockwise(pictureBox);
            UICommands.DisplayImages(lowerPanel, pictureBox, infoLabel);
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

        private void focusBox_Click(object sender, EventArgs e)
        {
            ImageHandler.loadImage(TempSettings.currentImage, pictureBox, infoLabel);
        }

        private void lowerPanel_Resize(object sender, EventArgs e)
        {
            resizeTimer.Start();
        }
        private void ResizeTimer_Tick(object sender, EventArgs e)
        {
            resizeTimer.Stop(); // Stop the timer
            UICommands.DisplayImages(lowerPanel, pictureBox, infoLabel);
        }

    }
}