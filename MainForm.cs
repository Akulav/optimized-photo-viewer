using optimizedPhotoViewer.Extensions;
using System.Runtime.InteropServices;

namespace optimizedPhotoViewer
{
    public partial class MainForm : Form
    {
        private string[] imagePaths;
        private int currentIndex;
        private bool isFullscreen;

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
                pictureBox.Image = new Bitmap(args);
                imagePaths = ImageHandler.getImages(args);
                currentIndex = ImageHandler.getCurrentIndex(args);
            }
            string fullPath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;

            FileAssociations.SetAssociation(".png", "optimizedViewer", "Image File", fullPath);
            FileAssociations.SetAssociation(".jpg", "optimizedViewer", "Image File", fullPath);
            FileAssociations.SetAssociation(".jpeg", "optimizedViewer", "Image File", fullPath);
            FileAssociations.SetAssociation(".gif", "optimizedViewer", "Image File", fullPath);
            FileAssociations.SetAssociation(".ico", "optimizedViewer", "Image File", fullPath);
            FileAssociations.SetAssociation(".webp", "optimizedViewer", "Image File", fullPath);
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            currentIndex = ImageHandler.deleteImages(imagePaths[currentIndex], pictureBox);
        }

        private void rotateButton_Click(object sender, EventArgs e)
        {
            ImageHandler.RotateImageClockwise(pictureBox, currentIndex, imagePaths[0]);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F11:
                    isFullscreen = UICommands.toggleFullscreen(this, isFullscreen, MainTable);
                    break;
                case Keys.D:
                    currentIndex = ImageHandler.LoadNextImage(currentIndex, pictureBox, imagePaths[0]);
                    break;
                case Keys.Right:
                    currentIndex = ImageHandler.LoadNextImage(currentIndex, pictureBox, imagePaths[0]);
                    break;
                case Keys.A:
                    currentIndex = ImageHandler.LoadPreviousImage(currentIndex, pictureBox, imagePaths[0]);
                    break;
                case Keys.Left:
                    currentIndex = ImageHandler.LoadPreviousImage(currentIndex, pictureBox, imagePaths[0]);
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void MainTable_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }
    }
}