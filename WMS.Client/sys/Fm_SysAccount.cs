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
    public partial class Fm_SysAccount : Form
    {
        public Fm_SysAccount()
        {
            InitializeComponent();
        }

        SysInfoClient sic = null;
        SysAccountVO sav = null;
        private void Fm_SysAccount_Load(object sender, EventArgs e)
        {
            sic = new SysInfoClient();
            sav = new SysAccountVO();
            tsmiRefresh_Click(sender, e);
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

            txtCode.Text = dgvAccount.SelectedRows[0].GetString("Code");
            txtName.Text = dgvAccount.SelectedRows[0].GetString("AccountName");
            cmbStructure.Text = dgvAccount.SelectedRows[0].GetString("StructureDesc");
            rtbDesc.Text = dgvAccount.SelectedRows[0].GetString("Description");
            txtEngName.Text = dgvAccount.SelectedRows[0].GetString("EnglishName");
            txtEmail.Text = dgvAccount.SelectedRows[0].GetString("Email");
            txtTel.Text = dgvAccount.SelectedRows[0].GetString("Tel");
            txtAddress.Text = dgvAccount.SelectedRows[0].GetString("Address");
            txtEmergencyContacts.Text = dgvAccount.SelectedRows[0].GetString("EmergencyContacts");
            txtEmergencyPhone.Text = dgvAccount.SelectedRows[0].GetString("EmergencyPhone");
            if (!dgvAccount.SelectedRows[0].GetDateTimeNullable("HireDate").IsNullOrEmpty())
            {
                dtpHireDate.Value = DateTime.Parse(dgvAccount.SelectedRows[0].GetDateTimeNullable("HireDate").ToString());
            }
            nudAge.Value = dgvAccount.SelectedRows[0].GetDecimal("Age");
            cmbSex.Text = dgvAccount.SelectedRows[0].GetString("Sex");
            cmbEducation.Text = dgvAccount.SelectedRows[0].GetString("Education");
            cmbMarriage.Text = dgvAccount.SelectedRows[0].GetString("Marriage");

            sav.ID = dgvAccount.SelectedRows[0].GetInt32("ID");
            sav.StructureID = dgvAccount.SelectedRows[0].GetInt32("StructureID");
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

                sic.enableAndDisableByAccountId(dgvAccount.SelectedRows[0].GetInt32("ID"), 1, PubInfoUtil.accountEasyVO.Code);
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

                sic.enableAndDisableByAccountId(dgvAccount.SelectedRows[0].GetInt32("ID"), 0, PubInfoUtil.accountEasyVO.Code);
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

                List<SysStructureVO> lst = sic.getStructureList();
                bindStructure(lst);

                List<SysRoleVO> lst2 = sic.getRoleList();
                PubMethods.loadRoleWithTreeView(tvRole, lst2);

                dgvAccount.AutoGenerateColumns = false;
                dgvAccount.DataSource = sic.getAccountList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmiResetPwd_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAccount.SelectedRows.Count <= 0)
                {
                    return;
                }

                if(sic.resetPassword(dgvAccount.SelectedRows[0].GetString("Code"),"123456"))
                {
                    tsslMsg.Text = "重置口令成功";
                }
                else
                {
                    tsslMsg.Text = "重置口令失败";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bindStructure(List<SysStructureVO> pLst)
        {
            cmbStructure.Nodes.Clear();
            var topDept = from m in pLst
                          where m.ParentID == null
                          select m;
            foreach (var item in topDept)//顶层组织
            {
                TreeNode topTN = new TreeNode();
                topTN.Name = item.ID.ToString();
                topTN.Text = item.StructureName;
                topTN.ImageIndex = item.Status == 0 ? 1 : 0;
                topTN.Tag = item;
                //加载子节点
                bindSubNode(ref topTN, item.ID, pLst);

                cmbStructure.Nodes.Add(topTN);
            }
            
        }

        private static void bindSubNode(ref TreeNode pTopNode, int pNodeId, List<SysStructureVO> pLst)
        {
            var items = from m in pLst
                        where m.ParentID == pNodeId
                        select m;
            foreach (var item in items)
            {
                TreeNode subNode = new TreeNode();
                subNode.Name = item.ID.ToString();
                subNode.Text = item.StructureName;
                subNode.ImageIndex = item.Status == 0 ? 1 : 0;
                subNode.Tag = item;
                //递归加载节点
                bindSubNode(ref subNode, item.ID, pLst);

                pTopNode.Nodes.Add(subNode);
            }
        }

        private void resetText()
        {
            txtCode.Text = string.Empty;
            txtName.Text = string.Empty;
            txtEngName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTel.Text = string.Empty;
            txtAddress.Text = string.Empty;
            nudAge.Value = 16;
            cmbSex.Text = "男";
            cmbEducation.Text = "本科";
            cmbMarriage.Text = "否";
            dtpHireDate.Value = DateTime.Today;
            txtEmergencyContacts.Text = string.Empty;
            txtEmergencyContacts.Text = string.Empty;
            rtbDesc.Text = string.Empty;
            cmbStructure.Text = string.Empty;
            tsslMsg.Text = string.Empty;

            sav.ID = -1;
            txtCode.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCode.Text))
                {
                    MessageBox.Show("请填写用户编号");
                    return;
                }

                sav.Code = txtCode.Text.Trim();
                sav.AccountName = txtName.Text.Trim();
                sav.Description = rtbDesc.Text.Trim();
                sav.EnglishName = txtEngName.Text.Trim();
                sav.Email = txtEmail.Text.Trim();
                sav.Tel = txtTel.Text.Trim();
                sav.Address = txtAddress.Text.Trim();
                sav.EmergencyContacts = txtEmergencyContacts.Text.Trim();
                sav.EmergencyPhone = txtEmergencyPhone.Text.Trim();
                sav.Age = Int32.Parse(nudAge.Value.ToString());
                sav.Sex = cmbSex.Text.Trim();
                sav.Education = cmbEducation.Text.Trim();
                sav.Marriage = cmbMarriage.Text.Trim();
                sav.HireDate = dtpHireDate.Value;
                sav.CreatedBy = PubInfoUtil.accountEasyVO.Code;
                sav.UpdatedBy = PubInfoUtil.accountEasyVO.Code;
                if(!cmbStructure.Text.Trim().IsNullOrEmpty())
                {
                    sav.StructureID = Int32.Parse(cmbStructure.SelectedNode.Name);
                }

                sic.saveAccount(sav);
                tsmiRefresh_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvAccount_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvAccount.SelectedRows.Count <= 0)
                {
                    return;
                }

                List<SysRoleAccountVO> lst = sic.getRoleAccountByAccountId(dgvAccount.SelectedRows[0].GetInt32("ID"));
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
                        tvUpCheck(tn, true, dgvAccount.SelectedRows[0].GetInt32("ID"));
                        tvDownCheck(tn, true, dgvAccount.SelectedRows[0].GetInt32("ID"));
                    }
                    else
                    {
                        tvDownCheck(tn, false, dgvAccount.SelectedRows[0].GetInt32("ID"));
                        tvUpCheck(tn, false, dgvAccount.SelectedRows[0].GetInt32("ID"));
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
                    sic.saveAccountId2Role(Int32.Parse(pTn.Name), pRID);
                }
                else
                {
                    pTn.Checked = pFlag;
                    //从数据表删除
                    sic.removeAccountId2Role(Int32.Parse(pTn.Name), pRID);

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
                    sic.saveAccountId2Role(Int32.Parse(pTn.Name), pRID);
                }
                foreach (TreeNode tn in pTn.Nodes)
                {
                    tn.Checked = pFlag;
                    tvDownCheck(tn, pFlag, pRID);
                    if (!pFlag)
                    {
                        //从数据表删除
                        sic.removeAccountId2Role(Int32.Parse(pTn.Name), pRID);
                    }
                }
            }
            else
            {
                if (pFlag)
                {
                    //保存到数据表
                    sic.saveAccountId2Role(Int32.Parse(pTn.Name), pRID);
                }
                else
                {
                    pTn.Checked = pFlag;
                    //从数据表删除
                    sic.removeAccountId2Role(Int32.Parse(pTn.Name), pRID);
                }
            }
        }

       
    }
}
