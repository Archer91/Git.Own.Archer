using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMS.Client.SysInfoSvr;
using WMS.Client.util;

namespace WMS.Client
{
    public partial class Fm_Login : Form
    {
        public Fm_Login()
        {
            InitializeComponent();
        }

        SysInfoClient sic = null;
        private void Fm_Login_Load(object sender, EventArgs e)
        {
            try
            {
                sic = new SysInfoClient();
                lblTitle.Text = ConfigUtil.getSystemTitle();

                cmbFactory.DisplayMember = "CompanyName";
                cmbFactory.ValueMember = "ID";
                cmbFactory.DataSource= sic.getCompanyEasyList();

                //设置皮肤
                skinEngine1.SkinFile =Path.Combine(System.Environment.CurrentDirectory,ConfigUtil.getSystemSkins());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtUserName.Text))
                {
                    errorProvider1.Clear();
                    errorProvider1.SetError(txtUserName, "请输入用户名");
                    return;
                }
                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    errorProvider1.Clear();
                    errorProvider1.SetError(txtPassword, "请输入密码");
                    return;
                }
                if(string.IsNullOrEmpty(cmbFactory.Text))
                {
                    errorProvider1.Clear();
                    errorProvider1.SetError(cmbFactory, "请选择公司");
                    return;
                }

                //登录验证
                PubInfoUtil.accountEasyVO = sic.checkLogin(txtUserName.Text, txtPassword.Text);
                PubInfoUtil.loginForm = this;
                PubInfoUtil.CurrentCompanyId = Int32.Parse(cmbFactory.SelectedValue.ToString());
                PubInfoUtil.CurrentCompanyName = cmbFactory.Text;

                Fm_SwitchRole roleForm = new Fm_SwitchRole();
                this.Hide();
                roleForm.ShowDialog();
                errorProvider1.Clear();
                txtUserName.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtUserName.Focus();
            }
            catch (ArgumentNullException ex)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(btnLogin, ex.Message);
            }
            catch (Exception ex)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(btnLogin, ex.Message);
            }
        }

        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnLogin_Click(sender, e);
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnLogin_Click(sender, e);
            }
        }

    }
}
