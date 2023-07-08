using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace optimizedPhotoViewer.Extensions
{
    public static class UICommands
    {
        public static void ToggleFullscreen(Form form, TableLayoutPanel panel, Panel lowerPanel, SQPhoto.SQPhoto pictureBox, Label info)
        {
            int rowCount = 3;
            form.WindowState = TempSettings.isFullscreen ? FormWindowState.Normal : FormWindowState.Maximized;
            float[] percentage;
            if (TempSettings.isFullscreen)
            {
                percentage = new float[] { 0.1f, 0.75f, 0.15f };
                ClearPreview(lowerPanel);
            }

            else
            {
                percentage = new float[] { 0.04f, 0.96f, 0.00f };
                DisplayImages(lowerPanel, pictureBox, info);
            }

            for (int i = 0; i < rowCount; i++)
            {
                panel.RowStyles[i] = new RowStyle(SizeType.Percent, percentage[i]);
            }
            TempSettings.isFullscreen = !TempSettings.isFullscreen;
        }

        public static void ClearPreview(Panel panel)
        {
            panel.Controls.Clear();
            GC.Collect();
        }


        public static void DisplayImages(Panel panel, SQPhoto.SQPhoto mainPicture, Label info)
        {
            List<string> imagePaths = ImageHandler.GetStringsInRange();
            ClearPreview(panel);

            int panelWidth = panel.Width;
            int panelHeight = panel.Height;
            int pictureBoxWidth = 80;
            int pictureBoxHeight = panelHeight;
            int spacing = 10;
            int totalWidth = (pictureBoxWidth + spacing) * imagePaths.Count - spacing;
            int startX = (panelWidth - totalWidth) / 2;

            foreach (string imagePath in imagePaths)
            {

                PictureBox pictureBox = new()
                {
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Size = new Size(pictureBoxWidth, pictureBoxHeight),
                    Name = imagePath
                };

                pictureBox.Click += (sender, e) =>
                {
                    TempSettings.currentImage = pictureBox.Name;
                    ImageHandler.loadImage(pictureBox.Name, mainPicture, info);
                    DisplayImages(panel, mainPicture, info);
                };


                using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        fs.CopyTo(ms);
                        ms.Seek(0, SeekOrigin.Begin);
                        Image originalImage = Image.FromStream(ms);

                        // Create a new Bitmap with the desired size for the preview
                        Bitmap previewBitmap = new Bitmap(pictureBoxWidth, pictureBoxHeight);

                        // Create a Graphics object from the preview Bitmap
                        using (Graphics graphics = Graphics.FromImage(previewBitmap))
                        {
                            // Set the interpolation mode for resizing
                            graphics.InterpolationMode = InterpolationMode.Low;
                            graphics.CompositingQuality = CompositingQuality.HighSpeed;
                            graphics.SmoothingMode = SmoothingMode.HighSpeed;
                            // Draw the original image onto the preview Bitmap with the desired size
                            graphics.DrawImage(originalImage, 0, 0, pictureBoxWidth, pictureBoxHeight);
                        }

                        // Assign the preview Bitmap to the PictureBox
                        pictureBox.Image = previewBitmap;

                        // Dispose the original image
                        originalImage.Dispose();
                    }
                }

                int x = startX + (pictureBoxWidth + spacing) * panel.Controls.Count;
                int y = (panelHeight - pictureBoxHeight) / 2;
                pictureBox.Location = new Point(x, y);
                panel.Controls.Add(pictureBox);
            }

        }
    }
}
