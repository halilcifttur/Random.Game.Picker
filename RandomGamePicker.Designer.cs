namespace RandomGameSelector
{
    partial class RandomGamePicker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RandomGamePicker));
            selectFolderBtn = new Button();
            selectRandGameBtn = new Button();
            folderBrowserDialog = new FolderBrowserDialog();
            selectedFolderLblTextBox = new TextBox();
            selectedGameLbl = new Label();
            openSelectedGameBtn = new Button();
            selectedGameIcon = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)selectedGameIcon).BeginInit();
            SuspendLayout();
            // 
            // selectFolderBtn
            // 
            selectFolderBtn.Font = new Font("Arial Rounded MT Bold", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            selectFolderBtn.Location = new Point(206, 204);
            selectFolderBtn.Name = "selectFolderBtn";
            selectFolderBtn.Size = new Size(56, 36);
            selectFolderBtn.TabIndex = 1;
            selectFolderBtn.Text = "Select Folder";
            selectFolderBtn.UseVisualStyleBackColor = true;
            selectFolderBtn.Click += selectFolderBtn_Click;
            // 
            // selectRandGameBtn
            // 
            selectRandGameBtn.Font = new Font("Arial Rounded MT Bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            selectRandGameBtn.Location = new Point(12, 246);
            selectRandGameBtn.Name = "selectRandGameBtn";
            selectRandGameBtn.Size = new Size(250, 33);
            selectRandGameBtn.TabIndex = 3;
            selectRandGameBtn.Text = "Select Random Game";
            selectRandGameBtn.UseVisualStyleBackColor = true;
            selectRandGameBtn.Click += selectRandGameBtn_Click;
            // 
            // selectedFolderLblTextBox
            // 
            selectedFolderLblTextBox.Location = new Point(12, 211);
            selectedFolderLblTextBox.Name = "selectedFolderLblTextBox";
            selectedFolderLblTextBox.ReadOnly = true;
            selectedFolderLblTextBox.Size = new Size(188, 23);
            selectedFolderLblTextBox.TabIndex = 5;
            // 
            // selectedGameLbl
            // 
            selectedGameLbl.AutoSize = true;
            selectedGameLbl.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            selectedGameLbl.Location = new Point(12, 9);
            selectedGameLbl.Name = "selectedGameLbl";
            selectedGameLbl.Size = new Size(131, 21);
            selectedGameLbl.TabIndex = 7;
            selectedGameLbl.Text = "Selected Game :";
            // 
            // openSelectedGameBtn
            // 
            openSelectedGameBtn.Font = new Font("Arial Rounded MT Bold", 10F);
            openSelectedGameBtn.Location = new Point(13, 135);
            openSelectedGameBtn.Name = "openSelectedGameBtn";
            openSelectedGameBtn.Size = new Size(250, 33);
            openSelectedGameBtn.TabIndex = 8;
            openSelectedGameBtn.UseVisualStyleBackColor = true;
            openSelectedGameBtn.Click += openSelectedGameBtn_Click;
            // 
            // selectedGameIcon
            // 
            selectedGameIcon.BackColor = SystemColors.ButtonShadow;
            selectedGameIcon.BackgroundImageLayout = ImageLayout.Center;
            selectedGameIcon.Location = new Point(86, 33);
            selectedGameIcon.Name = "selectedGameIcon";
            selectedGameIcon.Size = new Size(96, 96);
            selectedGameIcon.SizeMode = PictureBoxSizeMode.CenterImage;
            selectedGameIcon.TabIndex = 9;
            selectedGameIcon.TabStop = false;
            // 
            // RandomGamePicker
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(275, 286);
            Controls.Add(selectedGameIcon);
            Controls.Add(openSelectedGameBtn);
            Controls.Add(selectedGameLbl);
            Controls.Add(selectedFolderLblTextBox);
            Controls.Add(selectRandGameBtn);
            Controls.Add(selectFolderBtn);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "RandomGamePicker";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Random Game Picker";
            ((System.ComponentModel.ISupportInitialize)selectedGameIcon).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button selectFolderBtn;
        private Button selectRandGameBtn;
        private FolderBrowserDialog folderBrowserDialog;
        private TextBox selectedFolderLblTextBox;
        private Label selectedGameLbl;
        private Button openSelectedGameBtn;
        private PictureBox selectedGameIcon;
    }
}
