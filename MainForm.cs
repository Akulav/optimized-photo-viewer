using optimizedPhotoViewer.Extensions;

namespace optimizedPhotoViewer
{
    public partial class MainForm : Form
    {
        private string[] imagePaths;
        private int currentIndex;
        private bool isFullscreen;

        public MainForm(string args)
        {
            InitializeComponent();
            DoubleBuffered = true;
            if (args != null && File.Exists(args))
            {
                pictureBox.Image = new Bitmap(args);
                imagePaths = FileHandler.LoadImagePaths(args);
                currentIndex = Array.IndexOf(imagePaths, args);
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
            FileHandler.DeleteCurrentImage(imagePaths, currentIndex, pictureBox);

            if (imagePaths.Length > 0)
            {
                if (currentIndex + 1 >= imagePaths.Length)
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

        private void rotateButton_Click(object sender, EventArgs e)
        {
            FileHandler.RotateImageClockwise(pictureBox);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            deleteButton.FlatStyle = FlatStyle.Flat;
            deleteButton.FlatAppearance.BorderSize = 0;

            rotateButton.FlatStyle = FlatStyle.Flat;
            rotateButton.FlatAppearance.BorderSize = 0;

            previousImage.FlatStyle = FlatStyle.Flat;
            previousImage.FlatAppearance.BorderSize = 0;

            nextImage.FlatStyle = FlatStyle.Flat;
            nextImage.FlatAppearance.BorderSize = 0;
        }

        private void previousImage_MouseEnter(object sender, EventArgs e)
        {
            previousImage.Size = new Size(235, 73);
            previousImage.BackgroundImage = Properties.Resources.Megumin_LeftUP;
        }

        private void previousImage_MouseLeave(object sender, EventArgs e)
        {
            previousImage.Size = new Size(139, 49);
            previousImage.BackgroundImage = Properties.Resources.Megumin_Left;
        }

        private void nextImage_MouseEnter(object sender, EventArgs e)
        {
            nextImage.Size = new Size(235, 73);
            nextImage.BackgroundImage = Properties.Resources.Megumin_RightUP;
        }

        private void nextImage_MouseLeave(object sender, EventArgs e)
        {
            nextImage.Size = new Size(139, 49);
            nextImage.BackgroundImage = Properties.Resources.Megumin_RIght;
        }

        private void previousImage_Click(object sender, EventArgs e)
        {
            FileHandler.LoadPreviousImage(imagePaths, ref currentIndex, LoadImage, pictureBox);
        }

        private void nextImage_Click(object sender, EventArgs e)
        {
            FileHandler.LoadNextImage(imagePaths, ref currentIndex, LoadImage, pictureBox);
        }

        private void LoadImage(string imagePath)
        {
            pictureBox.Image?.Dispose();
            pictureBox.Image = new Bitmap(imagePath);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F11)
            {
                isFullscreen = UICommands.toggleFullscreen(this, isFullscreen);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}