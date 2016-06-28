namespace WMS.Client
{
    partial class Fm_SwitchRole
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fm_SwitchRole));
            this.lstRoles = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // lstRoles
            // 
            this.lstRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRoles.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstRoles.LargeImageList = this.imageList1;
            this.lstRoles.Location = new System.Drawing.Point(0, 0);
            this.lstRoles.MultiSelect = false;
            this.lstRoles.Name = "lstRoles";
            this.lstRoles.Size = new System.Drawing.Size(340, 338);
            this.lstRoles.SmallImageList = this.imageList1;
            this.lstRoles.TabIndex = 0;
            this.lstRoles.UseCompatibleStateImageBehavior = false;
            this.lstRoles.DoubleClick += new System.EventHandler(this.Fm_SwitchRole_DoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "p001.png");
            // 
            // Fm_SwitchRole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 338);
            this.Controls.Add(this.lstRoles);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Fm_SwitchRole";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "角色选择";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Fm_SwitchRole_FormClosed);
            this.Load += new System.EventHandler(this.Fm_SwitchRole_Load);
            this.Shown += new System.EventHandler(this.Fm_SwitchRole_Shown);
            this.DoubleClick += new System.EventHandler(this.Fm_SwitchRole_DoubleClick);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstRoles;
        private System.Windows.Forms.ImageList imageList1;
    }
}