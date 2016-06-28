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
using Extension.Util.Archer;
using WMS.Client.util;

namespace WMS.Client.sys
{
    public partial class Fm_SysUserDefined : Form
    {
        public Fm_SysUserDefined()
        {
            InitializeComponent();
        }

        SysInfoClient sic = null;
        SysUserDefinedVO sudv = null;
        
        private void Fm_SysUserDefined_Load(object sender, EventArgs e)
        {
            sic = new SysInfoClient();
            sudv = new SysUserDefinedVO();
            tsmiRefresh_Click(sender, e);
        }

        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            resetText();
            splitContainer1.Panel1Collapsed = false;
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            if(dgvUdc.SelectedRows.Count <= 0)
            {
                return;
            }
            txtSysCode.Text = dgvUdc.SelectedRows[0].GetString("SysCode");
            txtCategory.Text = dgvUdc.SelectedRows[0].GetString("Category");
            txtUKey.Text = dgvUdc.SelectedRows[0].GetString("UKey");
            txtUValue.Text = dgvUdc.SelectedRows[0].GetString("UValue");
            rtbDesc.Text = dgvUdc.SelectedRows[0].GetString("Description");

            sudv.ID = dgvUdc.SelectedRows[0].GetInt32("ID");
            txtSysCode.Focus();
            splitContainer1.Panel1Collapsed = false;
        }

        private void tsmiEnable_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUdc.SelectedRows.Count <= 0)
                {
                    return;
                }
                sic.enableAndDisableByUdcId(dgvUdc.SelectedRows[0].GetInt32("ID"), 1, PubInfoUtil.accountEasyVO.Code);
                tsmiRefresh_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmiDisable_Click(object sender, EventArgs e)
        {
            try
            {
                if(dgvUdc.SelectedRows.Count <= 0)
                {
                    return;
                }
                sic.enableAndDisableByUdcId(dgvUdc.SelectedRows[0].GetInt32("ID"), 0, PubInfoUtil.accountEasyVO.Code);
                tsmiRefresh_Click(sender, e);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmiRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                splitContainer1.Panel1Collapsed = true;
                resetText();

                dgvUdc.AutoGenerateColumns = false;
                dgvUdc.DataSource = sic.getUserDeinedList();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void resetText()
        {
            txtSysCode.Text = string.Empty;
            txtCategory.Text = string.Empty;
            txtUKey.Text = string.Empty;
            txtUValue.Text = string.Empty;
            rtbDesc.Text = string.Empty;
            tsslMsg.Text = string.Empty;

            sudv.ID = -1;
            txtSysCode.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtSysCode.Text.IsNullOrEmpty() ||
                    txtCategory.Text.IsNullOrEmpty() ||
                    txtUKey.Text.IsNullOrEmpty() ||
                    txtUValue.Text.IsNullOrEmpty())
                {
                    tsslMsg.Text = "请完整填写信息";
                    txtSysCode.Focus();
                    return;
                }

                sudv.SysCode = txtSysCode.Text.Trim();
                sudv.Category = txtCategory.Text.Trim();
                sudv.UKey = txtUKey.Text.Trim();
                sudv.UValue = txtUValue.Text.Trim();
                sudv.Description = rtbDesc.Text.Trim();
                sudv.CreatedBy = PubInfoUtil.accountEasyVO.Code;
                sudv.UpdatedBy = PubInfoUtil.accountEasyVO.Code;

                sic.saveUserDefined(sudv);
                tsmiRefresh_Click(sender, e);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
