namespace search.Forms
{
    partial class Search
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Search));
            this.startDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.filePatternTextBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.continueButton = new System.Windows.Forms.Button();
            this.fileTreeView = new System.Windows.Forms.TreeView();
            this.currentDirectoryLabel = new System.Windows.Forms.Label();
            this.foundFilesLabel = new System.Windows.Forms.Label();
            this.totalFilesLabel = new System.Windows.Forms.Label();
            this.elapsedTimeLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // startDirectoryTextBox
            // 
            this.startDirectoryTextBox.Location = new System.Drawing.Point(12, 38);
            this.startDirectoryTextBox.Name = "startDirectoryTextBox";
            this.startDirectoryTextBox.Size = new System.Drawing.Size(400, 26);
            this.startDirectoryTextBox.TabIndex = 0;
            // 
            // filePatternTextBox
            // 
            this.filePatternTextBox.Location = new System.Drawing.Point(12, 105);
            this.filePatternTextBox.Name = "filePatternTextBox";
            this.filePatternTextBox.Size = new System.Drawing.Size(400, 26);
            this.filePatternTextBox.TabIndex = 1;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(420, 10);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(120, 35);
            this.searchButton.TabIndex = 2;
            this.searchButton.Text = "Поиск";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(420, 55);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(120, 35);
            this.stopButton.TabIndex = 3;
            this.stopButton.Text = "Остановить";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // continueButton
            // 
            this.continueButton.Location = new System.Drawing.Point(420, 97);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(120, 35);
            this.continueButton.TabIndex = 4;
            this.continueButton.Text = "Продолжить";
            this.continueButton.UseVisualStyleBackColor = true;
            this.continueButton.Click += new System.EventHandler(this.ContinueButton_Click);
            // 
            // fileTreeView
            // 
            this.fileTreeView.Location = new System.Drawing.Point(12, 154);
            this.fileTreeView.Name = "fileTreeView";
            this.fileTreeView.Size = new System.Drawing.Size(528, 489);
            this.fileTreeView.TabIndex = 5;
            // 
            // currentDirectoryLabel
            // 
            this.currentDirectoryLabel.Location = new System.Drawing.Point(10, 680);
            this.currentDirectoryLabel.Name = "currentDirectoryLabel";
            this.currentDirectoryLabel.Size = new System.Drawing.Size(480, 23);
            this.currentDirectoryLabel.TabIndex = 6;
            // 
            // foundFilesLabel
            // 
            this.foundFilesLabel.Location = new System.Drawing.Point(10, 710);
            this.foundFilesLabel.Name = "foundFilesLabel";
            this.foundFilesLabel.Size = new System.Drawing.Size(480, 23);
            this.foundFilesLabel.TabIndex = 7;
            // 
            // totalFilesLabel
            // 
            this.totalFilesLabel.Location = new System.Drawing.Point(10, 740);
            this.totalFilesLabel.Name = "totalFilesLabel";
            this.totalFilesLabel.Size = new System.Drawing.Size(480, 23);
            this.totalFilesLabel.TabIndex = 8;
            // 
            // elapsedTimeLabel
            // 
            this.elapsedTimeLabel.Location = new System.Drawing.Point(10, 770);
            this.elapsedTimeLabel.Name = "elapsedTimeLabel";
            this.elapsedTimeLabel.Size = new System.Drawing.Size(480, 23);
            this.elapsedTimeLabel.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Стартовая директория";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Шаблон имени файла";
            // 
            // Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 902);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startDirectoryTextBox);
            this.Controls.Add(this.filePatternTextBox);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.fileTreeView);
            this.Controls.Add(this.currentDirectoryLabel);
            this.Controls.Add(this.foundFilesLabel);
            this.Controls.Add(this.totalFilesLabel);
            this.Controls.Add(this.elapsedTimeLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Search";
            this.Text = "Поиск файлов";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SearchForm_FormClosing);
            this.Load += new System.EventHandler(this.SearchForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.TextBox startDirectoryTextBox;
        private System.Windows.Forms.TextBox filePatternTextBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.TreeView fileTreeView;
        private System.Windows.Forms.Label currentDirectoryLabel;
        private System.Windows.Forms.Label foundFilesLabel;
        private System.Windows.Forms.Label totalFilesLabel;
        private System.Windows.Forms.Label elapsedTimeLabel;

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}