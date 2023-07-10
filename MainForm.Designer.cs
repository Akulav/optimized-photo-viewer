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
            imageList = new Panel();
            lowerPanel = new Panel();
            topPanel = new Panel();
            ListFavorites = new PictureBox();
            focusBox = new PictureBox();
            minimizeBox = new PictureBox();
            rotateBox = new PictureBox();
            deleteBox = new PictureBox();
            favBox = new PictureBox();
            maximizeBox = new PictureBox();
            exitBox = new PictureBox();
            infoLabel = new Label();
            pictureBox = new SQPhoto.SQPhoto();
            MainTable.SuspendLayout();
            imageList.SuspendLayout();
            topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ListFavorites).BeginInit();
            ((System.ComponentModel.ISupportInitialize)focusBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)minimizeBox).BeginInit();
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
            MainTable.Controls.Add(imageList, 0, 2);
            MainTable.Controls.Add(topPanel, 0, 0);
            MainTable.Controls.Add(pictureBox, 0, 1);
            MainTable.Dock = DockStyle.Fill;
            MainTable.Location = new Point(0, 0);
            MainTable.Name = "MainTable";
            MainTable.RowCount = 3;
            MainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            MainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 83.3333359F));
            MainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            MainTable.Size = new Size(1256, 703);
            MainTable.TabIndex = 0;
            // 
            // imageList
            // 
            imageList.BackColor = SystemColors.Desktop;
            imageList.Controls.Add(lowerPanel);
            imageList.Dock = DockStyle.Fill;
            imageList.Location = new Point(3, 599);
            imageList.Name = "imageList";
            imageList.Size = new Size(1250, 101);
            imageList.TabIndex = 3;
            // 
            // lowerPanel
            // 
            lowerPanel.Dock = DockStyle.Fill;
            lowerPanel.Location = new Point(0, 0);
            lowerPanel.Name = "lowerPanel";
            lowerPanel.Size = new Size(1250, 101);
            lowerPanel.TabIndex = 0;
            // 
            // topPanel
            // 
            topPanel.Controls.Add(ListFavorites);
            topPanel.Controls.Add(focusBox);
            topPanel.Controls.Add(minimizeBox);
            topPanel.Controls.Add(rotateBox);
            topPanel.Controls.Add(deleteBox);
            topPanel.Controls.Add(favBox);
            topPanel.Controls.Add(maximizeBox);
            topPanel.Controls.Add(exitBox);
            topPanel.Controls.Add(infoLabel);
            topPanel.Dock = DockStyle.Fill;
            topPanel.Location = new Point(3, 3);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(1250, 58);
            topPanel.TabIndex = 4;
            topPanel.MouseDown += TopPanel_MouseDown;
            // 
            // ListFavorites
            // 
            ListFavorites.Anchor = AnchorStyles.Top;
            ListFavorites.Image = Properties.Resources.list;
            ListFavorites.Location = new Point(697, 0);
            ListFavorites.Name = "ListFavorites";
            ListFavorites.Size = new Size(41, 49);
            ListFavorites.SizeMode = PictureBoxSizeMode.Zoom;
            ListFavorites.TabIndex = 9;
            ListFavorites.TabStop = false;
            ListFavorites.Click += ListFavorites_Click;
            ListFavorites.MouseLeave += ExitBox_MouseLeave;
            ListFavorites.MouseHover += ExitBox_MouseEnter;
            // 
            // focusBox
            // 
            focusBox.Anchor = AnchorStyles.Top;
            focusBox.Image = Properties.Resources.focus;
            focusBox.Location = new Point(651, 0);
            focusBox.Name = "focusBox";
            focusBox.Size = new Size(41, 49);
            focusBox.SizeMode = PictureBoxSizeMode.Zoom;
            focusBox.TabIndex = 8;
            focusBox.TabStop = false;
            focusBox.Click += FocusBox_Click;
            focusBox.MouseLeave += ExitBox_MouseLeave;
            focusBox.MouseHover += ExitBox_MouseEnter;
            // 
            // minimizeBox
            // 
            minimizeBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            minimizeBox.Image = Properties.Resources.minimize;
            minimizeBox.Location = new Point(1119, 0);
            minimizeBox.Name = "minimizeBox";
            minimizeBox.Size = new Size(41, 49);
            minimizeBox.SizeMode = PictureBoxSizeMode.Zoom;
            minimizeBox.TabIndex = 7;
            minimizeBox.TabStop = false;
            minimizeBox.Click += MinimizeBox_Click;
            minimizeBox.MouseLeave += ExitBox_MouseLeave;
            minimizeBox.MouseHover += ExitBox_MouseEnter;
            // 
            // rotateBox
            // 
            rotateBox.Anchor = AnchorStyles.Top;
            rotateBox.Image = Properties.Resources.rotate;
            rotateBox.Location = new Point(559, 0);
            rotateBox.Name = "rotateBox";
            rotateBox.Size = new Size(41, 49);
            rotateBox.SizeMode = PictureBoxSizeMode.Zoom;
            rotateBox.TabIndex = 6;
            rotateBox.TabStop = false;
            rotateBox.Click += RotateBox_Click;
            rotateBox.MouseLeave += ExitBox_MouseLeave;
            rotateBox.MouseHover += ExitBox_MouseEnter;
            // 
            // deleteBox
            // 
            deleteBox.Anchor = AnchorStyles.Top;
            deleteBox.Image = Properties.Resources.trashimg;
            deleteBox.Location = new Point(512, 0);
            deleteBox.Name = "deleteBox";
            deleteBox.Size = new Size(41, 49);
            deleteBox.SizeMode = PictureBoxSizeMode.Zoom;
            deleteBox.TabIndex = 5;
            deleteBox.TabStop = false;
            deleteBox.Click += DeleteBox_Click;
            deleteBox.MouseLeave += ExitBox_MouseLeave;
            deleteBox.MouseHover += ExitBox_MouseEnter;
            // 
            // favBox
            // 
            favBox.Anchor = AnchorStyles.Top;
            favBox.Image = Properties.Resources.fav;
            favBox.Location = new Point(605, 0);
            favBox.Name = "favBox";
            favBox.Size = new Size(41, 49);
            favBox.SizeMode = PictureBoxSizeMode.Zoom;
            favBox.TabIndex = 4;
            favBox.TabStop = false;
            favBox.Click += FavBox_Click;
            favBox.MouseLeave += ExitBox_MouseLeave;
            favBox.MouseHover += ExitBox_MouseEnter;
            // 
            // maximizeBox
            // 
            maximizeBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            maximizeBox.Image = Properties.Resources.maximize;
            maximizeBox.Location = new Point(1163, 0);
            maximizeBox.Name = "maximizeBox";
            maximizeBox.Size = new Size(41, 49);
            maximizeBox.SizeMode = PictureBoxSizeMode.Zoom;
            maximizeBox.TabIndex = 3;
            maximizeBox.TabStop = false;
            maximizeBox.Click += MaximizeBox_Click;
            maximizeBox.MouseLeave += ExitBox_MouseLeave;
            maximizeBox.MouseHover += ExitBox_MouseEnter;
            // 
            // exitBox
            // 
            exitBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            exitBox.Image = Properties.Resources.exit;
            exitBox.Location = new Point(1210, 0);
            exitBox.Name = "exitBox";
            exitBox.Size = new Size(41, 49);
            exitBox.SizeMode = PictureBoxSizeMode.Zoom;
            exitBox.TabIndex = 2;
            exitBox.TabStop = false;
            exitBox.Click += ExitBox_Click;
            exitBox.MouseLeave += ExitBox_MouseLeave;
            exitBox.MouseHover += ExitBox_MouseEnter;
            // 
            // infoLabel
            // 
            infoLabel.AutoSize = true;
            infoLabel.Font = new Font("Yu Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            infoLabel.ForeColor = Color.Gainsboro;
            infoLabel.Location = new Point(8, 15);
            infoLabel.Name = "infoLabel";
            infoLabel.Size = new Size(244, 21);
            infoLabel.TabIndex = 0;
            infoLabel.Text = "IMAGE_NAME_PLACEHOLDER";
            // 
            // pictureBox
            // 
            pictureBox.AutoReSize = true;
            pictureBox.BackColor = SystemColors.Desktop;
            pictureBox.CanMove = true;
            pictureBox.CanReSize = true;
            pictureBox.CanZoom = true;
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Image = Properties.Resources.default_bg;
            pictureBox.Location = new Point(4, 68);
            pictureBox.Margin = new Padding(4, 4, 4, 4);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(1248, 524);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 5;
            pictureBox.ZoomCenter = true;
            pictureBox.ZoomMin = 100;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowText;
            ClientSize = new Size(1256, 703);
            ControlBox = false;
            Controls.Add(MainTable);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PhotoViewer - By Akulav & map3x";
            ResizeEnd += MainForm_ResizeEnd;
            MainTable.ResumeLayout(false);
            imageList.ResumeLayout(false);
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ListFavorites).EndInit();
            ((System.ComponentModel.ISupportInitialize)focusBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)minimizeBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)rotateBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)deleteBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)favBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)maximizeBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)exitBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel MainTable;
        private Panel imageList;
        private Panel topPanel;
        private Label infoLabel;
        private PictureBox exitBox;
        private PictureBox maximizeBox;
        private PictureBox rotateBox;
        private PictureBox deleteBox;
        private PictureBox favBox;
        private PictureBox minimizeBox;
        private SQPhoto.SQPhoto pictureBox;
        private PictureBox ListFavorites;
        private PictureBox focusBox;
        private Panel lowerPanel;
    }
}