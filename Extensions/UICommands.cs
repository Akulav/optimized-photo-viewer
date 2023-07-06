namespace optimizedPhotoViewer.Extensions
{
    public static class UICommands
    {
        public static bool toggleFullscreen(Form form, bool isFullscreen, TableLayoutPanel panel)
        {
            int rowCount = 3;
            if (isFullscreen)
            {
                form.WindowState = FormWindowState.Normal;

                float[] percentage = new float[] { 0.1f, 0.8f, 0.10f };

                for (int i = 0; i < rowCount; i++)
                {
                    panel.RowStyles[i] = new RowStyle(SizeType.Percent, percentage[i]);
                }

                return false;
            }
            else
            {
                form.WindowState = FormWindowState.Maximized;

                float[] percentage = new float[] { 0.1f, 0.9f, 0.0f };

                for (int i = 0; i < rowCount; i++)
                {
                    panel.RowStyles[i] = new RowStyle(SizeType.Percent, percentage[i]);
                }
                return true;
            }
        }
    }
}
