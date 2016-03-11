namespace Archer.SSO
{
    partial class Frm_Customer
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
            this.dataGridViewComboBox1 = new Archer.ExtendControlLib.DataGridViewComboBox();
            this.treeViewComboBox1 = new Archer.ExtendControlLib.TreeViewComboBox();
            this.SuspendLayout();
            // 
            // dataGridViewComboBox1
            // 
            this.dataGridViewComboBox1.FormattingEnabled = true;
            this.dataGridViewComboBox1.Location = new System.Drawing.Point(42, 40);
            this.dataGridViewComboBox1.Name = "dataGridViewComboBox1";
            this.dataGridViewComboBox1.NullValue = "";
            this.dataGridViewComboBox1.PopupGridAutoSize = false;
            this.dataGridViewComboBox1.RowFilterVisible = false;
            this.dataGridViewComboBox1.sDisplayField = "";
            this.dataGridViewComboBox1.sDisplayMember = "";
            this.dataGridViewComboBox1.SeparatorChar = "|";
            this.dataGridViewComboBox1.Size = new System.Drawing.Size(153, 20);
            this.dataGridViewComboBox1.sKeyWords = "";
            this.dataGridViewComboBox1.sValueMember = "";
            this.dataGridViewComboBox1.TabIndex = 0;
            this.dataGridViewComboBox1.Value = "";
            // 
            // treeViewComboBox1
            // 
            this.treeViewComboBox1.ItemHoverBackColor = System.Drawing.SystemColors.Highlight;
            this.treeViewComboBox1.ItemHoverColor = System.Drawing.SystemColors.HighlightText;
            this.treeViewComboBox1.ItemHoverGradientBackColor = System.Drawing.Color.Empty;
            this.treeViewComboBox1.Location = new System.Drawing.Point(42, 83);
            this.treeViewComboBox1.Name = "treeViewComboBox1";
            this.treeViewComboBox1.Size = new System.Drawing.Size(153, 20);
            this.treeViewComboBox1.TabIndex = 1;
            this.treeViewComboBox1.Text = "treeViewComboBox1";
            // 
            // Frm_Customer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 562);
            this.Controls.Add(this.treeViewComboBox1);
            this.Controls.Add(this.dataGridViewComboBox1);
            this.Name = "Frm_Customer";
            this.Text = "客户维护";
            this.Load += new System.EventHandler(this.Frm_Customer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ExtendControlLib.DataGridViewComboBox dataGridViewComboBox1;
        private ExtendControlLib.TreeViewComboBox treeViewComboBox1;




    }
}