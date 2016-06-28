using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMS.Client.SysInfoSvr;
using WMS.Client.util;

namespace WMS.Client
{
    public partial class Fm_Main : Form
    {
        public Fm_Main()
        {
            InitializeComponent();
        }

        SysInfoClient sic = null;
        bool isFormClosing = true;

        private void Fm_Main_Load(object sender, EventArgs e)
        {
            try
            {
                sic = new SysInfoClient();
                
                isFormClosing = true;
                tsmiCompanyInfo.Text = PubInfoUtil.CurrentCompanyName;
                tsmiLoginInfo.Text = "Welcome:" + PubInfoUtil.accountEasyVO.AccountName ?? PubInfoUtil.accountEasyVO.Code;
                tsslComputer.Text = PubInfoUtil.HostName;
                tsslIP.Text = PubInfoUtil.HostIP;
                tsslUser.Text = PubInfoUtil.accountEasyVO.Code;
                tsslRole.Text = PubInfoUtil.CurrentRoleName;
                tsslTime.Text = DateTime.Now.ToLongDateString();
                tsslWeek.Text = DateTime.Now.DayOfWeek.ToString();

                List<SysMenuVO> lst = sic.getMenuByRoleId(PubInfoUtil.CurrentRoleId);
                PubMethods.loadMenuWithMenuStrip(menStrip, lst);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmiSwitchCompany_Click(object sender, EventArgs e)
        {
            //isFormClosing = false;
            Fm_SwitchCompany frmSwitchCompany = new Fm_SwitchCompany();
            frmSwitchCompany.ShowDialog();
            tsmiCompanyInfo.Text = PubInfoUtil.CurrentCompanyName;
        }

        private void tsmiSwitchRole_Click(object sender, EventArgs e)
        {
            isFormClosing = false;
            this.Close();
            PubInfoUtil.switchRoleForm.Show();
        }

        private void tsmiChangePwd_Click(object sender, EventArgs e)
        {
            Fm_ChangePwd changePwdForm = new Fm_ChangePwd();
            changePwdForm.ShowDialog();
        }

        private void tsmiLogout_Click(object sender, EventArgs e)
        {
            isFormClosing = false;
            this.Close();
            PubInfoUtil.loginForm.Show();
        }

        private void Fm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isFormClosing)
            {
                Application.Exit();
            }
        }

       
    }
}
