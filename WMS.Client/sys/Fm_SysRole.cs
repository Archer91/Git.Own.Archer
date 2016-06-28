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
using Extension.Util.Archer;

namespace WMS.Client.sys
{
    public partial class Fm_SysRole : Form
    {
        public Fm_SysRole()
        {
            InitializeComponent();
        }
        SysInfoClient sic = null;
        SysRoleVO srv = null;
        private void Fm_SysRole_Load(object sender, EventArgs e)
        {
            sic = new SysInfoClient();
            srv = new SysRoleVO();
            tsmiRefresh_Click(sender, e);
        }

        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            resetText();
            splitContainer1.Panel1Collapsed = false;
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvRole.SelectedRows.Count <= 0)
                {
                    return;
                }

                txtCode.Text = dgvRole.SelectedRows[0].GetString("Code");
                txtName.Text = dgvRole.SelectedRows[0].GetString("RoleName");
                rtbDesc.Text = dgvRole.SelectedRows[0].GetString("Description");

                srv.ID = dgvRole.SelectedRows[0].GetInt32("ID");
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
                if (dgvRole.SelectedRows.Count <= 0)
                {
                    return;
                }

                sic.enableAndDisableByRoleId(dgvRole.SelectedRows[0].GetInt32("ID"), 1, PubInfoUtil.accountEasyVO.Code);
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
                if (dgvRole.SelectedRows.Count <= 0)
                {
                    return;
                }

                sic.enableAndDisableByRoleId(dgvRole.SelectedRows[0].GetInt32("ID"), 0, PubInfoUtil.accountEasyVO.Code);
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

                dgvRole.AutoGenerateColumns = false;
                dgvRole.DataSource = sic.getRoleList();
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
            rtbDesc.Text = string.Empty;
            tsslMsg.Text = string.Empty;

            srv.ID = -1;
            txtCode.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCode.Text) ||
                    string.IsNullOrEmpty(txtName.Text))
                {
                    tsslMsg.Text = "请填写角色编号和角色名称";
                    txtCode.Focus();
                    return;
                }

                srv.Code = txtCode.Text.Trim();
                srv.RoleName = txtName.Text.Trim();
                srv.Description = rtbDesc.Text.Trim();
                srv.CreatedBy = PubInfoUtil.accountEasyVO.Code;
                srv.UpdatedBy = PubInfoUtil.accountEasyVO.Code;

                sic.saveRole(srv);
                tsmiRefresh_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvRole_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (null == dgvRole.CurrentRow)
                {
                    return;
                }

                List<SysRoleMenuVO> lst = sic.getRoleMenuByRoleId(dgvRole.CurrentRow.GetInt32("ID"));
                PubMethods.bindWithTreeView(tvMenu, lst);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tvMenu_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (dgvRole.SelectedRows.Count <= 0)
                {
                    return;
                }

                if (e.Action == TreeViewAction.ByMouse)
                {
                    TreeNode tn = e.Node;
                    if (tn.Checked)
                    {
                        tvUpCheck(tn, true, dgvRole.SelectedRows[0].GetInt32("ID"));
                        tvDownCheck(tn, true, dgvRole.SelectedRows[0].GetInt32("ID"));

                    }
                    else
                    {
                        tvDownCheck(tn, false, dgvRole.SelectedRows[0].GetInt32("ID"));
                        tvUpCheck(tn, false, dgvRole.SelectedRows[0].GetInt32("ID"));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 向上遍历节点
        /// </summary>
        /// <param name="pTn"></param>
        /// <param name="pFlag"></param>
        private void tvUpCheck(TreeNode pTn, bool pFlag, int pRID)
        {
            if (pTn != null)
            {
                if (pFlag)
                {
                    pTn.Checked = pFlag;
                    tvUpCheck(pTn.Parent, pFlag, pRID);

                    //保存到数据库
                    sic.saveMenuId2Role(pRID, Int32.Parse(pTn.Name));
                }
                else
                {
                    pTn.Checked = pFlag;
                    //从数据表删除
                    sic.removeMenuId2Role(pRID, Int32.Parse(pTn.Name));

                    if (pTn.Parent != null)
                    {
                        bool isCheck = false;
                        foreach (TreeNode tn in pTn.Parent.Nodes)
                        {
                            if (tn.Checked)
                            {
                                isCheck = true;
                                break;
                            }
                        }
                        if (!isCheck)
                        {
                            tvUpCheck(pTn.Parent, pFlag, pRID);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 向下遍历节点
        /// </summary>
        /// <param name="pTn"></param>
        /// <param name="pFlag"></param>
        private void tvDownCheck(TreeNode pTn, bool pFlag, int pRID)
        {
            if (pTn.Nodes.Count > 0)
            {
                if (pFlag)
                {
                    //保存到数据表
                    sic.saveMenuId2Role(pRID, Int32.Parse(pTn.Name));
                }
                foreach (TreeNode tn in pTn.Nodes)
                {
                    tn.Checked = pFlag;
                    tvDownCheck(tn, pFlag, pRID);
                    if (!pFlag)
                    {
                        //从数据表删除
                        sic.removeMenuId2Role(pRID, Int32.Parse(pTn.Name));
                    }
                }
            }
            else
            {
                if (pFlag)
                {
                    //保存到数据表
                    sic.saveMenuId2Role(pRID, Int32.Parse(pTn.Name));
                }
                else
                {
                    pTn.Checked = pFlag;
                    //从数据表删除
                    sic.removeMenuId2Role(pRID, Int32.Parse(pTn.Name));
                }
            }
        }
   
    }
}
