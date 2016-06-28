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

namespace WMS.Client.sys
{
    public partial class Fm_SysMenu : Form
    {
        public Fm_SysMenu()
        {
            InitializeComponent();
        }

        SysInfoClient sic = null;
        SysMenuVO smv = null;
        private void Fm_SysMenu_Load(object sender, EventArgs e)
        {
            sic = new SysInfoClient();
            smv = new SysMenuVO();
            tsmiRefresh_Click(sender, e);
        }

        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            resetText();
            splitContainer1.Panel1Collapsed = false;
        }

        private void tsmiSubAdd_Click(object sender, EventArgs e)
        {
            if(null == tvMenu.SelectedNode)
            {
                return;
            }
            resetText();
            txtParentMenu.Text = tvMenu.SelectedNode.Text;
            smv.ParentID = Int32.Parse(tvMenu.SelectedNode.Name);
            splitContainer1.Panel1Collapsed = false;
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if(null == tvMenu.SelectedNode)
                {
                    return;
                }
                SysMenuVO selectMenu = tvMenu.SelectedNode.Tag as SysMenuVO;

                txtName.Text = selectMenu.MenuName;
                txtForm.Text = selectMenu.CmdForm;
                txtExe.Text = selectMenu.CmdExe;
                txtParentMenu.Text = tvMenu.SelectedNode.Parent == null ? "" : tvMenu.SelectedNode.Parent.Text;
                txtIcon.Text = selectMenu.Icon;
                rtbDesc.Text = selectMenu.Description;

                smv.ID = Int32.Parse(tvMenu.SelectedNode.Name);
                txtName.Focus();
                splitContainer1.Panel1Collapsed = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmiEnable_Click(object sender, EventArgs e)
        {
            try
            {
                if(null == tvMenu.SelectedNode)
                {
                    return;
                }
                sic.enableAndDisableByMenuId(Int32.Parse(tvMenu.SelectedNode.Name),1,PubInfoUtil.accountEasyVO.Code);
                tsmiRefresh_Click(sender,e);
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
                if (null == tvMenu.SelectedNode)
                {
                    return;
                }
                sic.enableAndDisableByMenuId(Int32.Parse(tvMenu.SelectedNode.Name), 0, PubInfoUtil.accountEasyVO.Code);
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
                splitContainer1.Panel1Collapsed = true;
                resetText();

                List<SysMenuVO> lst = sic.getMenuList();
                PubMethods.loadMenuWithTreeView(tvMenu, lst);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void resetText()
        {
            txtName.Text = string.Empty;
            txtForm.Text = string.Empty;
            txtExe.Text = string.Empty;
            txtIcon.Text = string.Empty;
            txtParentMenu.Text = string.Empty;
            rtbDesc.Text = string.Empty;
            tsslMsg.Text = string.Empty;

            smv.ID = -1;
            smv.ParentID = null;
            txtName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtName.Text.Trim()))
                {
                    tsslMsg.Text = "请输入菜单名";
                    txtName.Focus();
                    return;
                }
                smv.MenuName = txtName.Text.Trim();
                smv.CmdForm = txtForm.Text.Trim();
                smv.CmdExe = txtExe.Text.Trim();
                smv.Icon = txtIcon.Text.Trim();
                smv.Description = rtbDesc.Text.Trim();
                smv.CreatedBy = PubInfoUtil.accountEasyVO.Code;
                smv.UpdatedBy = PubInfoUtil.accountEasyVO.Code;

                sic.saveMenu(smv);
                tsmiRefresh_Click(sender, e);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    
    }
}
