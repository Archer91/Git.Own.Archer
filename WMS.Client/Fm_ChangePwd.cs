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
    public partial class Fm_ChangePwd : Form
    {
        public Fm_ChangePwd()
        {
            InitializeComponent();
        }

        SysInfoClient sic = null;
        private void Fm_ChangePwd_Load(object sender, EventArgs e)
        {
            sic = new SysInfoClient();
            txtAccount.Text = PubInfoUtil.accountEasyVO.Code;
            txtOldPwd.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAccount.Text) ||
                    string.IsNullOrEmpty(txtOldPwd.Text) ||
                    string.IsNullOrEmpty(txtNewPwd.Text))
                {
                    MessageBox.Show("填写完整");
                    return;
                }

                sic.changePassword(txtAccount.Text.Trim(), txtOldPwd.Text.Trim(), txtNewPwd.Text.Trim());
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
