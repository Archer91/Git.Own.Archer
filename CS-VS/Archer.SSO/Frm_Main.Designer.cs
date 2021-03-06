﻿namespace Archer.SSO
{
    partial class Frm_Main
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
            this.menStrip = new System.Windows.Forms.MenuStrip();
            this.tsmiInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSwitchRole = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChangePwd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoginInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslRole = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslComputer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslIP = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslWeek = new System.Windows.Forms.ToolStripStatusLabel();
            this.menStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menStrip
            // 
            this.menStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiInfo,
            this.tsmiLoginInfo});
            this.menStrip.Location = new System.Drawing.Point(0, 0);
            this.menStrip.Name = "menStrip";
            this.menStrip.ShowItemToolTips = true;
            this.menStrip.Size = new System.Drawing.Size(934, 24);
            this.menStrip.TabIndex = 1;
            this.menStrip.Text = "menuStrip1";
            // 
            // tsmiInfo
            // 
            this.tsmiInfo.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsmiInfo.AutoToolTip = true;
            this.tsmiInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsmiInfo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSwitchRole,
            this.tsmiChangePwd,
            this.toolStripSeparator1,
            this.tsmiLogout});
            this.tsmiInfo.Image = global::Archer.SSO.Properties.Resources.user;
            this.tsmiInfo.Name = "tsmiInfo";
            this.tsmiInfo.Size = new System.Drawing.Size(28, 20);
            // 
            // tsmiSwitchRole
            // 
            this.tsmiSwitchRole.Image = global::Archer.SSO.Properties.Resources.my_account;
            this.tsmiSwitchRole.Name = "tsmiSwitchRole";
            this.tsmiSwitchRole.Size = new System.Drawing.Size(124, 22);
            this.tsmiSwitchRole.Text = "切换角色";
            this.tsmiSwitchRole.Click += new System.EventHandler(this.tsmiSwitchRole_Click);
            // 
            // tsmiChangePwd
            // 
            this.tsmiChangePwd.Image = global::Archer.SSO.Properties.Resources._lock;
            this.tsmiChangePwd.Name = "tsmiChangePwd";
            this.tsmiChangePwd.Size = new System.Drawing.Size(124, 22);
            this.tsmiChangePwd.Text = "修改密码";
            this.tsmiChangePwd.Click += new System.EventHandler(this.tsmiChangePwd_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(121, 6);
            // 
            // tsmiLogout
            // 
            this.tsmiLogout.Image = global::Archer.SSO.Properties.Resources.logout;
            this.tsmiLogout.Name = "tsmiLogout";
            this.tsmiLogout.Size = new System.Drawing.Size(124, 22);
            this.tsmiLogout.Text = "注销用户";
            this.tsmiLogout.Click += new System.EventHandler(this.tsmiLogout_Click);
            // 
            // tsmiLoginInfo
            // 
            this.tsmiLoginInfo.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsmiLoginInfo.Name = "tsmiLoginInfo";
            this.tsmiLoginInfo.Size = new System.Drawing.Size(12, 20);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslUser,
            this.toolStripStatusLabel2,
            this.tsslRole,
            this.toolStripStatusLabel4,
            this.tsslComputer,
            this.toolStripStatusLabel6,
            this.tsslIP,
            this.toolStripStatusLabel1,
            this.tsslTime,
            this.toolStripStatusLabel5,
            this.tsslWeek});
            this.statusStrip1.Location = new System.Drawing.Point(0, 640);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(934, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslUser
            // 
            this.tsslUser.Name = "tsslUser";
            this.tsslUser.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(4, 17);
            // 
            // tsslRole
            // 
            this.tsslRole.Name = "tsslRole";
            this.tsslRole.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(4, 17);
            // 
            // tsslComputer
            // 
            this.tsslComputer.Name = "tsslComputer";
            this.tsslComputer.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(4, 17);
            // 
            // tsslIP
            // 
            this.tsslIP.Name = "tsslIP";
            this.tsslIP.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(4, 17);
            // 
            // tsslTime
            // 
            this.tsslTime.Name = "tsslTime";
            this.tsslTime.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(4, 17);
            // 
            // tsslWeek
            // 
            this.tsslWeek.Name = "tsslWeek";
            this.tsslWeek.Size = new System.Drawing.Size(0, 17);
            // 
            // Frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 662);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menStrip;
            this.Name = "Frm_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "信息系统平台";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_Main_FormClosing);
            this.Load += new System.EventHandler(this.Frm_Main_Load);
            this.menStrip.ResumeLayout(false);
            this.menStrip.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menStrip;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiInfo;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoginInfo;
        private System.Windows.Forms.ToolStripMenuItem tsmiSwitchRole;
        private System.Windows.Forms.ToolStripMenuItem tsmiChangePwd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiLogout;
        private System.Windows.Forms.ToolStripStatusLabel tsslUser;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel tsslRole;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel tsslComputer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel tsslIP;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tsslTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel tsslWeek;
    }
}