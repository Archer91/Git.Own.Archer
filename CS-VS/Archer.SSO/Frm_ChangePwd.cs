using Archer.SSO.helpclass;
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
    public partial class Frm_ChangePwd : Form
    {
        public Frm_ChangePwd()
        {
            InitializeComponent();
        }

        private void Frm_ChangePwd_Load(object sender, EventArgs e)
        {
            txtAccount.Text = PublicInfo.accountVO.Code;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtAccount.Text) ||
                    string.IsNullOrEmpty(txtOldPwd.Text) || 
                    string.IsNullOrEmpty(txtNewPwd.Text))
                {
                    MessageBox.Show("填写完整");
                    return;
                }

                PublicInfo.loginImpl.changePassword(txtAccount.Text.Trim(), txtOldPwd.Text.Trim(), txtNewPwd.Text.Trim());
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
