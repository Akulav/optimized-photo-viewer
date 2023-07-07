namespace optimizedPhotoViewer.Extensions
{
    public static class UICommands
    {
        public static bool toggleFullscreen(Form form, bool isFullscreen, TableLayoutPanel panel)
        {
            int rowCount = 3;
            form.WindowState = isFullscreen ? FormWindowState.Normal : FormWindowState.Maximized;

            float[] percentage = isFullscreen ? new float[] { 0.1f, 0.75f, 0.15f } : new float[] { 0.04f, 0.96f, 0.00f };

            for (int i = 0; i < rowCount; i++)
            {
                panel.RowStyles[i] = new RowStyle(SizeType.Percent, percentage[i]);
            }

            return !isFullscreen;
        }

        public static void DisplayImages(List<string> imagePaths, Panel panel)
        {
            panel.SuspendLayout();
            panel.Controls.Clear();
            GC.Collect();

            int panelWidth = panel.Width;
            int panelHeight = panel.Height;
            int pictureBoxWidth = 64; // Set the desired width of the PictureBox
            int pictureBoxHeight = panelHeight - 15; // Set the desired height of the PictureBox
            int spacing = 10; // Set the desired spacing between PictureBoxes

            int totalWidth = (pictureBoxWidth + spacing) * imagePaths.Count - spacing;
            int startX = (panelWidth - totalWidth) / 2;

            foreach (string imagePath in imagePaths)
            {
                PictureBox pictureBox = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Image = new Bitmap(imagePath),
                    Size = new Size(pictureBoxWidth, pictureBoxHeight),
                };

                int x = startX + (pictureBoxWidth + spacing) * panel.Controls.Count;
                int y = (panelHeight - pictureBoxHeight) / 2;
                pictureBox.Location = new Point(x, y);

                panel.Controls.Add(pictureBox);
            }

            panel.ResumeLayout();
        }
    }
}
