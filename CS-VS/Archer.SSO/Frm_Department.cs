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
using SSO.Lib.VO.Basic;

namespace Archer.SSO
{
    public partial class Frm_Department : Form
    {
        public Frm_Department()
        {
            InitializeComponent();
        }

        private void Frm_Department_Load(object sender, EventArgs e)
        {
            tsmiRefresh_Click(sender, e);
        }

        DepartmentVO departmentVO = new DepartmentVO();
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

                departmentVO.Code = txtCode.Text;
                departmentVO.Name = txtName.Text;
                departmentVO.Description = txtDescription.Text;
                departmentVO.CreateBy = PublicInfo.accountVO.Code;
                departmentVO.UpdateBy = PublicInfo.accountVO.Code;

                PublicInfo.basicImpl.saveDepartment(departmentVO);
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
                List<DepartmentVO> lst = PublicInfo.basicImpl.getDepartment();
                PublicMethods.loadDepartmentWithTreeView(tvDepartment, lst);

                List<RoleVO> lst2 = PublicInfo.basicImpl.getRole();
                PublicMethods.loadRoleWithTreeView(tvRole, lst2.Where(t=>t.Status.Equals("1")).ToList());

                resetText();
                splitContainer1.Panel1Collapsed = true;
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

        private void tsmiSubAdd_Click(object sender, EventArgs e)
        {
            if(null == tvDepartment.SelectedNode)
            {
                return;
            }
            resetText();
            //TODO
            txtParent.Text = tvDepartment.SelectedNode.Text;
            departmentVO.ParentID = Guid.Parse(tvDepartment.SelectedNode.Name);
            splitContainer1.Panel1Collapsed = false;
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == tvDepartment.SelectedNode)
                {
                    return;
                }
                DepartmentVO selectDept = tvDepartment.SelectedNode.Tag as DepartmentVO;
                txtCode.Text = selectDept.Code;
                txtName.Text = selectDept.Name;
                txtDescription.Text = selectDept.Description;
                txtParent.Text = tvDepartment.SelectedNode.Parent == null ? "" : tvDepartment.SelectedNode.Parent.Text;

                departmentVO.ID = new Guid(tvDepartment.SelectedNode.Name);
                txtCode.Focus();
                splitContainer1.Panel1Collapsed = false;
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
                if (null == tvDepartment.SelectedNode)
                {
                    return;
                }

                PublicInfo.basicImpl.enableAndDisableByDepartmentCode((tvDepartment.SelectedNode.Tag as DepartmentVO).Code, "1", PublicInfo.accountVO.Code);
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
                if (null == tvDepartment.SelectedNode)
                {
                    return;
                }

                PublicInfo.basicImpl.enableAndDisableByDepartmentCode((tvDepartment.SelectedNode.Tag as DepartmentVO).Code, "0", PublicInfo.accountVO.Code);
                tsmiRefresh_Click(sender, e);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void resetText()
        {
            txtCode.Text = string.Empty;
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtParent.Text = string.Empty;

            departmentVO.ID = Guid.Empty;
            departmentVO.ParentID = null;
            txtCode.Focus();
        }

        private void showToolTip(object sender,EventArgs e)
        {
            if(null == sender)
            {
                return;
            }
            PictureBox pb = sender as PictureBox;
            switch (pb.Name)
            {
                case "pbAdd":
                    this.toolTip1.Show("添加部门", pb);
                    break;
                case "pbEdit":
                    this.toolTip1.Show("编辑部门", pb);
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

        private void tvDepartment_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (null == tvDepartment.SelectedNode)
                {
                    return;
                }

                List<RoleRVO> lst = PublicInfo.basicImpl.getRoleRByRID(Guid.Parse(tvDepartment.SelectedNode.Name));
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
                        PublicMethods.tvUpCheck(tn, true,tvDepartment.SelectedNode == null ? Guid.Empty : Guid.Parse(tvDepartment.SelectedNode.Name));
                        PublicMethods.tvDownCheck(tn, true, tvDepartment.SelectedNode == null ? Guid.Empty : Guid.Parse(tvDepartment.SelectedNode.Name));
                    }
                    else
                    {
                        PublicMethods.tvDownCheck(tn, false, tvDepartment.SelectedNode == null ? Guid.Empty : Guid.Parse(tvDepartment.SelectedNode.Name));
                        PublicMethods.tvUpCheck(tn, false, tvDepartment.SelectedNode == null ? Guid.Empty : Guid.Parse(tvDepartment.SelectedNode.Name));
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
