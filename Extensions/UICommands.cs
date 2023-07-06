﻿namespace optimizedPhotoViewer.Extensions
{
    public static class UICommands
    {
        public static bool toggleFullscreen(Form form, bool isFullscreen)
        {
            if (isFullscreen)
            {
                form.FormBorderStyle = FormBorderStyle.Sizable;
                form.WindowState = FormWindowState.Normal;
                return false;
            }
            else
            {
                form.FormBorderStyle = FormBorderStyle.None;
                form.WindowState = FormWindowState.Maximized;
                return true;
            }
        }

        public static void LoadImage(PictureBox pictureBox, string path) 
        {
            pictureBox.Image?.Dispose();
            pictureBox.Image = new Bitmap(path);
        }
    }
}
