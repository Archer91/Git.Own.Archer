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
    public partial class Frm_Role : Form
    {
        public Frm_Role()
        {
            InitializeComponent();
        }

        private void Frm_Role_Load(object sender, EventArgs e)
        {
            tsmiRefresh_Click(sender, e);
        }

        RoleVO roleVO = new RoleVO();
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCode.Text) ||
                    string.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("请完整填写");
                    return;
                }

                roleVO.Code = txtCode.Text;
                roleVO.Name = txtName.Text;
                roleVO.Description = txtDescription.Text;
                roleVO.CreateBy = PublicInfo.accountVO.Code;
                roleVO.UpdateBy = PublicInfo.accountVO.Code;

                PublicInfo.basicImpl.saveRole(roleVO);
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

        private void tsmiSubAdd_Click(object sender, EventArgs e)
        {
            if (null == tvRole.SelectedNode)
            {
                return;
            }
            resetText();
            //TODO
            txtParent.Text = tvRole.SelectedNode.Text;
            roleVO.ParentID = Guid.Parse(tvRole.SelectedNode.Name);
            splitContainer1.Panel1Collapsed = false;
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == tvRole.SelectedNode)
                {
                    return;
                }
                RoleVO selectRole = tvRole.SelectedNode.Tag as RoleVO;
                txtCode.Text = selectRole.Code;
                txtName.Text = selectRole.Name;
                txtDescription.Text = selectRole.Description;
                txtParent.Text = tvRole.SelectedNode.Parent == null ? "" : tvRole.SelectedNode.Parent.Text;

                roleVO.ID = new Guid(tvRole.SelectedNode.Name);
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
                if (null == tvRole.SelectedNode)
                {
                    return;
                }

                PublicInfo.basicImpl.enableAndDisableByRoleCode((tvRole.SelectedNode.Tag as RoleVO).Code, "1", PublicInfo.accountVO.Code);
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
                if (null == tvRole.SelectedNode)
                {
                    return;
                }

                PublicInfo.basicImpl.enableAndDisableByRoleCode((tvRole.SelectedNode.Tag as RoleVO).Code, "0", PublicInfo.accountVO.Code);
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
                List<RoleVO> lst = PublicInfo.basicImpl.getRole();
                PublicMethods.loadRoleWithTreeView(tvRole, lst);

                List<MenuVO> lst2 = PublicInfo.basicImpl.getMenu();
                PublicMethods.loadMenuWithTreeView(tvMenu, lst2.Where(t=>t.Status.Equals("1")).ToList());

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
            txtDescription.Text = string.Empty;
            txtParent.Text = string.Empty;

            roleVO.ID = Guid.Empty;
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
                    this.toolTip1.Show("添加角色", pb);
                    break;
                case "pbEdit":
                    this.toolTip1.Show("编辑角色", pb);
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

        private void tvRole_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if(null == tvRole.SelectedNode)
                {
                    return;
                }

                List<RoleRVO> lst = PublicInfo.basicImpl.getRoleRByRID(Guid.Parse(tvRole.SelectedNode.Name));
                PublicMethods.bindWithTreeView(tvMenu, lst);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tvMenu_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Action == TreeViewAction.ByMouse)
                {
                    TreeNode tn = e.Node;
                    if (tn.Checked)
                    {
                        PublicMethods.tvUpCheck(tn, true,tvRole.SelectedNode == null ? Guid.Empty : Guid.Parse(tvRole.SelectedNode.Name));
                        PublicMethods.tvDownCheck(tn, true, tvRole.SelectedNode == null ? Guid.Empty : Guid.Parse(tvRole.SelectedNode.Name));
                    }
                    else
                    {
                        PublicMethods.tvDownCheck(tn, false, tvRole.SelectedNode == null ? Guid.Empty : Guid.Parse(tvRole.SelectedNode.Name));
                        PublicMethods.tvUpCheck(tn, false, tvRole.SelectedNode == null ? Guid.Empty : Guid.Parse(tvRole.SelectedNode.Name));
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
