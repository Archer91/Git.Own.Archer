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
    public partial class Fm_SwitchCompany : Form
    {
        public Fm_SwitchCompany()
        {
            InitializeComponent();
        }
        SysInfoClient sic = null;
        private void Fm_SwitchCompany_Load(object sender, EventArgs e)
        {
            try
            {
                sic = new SysInfoClient();

                txtCurCompany.Text = PubInfoUtil.CurrentCompanyName;

                //加载公司列表
                cmbCompany.DisplayMember = "CompanyName";
                cmbCompany.ValueMember = "ID";
                cmbCompany.DataSource = sic.getCompanyEasyList();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCompany.SelectedValue != null)
                {
                    int tmpcompanyid = Int32.Parse(cmbCompany.SelectedValue.ToString());
                    if (PubInfoUtil.CurrentCompanyId != tmpcompanyid)
                    {
                        if (DialogResult.Yes == MessageBox.Show("切换公司将会关闭系统打开的所有窗口，确定继续切换吗？", "WMS-Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2))
                        {
                            PubInfoUtil.CurrentCompanyId = tmpcompanyid;
                            PubInfoUtil.CurrentCompanyName = cmbCompany.Text;

                            //关闭已打开的窗口
                            foreach (Form item in PubInfoUtil.mainForm.MdiChildren)
                            {
                                item.Close();
                                item.Dispose();
                            }
                        }
                    }
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
