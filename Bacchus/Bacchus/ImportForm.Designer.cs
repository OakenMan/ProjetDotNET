namespace Bacchus
{
    partial class ImportForm
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
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.OpenFileButton = new System.Windows.Forms.Button();
            this.FileTextBox = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.OverwriteDataButton = new System.Windows.Forms.Button();
            this.AppendDataButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(3, 76);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(551, 23);
            this.ProgressBar.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel4);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel3);
            this.flowLayoutPanel1.Controls.Add(this.ProgressBar);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(562, 103);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.OpenFileButton);
            this.flowLayoutPanel4.Controls.Add(this.FileTextBox);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(556, 29);
            this.flowLayoutPanel4.TabIndex = 7;
            // 
            // OpenFileButton
            // 
            this.OpenFileButton.Location = new System.Drawing.Point(3, 3);
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(137, 23);
            this.OpenFileButton.TabIndex = 1;
            this.OpenFileButton.Text = "Ouvrir";
            this.OpenFileButton.UseVisualStyleBackColor = true;
            this.OpenFileButton.Click += new System.EventHandler(this.OpenFileButton_Click);
            // 
            // FileTextBox
            // 
            this.FileTextBox.Location = new System.Drawing.Point(146, 3);
            this.FileTextBox.Name = "FileTextBox";
            this.FileTextBox.Size = new System.Drawing.Size(405, 22);
            this.FileTextBox.TabIndex = 2;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.OverwriteDataButton);
            this.flowLayoutPanel3.Controls.Add(this.AppendDataButton);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 38);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(556, 32);
            this.flowLayoutPanel3.TabIndex = 6;
            // 
            // OverwriteDataButton
            // 
            this.OverwriteDataButton.Location = new System.Drawing.Point(3, 3);
            this.OverwriteDataButton.Name = "OverwriteDataButton";
            this.OverwriteDataButton.Size = new System.Drawing.Size(266, 23);
            this.OverwriteDataButton.TabIndex = 3;
            this.OverwriteDataButton.Text = "Écraser les données";
            this.OverwriteDataButton.Click += new System.EventHandler(this.OverwriteDataButton_Click);
            // 
            // AppendDataButton
            // 
            this.AppendDataButton.Location = new System.Drawing.Point(275, 3);
            this.AppendDataButton.Name = "AppendDataButton";
            this.AppendDataButton.Size = new System.Drawing.Size(276, 23);
            this.AppendDataButton.TabIndex = 4;
            this.AppendDataButton.Text = "Ajouter les données";
            this.AppendDataButton.UseVisualStyleBackColor = true;
            this.AppendDataButton.Click += new System.EventHandler(this.AppendDataButton_Click);
            // 
            // ImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 103);
            this.Controls.Add(this.flowLayoutPanel1);
            this.MaximumSize = new System.Drawing.Size(580, 150);
            this.MinimumSize = new System.Drawing.Size(580, 150);
            this.Name = "ImportForm";
            this.Text = "Importer un fichier SQL";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button OpenFileButton;
        private System.Windows.Forms.TextBox FileTextBox;
        private System.Windows.Forms.Button OverwriteDataButton;
        private System.Windows.Forms.Button AppendDataButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
    }
}