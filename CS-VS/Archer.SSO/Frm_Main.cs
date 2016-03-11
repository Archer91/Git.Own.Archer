using Archer.SSO.helpclass;
using SSO.Lib.VO.Basic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Archer.SSO
{
    public partial class Frm_Main : Form
    {
        public Frm_Main()
        {
            InitializeComponent();
        }

        public Frm_Main(List<MenuVO> pLst)
            : this()
        {
            PublicMethods.loadMenuWithMenuStrip(menStrip, pLst);
        }

        private void Frm_Main_Load(object sender, EventArgs e)
        {
            isFormClosing = true;
            tsmiLoginInfo.Text = "Welcome:" + PublicInfo.accountVO.Name ?? PublicInfo.accountVO.Code;
            tsslComputer.Text = PublicInfo.HostName;
            tsslIP.Text = PublicInfo.HostIP;
            tsslUser.Text = PublicInfo.accountVO.Code;
            tsslRole.Text = PublicInfo.CurrentRoleName;
            tsslTime.Text = DateTime.Now.ToLongDateString();
            tsslWeek.Text = DateTime.Now.DayOfWeek.ToString();
        }

        private void Frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (e.CloseReason == CloseReason.ApplicationExitCall)
            //{
            //    return;
            //}

            //if (DialogResult.Yes == MessageBox.Show("系统正在使用中,确认退出系统?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            //{
            //    //System.Environment.Exit(System.Environment.ExitCode);
            //    Application.Exit();
            //}
            //else
            //{
            //    e.Cancel = true;
            //}
            if(isFormClosing)
            {
                //Application.Exit();
                System.Environment.Exit(System.Environment.ExitCode);
            }
        }

        bool isFormClosing = true;
        private void tsmiSwitchRole_Click(object sender, EventArgs e)
        {
            isFormClosing = false;
            this.Close();
            PublicInfo.switchRoleForm.Show();
        }

        private void tsmiChangePwd_Click(object sender, EventArgs e)
        {
            Frm_ChangePwd changePwdForm = new Frm_ChangePwd();
            changePwdForm.ShowDialog();
        }

        private void tsmiLogout_Click(object sender, EventArgs e)
        {
            isFormClosing = false;
            this.Close();
            PublicInfo.loginForm.Show();
        }


    }
}
