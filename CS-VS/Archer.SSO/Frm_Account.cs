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
    public partial class Frm_Account : Form
    {
        public Frm_Account()
        {
            InitializeComponent();
        }

        AccountVO accountVO = new AccountVO();
        private void Frm_Account_Load(object sender, EventArgs e)
        {
            tsmiRefresh_Click(sender, e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCode.Text))
                {
                    MessageBox.Show("请完整填写");
                    return;
                }

                accountVO.Code = txtCode.Text.Trim();
                accountVO.Name = txtName.Text.Trim();
                accountVO.EngName = txtEngName.Text.Trim();
                if(string.IsNullOrEmpty(cmbDepartment.Text))
                {
                    accountVO.DeptCode = "";
                    accountVO.DeptID =  null;
                }
                else
                {
                    accountVO.DeptCode =  cmbDepartment.SelectedValue.ToString();
                    accountVO.DeptID =  (cmbDepartment.SelectedItem as DepartmentVO).ID;
                }
                accountVO.Email = txtEmail.Text.Trim();
                accountVO.Tel = txtTel.Text.Trim();
                accountVO.CreateBy = PublicInfo.accountVO.Code;
                accountVO.UpdateBy = PublicInfo.accountVO.Code;

                PublicInfo.basicImpl.saveAccount(accountVO);
                tsmiRefresh_Click(sender, e);
            }
            catch (Exception ex)
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
            if (dgvAccount.SelectedRows.Count <= 0)
            {
                return;
            }
            txtCode.Text = dgvAccount.SelectedRows[0].Cells["Code"].Value.ToString();
            txtName.Text = dgvAccount.SelectedRows[0].Cells["Name"].Value == null ? "" : 
                dgvAccount.SelectedRows[0].Cells["Name"].Value.ToString();
            txtEngName.Text = dgvAccount.SelectedRows[0].Cells["EngName"].Value == null ? "" : 
                dgvAccount.SelectedRows[0].Cells["EngName"].Value.ToString();
            cmbDepartment.SelectedValue = dgvAccount.SelectedRows[0].Cells["DeptCode"].Value ?? "";
            txtEmail.Text = dgvAccount.SelectedRows[0].Cells["Email"].Value == null ? "" : 
                dgvAccount.SelectedRows[0].Cells["Email"].Value.ToString();
            txtTel.Text = dgvAccount.SelectedRows[0].Cells["Tel"].Value == null ? "" :
                dgvAccount.SelectedRows[0].Cells["Tel"].Value.ToString();

            accountVO.ID = Guid.Parse(dgvAccount.SelectedRows[0].Cells["ID"].Value.ToString());
            txtCode.Focus();
            splitContainer1.Panel1Collapsed = false;
        }

        private void tsmiEnable_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAccount.SelectedRows.Count <= 0)
                {
                    return;
                }
                PublicInfo.basicImpl.enableAndDisableByAccountCode(dgvAccount.SelectedRows[0].Cells["Code"].Value.ToString(), "1", PublicInfo.accountVO.Code);
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
                if (dgvAccount.SelectedRows.Count <= 0)
                {
                    return;
                }
                PublicInfo.basicImpl.enableAndDisableByAccountCode(dgvAccount.SelectedRows[0].Cells["Code"].Value.ToString(), "0", PublicInfo.accountVO.Code);
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
                cmbDepartment.DisplayMember = "Name";
                cmbDepartment.ValueMember = "Code";
                cmbDepartment.DataSource = PublicInfo.basicImpl.getDepartment().Where(m=>m.Status.Equals("1")).ToList();

                List<RoleVO> lst = PublicInfo.basicImpl.getRole();
                PublicMethods.loadRoleWithTreeView(tvRole, lst.Where(t=>t.Status.Equals("1")).ToList());

                dgvAccount.DataSource = PublicInfo.basicImpl.getAccount();
                dgvAccount.Columns["ID"].Visible = false;
                dgvAccount.Columns["DeptID"].Visible = false;
                dgvAccount.Columns["Password"].Visible = false;
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
            txtName.Text = string.Empty;
            txtEngName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTel.Text = string.Empty;
            cmbDepartment.Text = string.Empty;

            accountVO.ID = Guid.Empty;
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
                    this.toolTip1.Show("添加用户", pb);
                    break;
                case "pbEdit":
                    this.toolTip1.Show("编辑用户", pb);
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

        private void dgvAccount_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvAccount.SelectedRows.Count <= 0 )
                {
                    return;
                }

                List<RoleRVO> lst = PublicInfo.basicImpl.getRoleRByRID(Guid.Parse(dgvAccount.SelectedRows[0].Cells["ID"].Value.ToString()));
                PublicMethods.bindWithTreeView(tvRole, lst);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tvRole_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Action == TreeViewAction.ByMouse)
                {
                    TreeNode tn = e.Node;
                    if (tn.Checked)
                    {
                        PublicMethods.tvUpCheck(tn, true,dgvAccount.SelectedRows.Count <= 0 ? Guid.Empty : Guid.Parse(dgvAccount.SelectedRows[0].Cells["ID"].Value.ToString()));
                        PublicMethods.tvDownCheck(tn, true, dgvAccount.SelectedRows.Count <= 0 ? Guid.Empty : Guid.Parse(dgvAccount.SelectedRows[0].Cells["ID"].Value.ToString()));
                    }
                    else
                    {
                        PublicMethods.tvDownCheck(tn, false, dgvAccount.SelectedRows.Count <= 0 ? Guid.Empty : Guid.Parse(dgvAccount.SelectedRows[0].Cells["ID"].Value.ToString()));
                        PublicMethods.tvUpCheck(tn, false, dgvAccount.SelectedRows.Count <= 0 ? Guid.Empty : Guid.Parse(dgvAccount.SelectedRows[0].Cells["ID"].Value.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
