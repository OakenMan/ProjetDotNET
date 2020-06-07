namespace Bacchus
{
    partial class SousFamilleForm
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.RefTextBox = new System.Windows.Forms.TextBox();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.ConfirmButton = new System.Windows.Forms.Button();
            this.FamilleComboBox = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(455, 265);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.RefTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.NameTextBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.FamilleComboBox, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 13);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(429, 191);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 34);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nom Sous-Famille";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Famille";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 34);
            this.label4.TabIndex = 5;
            this.label4.Text = "Réference Sous-Famille";
            // 
            // RefTextBox
            // 
            this.RefTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.RefTextBox.Enabled = false;
            this.RefTextBox.Location = new System.Drawing.Point(118, 83);
            this.RefTextBox.Name = "RefTextBox";
            this.RefTextBox.ReadOnly = true;
            this.RefTextBox.Size = new System.Drawing.Size(300, 22);
            this.RefTextBox.TabIndex = 6;
            // 
            // NameTextBox
            // 
            this.NameTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NameTextBox.Location = new System.Drawing.Point(118, 147);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(300, 22);
            this.NameTextBox.TabIndex = 7;
            this.NameTextBox.TextChanged += new System.EventHandler(this.NameTextBox_TextChanged);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.ConfirmButton);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 217);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(10);
            this.flowLayoutPanel2.Size = new System.Drawing.Size(455, 48);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // ConfirmButton
            // 
            this.ConfirmButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ConfirmButton.Location = new System.Drawing.Point(13, 13);
            this.ConfirmButton.Name = "ConfirmButton";
            this.ConfirmButton.Size = new System.Drawing.Size(429, 25);
            this.ConfirmButton.TabIndex = 0;
            this.ConfirmButton.UseVisualStyleBackColor = true;
            this.ConfirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // FamilleComboBox
            // 
            this.FamilleComboBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.FamilleComboBox.FormattingEnabled = true;
            this.FamilleComboBox.Location = new System.Drawing.Point(118, 19);
            this.FamilleComboBox.Name = "FamilleComboBox";
            this.FamilleComboBox.Size = new System.Drawing.Size(300, 24);
            this.FamilleComboBox.TabIndex = 8;
            // 
            // SousFamilleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 265);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.MaximumSize = new System.Drawing.Size(473, 312);
            this.MinimumSize = new System.Drawing.Size(473, 312);
            this.Name = "SousFamilleForm";
            this.Text = "Créer ou modifier une sous-famille";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button ConfirmButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox RefTextBox;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.ComboBox FamilleComboBox;
    }
}