namespace optimizedPhotoViewer
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            MainTable = new TableLayoutPanel();
            pictureBox = new PictureBox();
            imageList = new Panel();
            topPanel = new Panel();
            rotateBox = new PictureBox();
            deleteBox = new PictureBox();
            favBox = new PictureBox();
            maximizeBox = new PictureBox();
            exitBox = new PictureBox();
            infoLabel = new Label();
            MainTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)rotateBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)deleteBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)favBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)maximizeBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)exitBox).BeginInit();
            SuspendLayout();
            // 
            // MainTable
            // 
            MainTable.ColumnCount = 1;
            MainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            MainTable.Controls.Add(pictureBox, 0, 1);
            MainTable.Controls.Add(imageList, 0, 2);
            MainTable.Controls.Add(topPanel, 0, 0);
            MainTable.Dock = DockStyle.Fill;
            MainTable.Location = new Point(0, 0);
            MainTable.Name = "MainTable";
            MainTable.RowCount = 3;
            MainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            MainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            MainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            MainTable.Size = new Size(1256, 550);
            MainTable.TabIndex = 0;
            // 
            // pictureBox
            // 
            pictureBox.BackColor = SystemColors.Desktop;
            pictureBox.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Image = Properties.Resources.hentai_default_bg;
            pictureBox.InitialImage = null;
            pictureBox.Location = new Point(3, 58);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(1250, 434);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // imageList
            // 
            imageList.BackColor = SystemColors.Desktop;
            imageList.Dock = DockStyle.Fill;
            imageList.Location = new Point(3, 498);
            imageList.Name = "imageList";
            imageList.Size = new Size(1250, 49);
            imageList.TabIndex = 3;
            // 
            // topPanel
            // 
            topPanel.Controls.Add(rotateBox);
            topPanel.Controls.Add(deleteBox);
            topPanel.Controls.Add(favBox);
            topPanel.Controls.Add(maximizeBox);
            topPanel.Controls.Add(exitBox);
            topPanel.Controls.Add(infoLabel);
            topPanel.Dock = DockStyle.Fill;
            topPanel.Location = new Point(3, 3);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(1250, 49);
            topPanel.TabIndex = 4;
            topPanel.MouseDown += topPanel_MouseDown;
            // 
            // rotateBox
            // 
            rotateBox.Anchor = AnchorStyles.Top;
            rotateBox.Image = Properties.Resources.rotate;
            rotateBox.Location = new Point(559, 0);
            rotateBox.Name = "rotateBox";
            rotateBox.Size = new Size(40, 49);
            rotateBox.SizeMode = PictureBoxSizeMode.Zoom;
            rotateBox.TabIndex = 6;
            rotateBox.TabStop = false;
            rotateBox.Click += rotateBox_Click;
            // 
            // deleteBox
            // 
            deleteBox.Anchor = AnchorStyles.Top;
            deleteBox.Image = Properties.Resources.trashimg;
            deleteBox.Location = new Point(651, 0);
            deleteBox.Name = "deleteBox";
            deleteBox.Size = new Size(40, 49);
            deleteBox.SizeMode = PictureBoxSizeMode.Zoom;
            deleteBox.TabIndex = 5;
            deleteBox.TabStop = false;
            deleteBox.Click += deleteBox_Click;
            // 
            // favBox
            // 
            favBox.Anchor = AnchorStyles.Top;
            favBox.Image = Properties.Resources.fav;
            favBox.Location = new Point(605, 0);
            favBox.Name = "favBox";
            favBox.Size = new Size(40, 49);
            favBox.SizeMode = PictureBoxSizeMode.Zoom;
            favBox.TabIndex = 4;
            favBox.TabStop = false;
            favBox.Click += favBox_Click;
            // 
            // maximizeBox
            // 
            maximizeBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            maximizeBox.Image = Properties.Resources.maximize;
            maximizeBox.Location = new Point(1164, 0);
            maximizeBox.Name = "maximizeBox";
            maximizeBox.Size = new Size(40, 49);
            maximizeBox.SizeMode = PictureBoxSizeMode.Zoom;
            maximizeBox.TabIndex = 3;
            maximizeBox.TabStop = false;
            maximizeBox.Click += maximizeBox_Click;
            // 
            // exitBox
            // 
            exitBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            exitBox.Image = Properties.Resources.exit;
            exitBox.Location = new Point(1210, 0);
            exitBox.Name = "exitBox";
            exitBox.Size = new Size(40, 49);
            exitBox.SizeMode = PictureBoxSizeMode.Zoom;
            exitBox.TabIndex = 2;
            exitBox.TabStop = false;
            exitBox.Click += exitBox_Click;
            exitBox.MouseLeave += exitBox_MouseLeave;
            exitBox.MouseHover += exitBox_MouseHover;
            // 
            // infoLabel
            // 
            infoLabel.AutoSize = true;
            infoLabel.Font = new Font("Yu Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            infoLabel.ForeColor = Color.Gainsboro;
            infoLabel.Location = new Point(9, 15);
            infoLabel.Name = "infoLabel";
            infoLabel.Size = new Size(244, 21);
            infoLabel.TabIndex = 0;
            infoLabel.Text = "IMAGE_NAME_PLACEHOLDER";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowText;
            ClientSize = new Size(1256, 550);
            ControlBox = false;
            Controls.Add(MainTable);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "PhotoViewer - By Akulav & map3x";
            MainTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)rotateBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)deleteBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)favBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)maximizeBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)exitBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel MainTable;
        private PictureBox pictureBox;
        private Panel imageList;
        private Panel topPanel;
        private Label infoLabel;
        private PictureBox exitBox;
        private PictureBox maximizeBox;
        private PictureBox rotateBox;
        private PictureBox deleteBox;
        private PictureBox favBox;
    }
}