using optimizedPhotoViewer.Extensions;
using System.Runtime.InteropServices;

namespace optimizedPhotoViewer
{
    public partial class MainForm : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        public MainForm(string args)
        {
            InitializeComponent();
            if (args != null && File.Exists(args))
            {
                TempSettings.DefaultPath = args;
                ImageHandler.LoadImage(args, pictureBox, infoLabel);
                ImageHandler.GetImages();
                ImageHandler.GetCurrentIndex();

                string fullPath = Environment.ProcessPath;

                string[] fileExtensions = { ".png", ".jpg", ".jpeg", ".ico", ".tiff", ".bmp" };

                Parallel.ForEach(fileExtensions, extension =>
                {
                    FileAssociations.SetAssociation(extension, "optimizedViewer", "Image File", fullPath);
                });

                UICommands.DisplayImages(lowerPanel, pictureBox, infoLabel);
                BackgroundProcesser worker = new();
                worker.StartFunction();
            }
        }


        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            UICommands.DisplayImages(lowerPanel, pictureBox, infoLabel);
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F11:
                    UICommands.ToggleFullscreen(this, MainTable, lowerPanel, pictureBox, infoLabel, true);
                    break;
                case Keys.D:
                case Keys.Right:
                    UICommands.ScrollImage(pictureBox, infoLabel, true);
                    UICommands.DisplayImages(lowerPanel, pictureBox, infoLabel);
                    break;
                case Keys.A:
                case Keys.Left:
                    UICommands.ScrollImage(pictureBox, infoLabel, false);
                    UICommands.DisplayImages(lowerPanel, pictureBox, infoLabel);
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void ExitBox_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MaximizeBox_Click(object sender, EventArgs e)
        {
            UICommands.ToggleFullscreen(this, MainTable, lowerPanel, pictureBox, infoLabel, false);
        }

        private void DeleteBox_Click(object sender, EventArgs e)
        {
            ImageHandler.DeleteImages(pictureBox, infoLabel);
            UICommands.DisplayImages(lowerPanel, pictureBox, infoLabel);
        }

        private void RotateBox_Click(object sender, EventArgs e)
        {
            ImageHandler.RotateImageClockwise(pictureBox);
            UICommands.DisplayImages(lowerPanel, pictureBox, infoLabel);
        }

        private void FavBox_Click(object sender, EventArgs e)
        {

        }

        private void ExitBox_MouseEnter(object sender, EventArgs e)
        {
            if (sender is PictureBox pictureBox)
            {
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void ExitBox_MouseLeave(object sender, EventArgs e)
        {
            if (sender is PictureBox pictureBox)
            {
                pictureBox.BorderStyle = BorderStyle.None;
            }
        }

        private void MinimizeBox_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void FocusBox_Click(object sender, EventArgs e)
        {
            ImageHandler.LoadImage(TempSettings.CurrentImage, pictureBox, infoLabel);
        }

        private void ListFavorites_Click(object sender, EventArgs e)
        {

        }
    }
}