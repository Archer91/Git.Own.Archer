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
    public partial class Fm_SysStructure : Form
    {
        public Fm_SysStructure()
        {
            InitializeComponent();
        }

        SysInfoClient sic = null;
        SysStructureVO ssv = null;

        private void Fm_SysDepartment_Load(object sender, EventArgs e)
        {
            sic = new SysInfoClient();
            ssv = new SysStructureVO();
            tsmiRefresh_Click(sender, e);
        }

        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            resetText();
            splitContainer1.Panel1Collapsed = false;
        }

        private void tsmiSubAdd_Click(object sender, EventArgs e)
        {
            if (null == tvStructure.SelectedNode)
            {
                return;
            }
            resetText();
            txtParentStructure.Text = tvStructure.SelectedNode.Text;
            ssv.ParentID = Int32.Parse(tvStructure.SelectedNode.Name);
            splitContainer1.Panel1Collapsed = false;
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == tvStructure.SelectedNode)
                {
                    return;
                }
                SysStructureVO selectDept = tvStructure.SelectedNode.Tag as SysStructureVO;
                txtCode.Text = selectDept.Code;
                txtName.Text = selectDept.StructureName;
                rtbDesc.Text = selectDept.Description;
                txtParentStructure.Text = tvStructure.SelectedNode.Parent == null ? "" : tvStructure.SelectedNode.Parent.Text;
                cmbCompany.SelectedValue = selectDept.CompanyID;

                ssv.ID = Int32.Parse(tvStructure.SelectedNode.Name);
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
                if (null == tvStructure.SelectedNode)
                {
                    return;
                }

                sic.enableAndDisableByStructureId((tvStructure.SelectedNode.Tag as SysStructureVO).ID, 1, PubInfoUtil.accountEasyVO.Code);
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
                if (null == tvStructure.SelectedNode)
                {
                    return;
                }

                sic.enableAndDisableByStructureId((tvStructure.SelectedNode.Tag as SysStructureVO).ID, 0, PubInfoUtil.accountEasyVO.Code);
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

                cmbCompany.DisplayMember = "CompanyName";
                cmbCompany.ValueMember = "ID";
                cmbCompany.DataSource = sic.getCompanyEasyList();

                List<SysStructureVO> lst = sic.getStructureList();
                PubMethods.loadStructureWithTreeView(tvStructure, lst);

                List<SysRoleVO> lst2 = sic.getRoleList();
                PubMethods.loadRoleWithTreeView(tvRole, lst2);
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
            txtParentStructure.Text = string.Empty;
            cmbCompany.Text = string.Empty;
            rtbDesc.Text = string.Empty;
            tsslMSg.Text = string.Empty;

            ssv.ID = -1;
            ssv.ParentID = null;
            txtCode.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCode.Text) ||
                    string.IsNullOrEmpty(txtName.Text))
                {
                    tsslMSg.Text = "请填写组织编号和组织名称";
                    return;
                }

                ssv.Code = txtCode.Text.Trim();
                ssv.StructureName = txtName.Text.Trim();
                if(!cmbCompany.Text.Trim().IsNullOrEmpty())
                {
                    if (cmbCompany.SelectedValue != null)
                    {
                        ssv.CompanyID = Int32.Parse(cmbCompany.SelectedValue.ToString());
                    }
                }
                ssv.Description = rtbDesc.Text.Trim();
                ssv.CreatedBy = PubInfoUtil.accountEasyVO.Code;
                ssv.UpdatedBy = PubInfoUtil.accountEasyVO.Code;

                sic.saveStructure(ssv);
                tsmiRefresh_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tvStructure_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (null == tvStructure.SelectedNode)
                {
                    return;
                }

                List<SysRoleStructureVO> lst = sic.getRoleStructureByStructureId(Int32.Parse(tvStructure.SelectedNode.Name));
                PubMethods.bindWithTreeView(tvRole, lst);
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
                        tvUpCheck(tn, true, Int32.Parse(tvStructure.SelectedNode.Name));
                        tvDownCheck(tn, true,Int32.Parse(tvStructure.SelectedNode.Name));
                    }
                    else
                    {
                        tvDownCheck(tn, false, Int32.Parse(tvStructure.SelectedNode.Name));
                        tvUpCheck(tn, false, Int32.Parse(tvStructure.SelectedNode.Name));
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
                    sic.saveStructureId2Role( Int32.Parse(pTn.Name),pRID);
                }
                else
                {
                    pTn.Checked = pFlag;
                    //从数据表删除
                    sic.removeStructureId2Role(Int32.Parse(pTn.Name),pRID);

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
                    sic.saveStructureId2Role(Int32.Parse(pTn.Name),pRID);
                }
                foreach (TreeNode tn in pTn.Nodes)
                {
                    tn.Checked = pFlag;
                    tvDownCheck(tn, pFlag, pRID);
                    if (!pFlag)
                    {
                        //从数据表删除
                        sic.removeStructureId2Role(Int32.Parse(pTn.Name),pRID);
                    }
                }
            }
            else
            {
                if (pFlag)
                {
                    //保存到数据表
                    sic.saveStructureId2Role(Int32.Parse(pTn.Name), pRID);
                }
                else
                {
                    pTn.Checked = pFlag;
                    //从数据表删除
                    sic.removeStructureId2Role(Int32.Parse(pTn.Name), pRID);
                }
            }
        }
    }
}
