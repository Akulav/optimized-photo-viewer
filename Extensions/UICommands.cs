using System.Collections.Concurrent;
using System.Drawing.Drawing2D;

namespace optimizedPhotoViewer.Extensions
{
    public static class UICommands
    {
        public static void ToggleFullscreen(Form form, TableLayoutPanel panel, Panel lowerPanel, SQPhoto.SQPhoto pictureBox, Label info, bool f11)
        {
            form.WindowState = TempSettings.IsFullscreen ? FormWindowState.Normal : FormWindowState.Maximized;

            if (TempSettings.IsFullscreen)
            {
                panel.RowStyles.Clear();
                panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
                panel.RowStyles.Add(new RowStyle(SizeType.Percent, 85)); 
                panel.RowStyles.Add(new RowStyle(SizeType.Percent, 15)); 

                DisplayImages(lowerPanel, pictureBox, info);

                form.TopMost = false;
                form.FormBorderStyle = FormBorderStyle.Sizable;
                form.WindowState = FormWindowState.Normal;

            }
            else
            {
                panel.RowStyles.Clear();
                panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60)); 
                panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                panel.RowStyles.Add(new RowStyle(SizeType.Percent, 0)); 

                ClearPreview(lowerPanel);

                if (f11)
                {
                    form.TopMost = true;
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.WindowState = FormWindowState.Maximized;
                }
            }
            TempSettings.IsFullscreen = !TempSettings.IsFullscreen;
        }



        public static void ScrollImage(SQPhoto.SQPhoto pictureBox, Label info, bool next)
        {
            int imagesLength = TempSettings.AllPaths.Length;
            TempSettings.CurrentIndex = next ? (TempSettings.CurrentIndex + 1) % imagesLength : (TempSettings.CurrentIndex - 1 + imagesLength) % imagesLength;
            ImageHandler.LoadImage(TempSettings.AllPaths[TempSettings.CurrentIndex], pictureBox, info);
            TempSettings.CurrentImage = TempSettings.AllPaths[TempSettings.CurrentIndex];
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
            int spacing = 10;
            int pictureBoxHeight = panelHeight;

            ConcurrentDictionary<string, Size> imageDimensions = new();

            int totalSpacing = spacing * (imagePaths.Count - 1);
            int totalPictureBoxWidth = 0;

            Parallel.ForEach(imagePaths, imagePath =>
            {
                Size dimensions = GetImageDimensions(imagePath);
                imageDimensions.TryAdd(imagePath, dimensions);

                int pictureBoxWidth = dimensions.Width / (dimensions.Height / pictureBoxHeight);
                Interlocked.Add(ref totalPictureBoxWidth, pictureBoxWidth);
            });

            int startX = (panelWidth - (totalPictureBoxWidth + totalSpacing)) / 2;

            ConcurrentDictionary<int, PictureBox> pictureBoxes = new();

            Parallel.ForEach(imagePaths, (imagePath, state, index) =>
            {
                Size dimensions = imageDimensions[imagePath];
                int pictureBoxWidth = dimensions.Width / (dimensions.Height / pictureBoxHeight);

                PictureBox pictureBox = CreatePictureBox(imagePath, pictureBoxHeight, pictureBoxWidth, mainPicture, info);

                pictureBoxes.TryAdd((int)index, pictureBox);
            });

            int totalWidth = totalPictureBoxWidth + totalSpacing;

            IEnumerable<PictureBox> sortedPictureBoxes = pictureBoxes.OrderBy(kv => startX + (int)kv.Key * (totalWidth / imagePaths.Count)).Select(kv => kv.Value);

            foreach (PictureBox pictureBox in sortedPictureBoxes)
            {
                pictureBox.Location = new Point(startX, (panelHeight - pictureBox.Height) / 2);
                panel.Controls.Add(pictureBox);

                startX += pictureBox.Width + spacing;
            }
        }



        private static Size GetImageDimensions(string imagePath)
        {
            using Image originalImage = Image.FromFile(imagePath);
            return originalImage.Size;
        }

        private static PictureBox CreatePictureBox(string imagePath, int pictureBoxHeight, int pictureBoxWidth, SQPhoto.SQPhoto mainPicture, Label info)
        {
            PictureBox pictureBox = new()
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Name = imagePath,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Top
            };

            pictureBox.Click += (sender, e) =>
            {
                TempSettings.CurrentImage = pictureBox.Name;
                ImageHandler.LoadImage(pictureBox.Name, mainPicture, info);
                DisplayImages(pictureBox.Parent as Panel, mainPicture, info);
            };

            using (Image originalImage = Image.FromFile(imagePath))
            {
                Bitmap previewBitmap = new(pictureBoxWidth, pictureBoxHeight);
                using (Graphics graphics = Graphics.FromImage(previewBitmap))
                {
                    graphics.InterpolationMode = InterpolationMode.Low;
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.SmoothingMode = SmoothingMode.HighSpeed;
                    graphics.DrawImage(originalImage, 0, 0, pictureBoxWidth, pictureBoxHeight);
                    if (imagePath == TempSettings.CurrentImage)
                    {
                        int borderWidth = 5;
                        graphics.DrawRectangle(new Pen(Color.White, borderWidth), borderWidth / 2, borderWidth / 2, pictureBoxWidth - borderWidth, pictureBoxHeight - borderWidth);
                    }
                }

                pictureBox.Size = new Size(pictureBoxWidth, pictureBoxHeight);
                pictureBox.Image = previewBitmap;
            }

            return pictureBox;
        }
    }
}
