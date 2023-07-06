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
            splitContainer1 = new SplitContainer();
            rotateButton = new Button();
            deleteButton = new Button();
            MainTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // MainTable
            // 
            MainTable.ColumnCount = 1;
            MainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            MainTable.Controls.Add(pictureBox, 0, 1);
            MainTable.Controls.Add(splitContainer1, 0, 0);
            MainTable.Dock = DockStyle.Fill;
            MainTable.Location = new Point(0, 0);
            MainTable.Name = "MainTable";
            MainTable.RowCount = 3;
            MainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            MainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 85F));
            MainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            MainTable.Size = new Size(1256, 550);
            MainTable.TabIndex = 0;
            MainTable.MouseDown += MainTable_MouseDown;
            // 
            // pictureBox
            // 
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Image = Properties.Resources.hentai_default_bg;
            pictureBox.InitialImage = null;
            pictureBox.Location = new Point(3, 30);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(1250, 461);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // splitContainer1
            // 
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(rotateButton);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(deleteButton);
            splitContainer1.Size = new Size(486, 21);
            splitContainer1.SplitterDistance = 216;
            splitContainer1.TabIndex = 2;
            // 
            // rotateButton
            // 
            rotateButton.Location = new Point(0, 0);
            rotateButton.Name = "rotateButton";
            rotateButton.Size = new Size(213, 21);
            rotateButton.TabIndex = 0;
            rotateButton.Text = "Rotate";
            rotateButton.UseVisualStyleBackColor = true;
            rotateButton.Click += rotateButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.BackgroundImage = (Image)resources.GetObject("deleteButton.BackgroundImage");
            deleteButton.Location = new Point(62, 0);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(120, 21);
            deleteButton.TabIndex = 1;
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += delete_button_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
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
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel MainTable;
        private PictureBox pictureBox;
        private Button deleteButton;
        private SplitContainer splitContainer1;
        private Button rotateButton;
    }
}