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
    public partial class Frm_Menu : Form
    {
        public Frm_Menu()
        {
            InitializeComponent();
        }

        private void Frm_Menu_Load(object sender, EventArgs e)
        {
            tsmiRefresh_Click(sender, e);
        }

        MenuVO menuVO = new MenuVO();
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("请完整填写");
                    return;
                }

                menuVO.Name = txtName.Text;
                menuVO.Description = txtDescription.Text;
                menuVO.CmdForm = txtCmdForm.Text;
                menuVO.CmdExe = txtCmdExe.Text;
                menuVO.CreateBy = PublicInfo.accountVO.Code;
                menuVO.UpdateBy = PublicInfo.accountVO.Code;

                PublicInfo.basicImpl.saveMenu(menuVO);
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
            if (null == tvMenu.SelectedNode)
            {
                return;
            }
            resetText();
            //TODO
            txtParent.Text = tvMenu.SelectedNode.Text;
            menuVO.ParentID = Guid.Parse(tvMenu.SelectedNode.Name);
            splitContainer1.Panel1Collapsed = false;
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == tvMenu.SelectedNode)
                {
                    return;
                }
                MenuVO selectMenu = tvMenu.SelectedNode.Tag as MenuVO;
                txtName.Text = selectMenu.Name;
                txtDescription.Text = selectMenu.Description;
                txtParent.Text = tvMenu.SelectedNode.Parent == null ? "" : tvMenu.SelectedNode.Parent.Text;
                txtCmdForm.Text = selectMenu.CmdForm;
                txtCmdExe.Text = selectMenu.CmdExe;

                menuVO.ID = new Guid(tvMenu.SelectedNode.Name);
                txtName.Focus();
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
                if (null == tvMenu.SelectedNode)
                {
                    return;
                }

                PublicInfo.basicImpl.enableAndDisableByMenuId(Guid.Parse(tvMenu.SelectedNode.Name), "1", PublicInfo.accountVO.Code);
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
                if (null == tvMenu.SelectedNode)
                {
                    return;
                }

                PublicInfo.basicImpl.enableAndDisableByMenuId(Guid.Parse(tvMenu.SelectedNode.Name), "0", PublicInfo.accountVO.Code);
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
                List<MenuVO> lst = PublicInfo.basicImpl.getMenu();
                PublicMethods.loadMenuWithTreeView(tvMenu, lst);
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
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtParent.Text = string.Empty;
            txtCmdForm.Text = string.Empty;
            txtCmdExe.Text = string.Empty;

            menuVO.ID = Guid.Empty;
            menuVO.ParentID = null;
            txtName.Focus();
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
                    this.toolTip1.Show("添加菜单", pb);
                    break;
                case "pbEdit":
                    this.toolTip1.Show("编辑菜单", pb);
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
