using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SSO.Lib.Impl.Basic;
using SSO.Lib.VO.Basic;
using Archer.SSO.helpclass;

namespace Archer.SSO
{
    public partial class Frm_Login : Form
    {
        public Frm_Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtUserName.Text))
                {
                    errorProvider1.Clear();
                    errorProvider1.SetError(txtUserName, "请输入用户名");
                    return;
                }
                if(string.IsNullOrEmpty(txtPassword.Text))
                {
                    errorProvider1.Clear();
                    errorProvider1.SetError(txtPassword, "请输入密码");
                    return;
                }

                //登录验证
                PublicInfo.accountVO = PublicInfo.loginImpl.checkLogin(txtUserName.Text, txtPassword.Text);
                List<RoleRVO> lst = PublicInfo.basicImpl.getRoleByAccount(PublicInfo.accountVO.ID, PublicInfo.accountVO.DeptID);
                PublicInfo.loginForm = this;
                Frm_SwitchRole roleForm = new Frm_SwitchRole(lst);
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Frm_Login_Load(object sender, EventArgs e)
        {
            try
            {
                lblTitle.Text = ReadSystemSet.getSystemTitle();
                cmbFactory.DataSource = ReadSystemSet.getFactories();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
