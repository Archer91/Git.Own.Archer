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
    public partial class Frm_Factory : Form
    {
        public Frm_Factory()
        {
            InitializeComponent();
        }

        private void Frm_Factory_Load(object sender, EventArgs e)
        {
            tsmiRefresh_Click(sender, e);
        }

        FactoryVO factoryVO = new FactoryVO();
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtCode.Text) ||
                    string.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("请完整填写");
                    return;
                }

                factoryVO.Code = txtCode.Text.Trim();
                factoryVO.Name = txtName.Text.Trim();
                factoryVO.Description = txtDescription.Text.Trim();
                factoryVO.CreateBy = PublicInfo.accountVO.Code;
                factoryVO.UpdateBy = PublicInfo.accountVO.Code;

                PublicInfo.basicImpl.saveFactory(factoryVO);
                tsmiRefresh_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmiEnable_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvFactory.SelectedRows.Count <= 0)
                {
                    return;
                }
                PublicInfo.basicImpl.enableAndDisableByFactoryCode(dgvFactory.SelectedRows[0].Cells["Code"].Value.ToString(), "1", PublicInfo.accountVO.Code);
                tsmiRefresh_Click(sender, e);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmiDisable_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvFactory.SelectedRows.Count <= 0)
                {
                    return;
                }
                PublicInfo.basicImpl.enableAndDisableByFactoryCode(dgvFactory.SelectedRows[0].Cells["Code"].Value.ToString(), "0", PublicInfo.accountVO.Code);
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
                dgvFactory.DataSource = PublicInfo.basicImpl.getFactory();
                dgvFactory.Columns["ID"].Visible = false;
                resetText();
                splitContainer1.Panel1Collapsed = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            if (dgvFactory.SelectedRows.Count <= 0)
            {
                return;
            }
            txtCode.Text = dgvFactory.SelectedRows[0].Cells["Code"].Value.ToString();
            txtName.Text = dgvFactory.SelectedRows[0].Cells["Name"].Value.ToString();
            txtDescription.Text = dgvFactory.SelectedRows[0].Cells["Description"].Value.ToString();

            factoryVO.ID = Guid.Parse(dgvFactory.SelectedRows[0].Cells["ID"].Value.ToString());
            txtCode.Focus();
            splitContainer1.Panel1Collapsed = false;
        }

        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            resetText();
            splitContainer1.Panel1Collapsed = false;
        }

        private void resetText()
        {
            txtCode.Text = string.Empty;
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;

            factoryVO.ID = Guid.Empty;
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
                    this.toolTip1.Show("添加工厂", pb);
                    break;
                case "pbEdit":
                    this.toolTip1.Show("编辑工厂", pb);
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
