namespace Bacchus
{
    partial class ArticleForm
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.QuantiteLabel = new System.Windows.Forms.Label();
            this.PrixLabel = new System.Windows.Forms.Label();
            this.MarqueLabel = new System.Windows.Forms.Label();
            this.SousFamilleLabel = new System.Windows.Forms.Label();
            this.FamilleLabel = new System.Windows.Forms.Label();
            this.DescLabel = new System.Windows.Forms.Label();
            this.RefLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.MarqueComboBox = new System.Windows.Forms.ComboBox();
            this.SousFamilleComboBox = new System.Windows.Forms.ComboBox();
            this.DescTextBox = new System.Windows.Forms.TextBox();
            this.FamilleComboBox = new System.Windows.Forms.ComboBox();
            this.RefTextBox = new System.Windows.Forms.TextBox();
            this.PrixTextBox = new System.Windows.Forms.TextBox();
            this.QuantiteNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.ConfirmButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QuantiteNumericUpDown)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel2);
            this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(342, 328);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.QuantiteLabel, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.PrixLabel, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.MarqueLabel, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.SousFamilleLabel, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.FamilleLabel, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.DescLabel, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.RefLabel, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.tableLayoutPanel2.RowCount = 7;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(94, 277);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // QuantiteLabel
            // 
            this.QuantiteLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.QuantiteLabel.AutoSize = true;
            this.QuantiteLabel.Location = new System.Drawing.Point(10, 249);
            this.QuantiteLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.QuantiteLabel.Name = "QuantiteLabel";
            this.QuantiteLabel.Size = new System.Drawing.Size(47, 13);
            this.QuantiteLabel.TabIndex = 6;
            this.QuantiteLabel.Text = "Quantité";
            // 
            // PrixLabel
            // 
            this.PrixLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.PrixLabel.AutoSize = true;
            this.PrixLabel.Location = new System.Drawing.Point(10, 208);
            this.PrixLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PrixLabel.Name = "PrixLabel";
            this.PrixLabel.Size = new System.Drawing.Size(42, 13);
            this.PrixLabel.TabIndex = 5;
            this.PrixLabel.Text = "Prix HT";
            // 
            // MarqueLabel
            // 
            this.MarqueLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.MarqueLabel.AutoSize = true;
            this.MarqueLabel.Location = new System.Drawing.Point(10, 169);
            this.MarqueLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MarqueLabel.Name = "MarqueLabel";
            this.MarqueLabel.Size = new System.Drawing.Size(43, 13);
            this.MarqueLabel.TabIndex = 4;
            this.MarqueLabel.Text = "Marque";
            // 
            // SousFamilleLabel
            // 
            this.SousFamilleLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.SousFamilleLabel.AutoSize = true;
            this.SousFamilleLabel.Location = new System.Drawing.Point(10, 130);
            this.SousFamilleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.SousFamilleLabel.Name = "SousFamilleLabel";
            this.SousFamilleLabel.Size = new System.Drawing.Size(66, 13);
            this.SousFamilleLabel.TabIndex = 3;
            this.SousFamilleLabel.Text = "Sous-Famille";
            // 
            // FamilleLabel
            // 
            this.FamilleLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.FamilleLabel.AutoSize = true;
            this.FamilleLabel.Location = new System.Drawing.Point(10, 91);
            this.FamilleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FamilleLabel.Name = "FamilleLabel";
            this.FamilleLabel.Size = new System.Drawing.Size(39, 13);
            this.FamilleLabel.TabIndex = 2;
            this.FamilleLabel.Text = "Famille";
            // 
            // DescLabel
            // 
            this.DescLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.DescLabel.AutoSize = true;
            this.DescLabel.Location = new System.Drawing.Point(10, 52);
            this.DescLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.DescLabel.Name = "DescLabel";
            this.DescLabel.Size = new System.Drawing.Size(60, 13);
            this.DescLabel.TabIndex = 1;
            this.DescLabel.Text = "Description";
            // 
            // RefLabel
            // 
            this.RefLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RefLabel.AutoSize = true;
            this.RefLabel.Location = new System.Drawing.Point(10, 13);
            this.RefLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RefLabel.Name = "RefLabel";
            this.RefLabel.Size = new System.Drawing.Size(57, 13);
            this.RefLabel.TabIndex = 0;
            this.RefLabel.Text = "Réference";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.MarqueComboBox, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.SousFamilleComboBox, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.DescTextBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.FamilleComboBox, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.RefTextBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.PrixTextBox, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.QuantiteNumericUpDown, 0, 6);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(100, 2);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(236, 277);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // MarqueComboBox
            // 
            this.MarqueComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.MarqueComboBox.FormattingEnabled = true;
            this.MarqueComboBox.Location = new System.Drawing.Point(2, 165);
            this.MarqueComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.MarqueComboBox.Name = "MarqueComboBox";
            this.MarqueComboBox.Size = new System.Drawing.Size(226, 21);
            this.MarqueComboBox.TabIndex = 5;
            this.MarqueComboBox.SelectedIndexChanged += new System.EventHandler(this.MarqueComboBox_SelectedIndexChanged);
            // 
            // SousFamilleComboBox
            // 
            this.SousFamilleComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.SousFamilleComboBox.FormattingEnabled = true;
            this.SousFamilleComboBox.Location = new System.Drawing.Point(2, 126);
            this.SousFamilleComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.SousFamilleComboBox.Name = "SousFamilleComboBox";
            this.SousFamilleComboBox.Size = new System.Drawing.Size(226, 21);
            this.SousFamilleComboBox.TabIndex = 4;
            this.SousFamilleComboBox.SelectedIndexChanged += new System.EventHandler(this.SousFamilleComboBox_SelectedIndexChanged);
            // 
            // DescTextBox
            // 
            this.DescTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.DescTextBox.Location = new System.Drawing.Point(2, 48);
            this.DescTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.DescTextBox.Name = "DescTextBox";
            this.DescTextBox.Size = new System.Drawing.Size(226, 20);
            this.DescTextBox.TabIndex = 2;
            this.DescTextBox.TextChanged += new System.EventHandler(this.DescTextBox_TextChanged);
            // 
            // FamilleComboBox
            // 
            this.FamilleComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.FamilleComboBox.FormattingEnabled = true;
            this.FamilleComboBox.Location = new System.Drawing.Point(2, 87);
            this.FamilleComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.FamilleComboBox.Name = "FamilleComboBox";
            this.FamilleComboBox.Size = new System.Drawing.Size(226, 21);
            this.FamilleComboBox.TabIndex = 0;
            // 
            // RefTextBox
            // 
            this.RefTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RefTextBox.Location = new System.Drawing.Point(2, 9);
            this.RefTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.RefTextBox.Name = "RefTextBox";
            this.RefTextBox.ReadOnly = true;
            this.RefTextBox.Size = new System.Drawing.Size(226, 20);
            this.RefTextBox.TabIndex = 1;
            // 
            // PrixTextBox
            // 
            this.PrixTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.PrixTextBox.Location = new System.Drawing.Point(2, 204);
            this.PrixTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.PrixTextBox.Name = "PrixTextBox";
            this.PrixTextBox.Size = new System.Drawing.Size(226, 20);
            this.PrixTextBox.TabIndex = 3;
            this.PrixTextBox.TextChanged += new System.EventHandler(this.PrixTextBox_TextChanged);

            // 
            // QuantiteNumericUpDown
            // 
            this.QuantiteNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.QuantiteNumericUpDown.Location = new System.Drawing.Point(2, 245);
            this.QuantiteNumericUpDown.Margin = new System.Windows.Forms.Padding(2);
            this.QuantiteNumericUpDown.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.QuantiteNumericUpDown.Name = "QuantiteNumericUpDown";
            this.QuantiteNumericUpDown.Size = new System.Drawing.Size(225, 20);
            this.QuantiteNumericUpDown.TabIndex = 6;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.ConfirmButton);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 287);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(8);
            this.flowLayoutPanel2.Size = new System.Drawing.Size(342, 41);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // ConfirmButton
            // 
            this.ConfirmButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ConfirmButton.Location = new System.Drawing.Point(10, 10);
            this.ConfirmButton.Margin = new System.Windows.Forms.Padding(2);
            this.ConfirmButton.Name = "ConfirmButton";
            this.ConfirmButton.Size = new System.Drawing.Size(319, 20);
            this.ConfirmButton.TabIndex = 0;
            this.ConfirmButton.Text = "Ajouter";
            this.ConfirmButton.UseVisualStyleBackColor = true;
            this.ConfirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // ArticleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 328);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(358, 367);
            this.MinimumSize = new System.Drawing.Size(358, 367);
            this.Name = "ArticleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ajouter / Modifier un article";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QuantiteNumericUpDown)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button ConfirmButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label QuantiteLabel;
        private System.Windows.Forms.Label PrixLabel;
        private System.Windows.Forms.Label MarqueLabel;
        private System.Windows.Forms.Label SousFamilleLabel;
        private System.Windows.Forms.Label FamilleLabel;
        private System.Windows.Forms.Label DescLabel;
        private System.Windows.Forms.Label RefLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox MarqueComboBox;
        private System.Windows.Forms.ComboBox SousFamilleComboBox;
        private System.Windows.Forms.TextBox DescTextBox;
        private System.Windows.Forms.ComboBox FamilleComboBox;
        private System.Windows.Forms.TextBox RefTextBox;
        private System.Windows.Forms.TextBox PrixTextBox;
        private System.Windows.Forms.NumericUpDown QuantiteNumericUpDown;
    }
}