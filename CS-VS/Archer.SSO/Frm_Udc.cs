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
    public partial class Frm_Udc : Form
    {
        public Frm_Udc()
        {
            InitializeComponent();
        }

        UdcVO udcVO = new UdcVO();
        private void Frm_Udc_Load(object sender, EventArgs e)
        {
            tsmiRefresh_Click(sender, e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtCode.Text) ||
                    string.IsNullOrEmpty(txtSysCode.Text) ||
                    string.IsNullOrEmpty(txtSysCategory.Text))
                {
                    MessageBox.Show("请填写完整");
                    return;
                }

                udcVO.Code = txtCode.Text.Trim();
                udcVO.Value = txtValue.Text.Trim();
                udcVO.SysCode = txtSysCode.Text.Trim();
                udcVO.Category = txtSysCategory.Text.Trim();
                udcVO.Description = txtDescription.Text.Trim();
                udcVO.CreateBy = PublicInfo.accountVO.Code;
                udcVO.UpdateBy = PublicInfo.accountVO.Code;

                PublicInfo.basicImpl.saveUdc(udcVO);
                tsmiRefresh_Click(sender, e);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            resetText();
            splitContainer1.Panel1Collapsed = false;
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            if (dgvUdc.SelectedRows.Count <= 0)
            {
                return;
            }
            txtCode.Text = dgvUdc.SelectedRows[0].Cells["Code"].Value.ToString();
            txtSysCode.Text = dgvUdc.SelectedRows[0].Cells["SysCode"].Value == null ? "" :
                dgvUdc.SelectedRows[0].Cells["SysCode"].Value.ToString();
            txtSysCategory.Text = dgvUdc.SelectedRows[0].Cells["Category"].Value == null ? "" :
                dgvUdc.SelectedRows[0].Cells["Category"].Value.ToString();
            txtValue.Text = dgvUdc.SelectedRows[0].Cells["Value"].Value == null ? "" :
                dgvUdc.SelectedRows[0].Cells["Value"].Value.ToString();
            txtDescription.Text = dgvUdc.SelectedRows[0].Cells["Description"].Value == null ? "" :
                dgvUdc.SelectedRows[0].Cells["Description"].Value.ToString();

            udcVO.ID = Guid.Parse(dgvUdc.SelectedRows[0].Cells["ID"].Value.ToString());
            txtCode.Focus();
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
                PublicInfo.basicImpl.enableAndDisableByUdcCode(dgvUdc.SelectedRows[0].Cells["Code"].Value.ToString(), "1", PublicInfo.accountVO.Code);
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
                if (dgvUdc.SelectedRows.Count <= 0)
                {
                    return;
                }
                PublicInfo.basicImpl.enableAndDisableByUdcCode(dgvUdc.SelectedRows[0].Cells["Code"].Value.ToString(), "0", PublicInfo.accountVO.Code);
                tsmiRefresh_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmiRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                dgvUdc.DataSource = PublicInfo.basicImpl.getUdc();
                dgvUdc.Columns["ID"].Visible = false;
                resetText();
                splitContainer1.Panel1Collapsed = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void resetText()
        {
            txtCode.Text = string.Empty;
            txtSysCode.Text = string.Empty;
           txtSysCategory.Text = string.Empty;
           txtValue.Text = string.Empty;
           txtDescription.Text = string.Empty;

            udcVO.ID = Guid.Empty;
            txtCode.Focus();
        }

        private void showToolTip(object sender, EventArgs e)
        {
            if (null == sender)
            {
                return;
            }
            PictureBox pb = sender as PictureBox;
            switch (pb.Name)
            {
                case "pbAdd":
                    this.toolTip1.Show("添加", pb);
                    break;
                case "pbEdit":
                    this.toolTip1.Show("编辑", pb);
                    break;
                case "pbCancel":
                    this.toolTip1.Show("撤销", pb);
                    break;
                case "pbSearch":
                    this.toolTip1.Show("查询", pb);
                    break;
            }
        }

        private void pbSearch_MouseClick(object sender, MouseEventArgs e)
        {

        }

    }
}
