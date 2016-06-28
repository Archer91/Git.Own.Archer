using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMS.Client.BasicSvr;
using Extension.Util.Archer;
using WMS.Client.util;

namespace WMS.Client.basic
{
    public partial class Fm_Material : Form
    {
        public Fm_Material()
        {
            InitializeComponent();
        }

        BasicClient bc = null;
        MaterialVO mv = null;

        private void Fm_Material_Load(object sender, EventArgs e)
        {
            bc = new BasicClient();
            mv = new MaterialVO();
            tsmiRefresh_Click(sender, e);
            tsmiRefreshCategory_Click(sender, e);
        }

        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            resetText();
            splitContainer1.Panel1Collapsed = false;
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            if (dgvMaterial.SelectedRows.Count <= 0)
            {
                return;
            }
            txtCode.Text = dgvMaterial.SelectedRows[0].GetString("Code");
            txtName.Text = dgvMaterial.SelectedRows[0].GetString("ItemName");
            cmbSupplier.Text = dgvMaterial.SelectedRows[0].GetString("SupplierDesc");
            cmbType.Text = dgvMaterial.SelectedRows[0].GetString("Type");
            cmbCategory.Text = dgvMaterial.SelectedRows[0].GetString("CategoryDesc");
            rtbDesc.Text = dgvMaterial.SelectedRows[0].GetString("Description");

            mv.ID = dgvMaterial.SelectedRows[0].GetInt32("ID");
            mv.SupplierID = dgvMaterial.SelectedRows[0].GetInt32("SupplierID");
            mv.CategoryID = dgvMaterial.SelectedRows[0].GetInt32("CategoryID");
            txtCode.Focus();
            splitContainer1.Panel1Collapsed = false;
        }

        private void tsmiEnable_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvMaterial.SelectedRows.Count <= 0)
                {
                    return;
                }
                bc.enableAndDisableMaterialByMaterialID(dgvMaterial.SelectedRows[0].GetInt32("ID"), 1, PubInfoUtil.accountEasyVO.Code);
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
                if (dgvMaterial.SelectedRows.Count <= 0)
                {
                    return;
                }
                bc.enableAndDisableMaterialByMaterialID(dgvMaterial.SelectedRows[0].GetInt32("ID"), 0, PubInfoUtil.accountEasyVO.Code);
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

                cmbSupplier.DisplayMember = "SupplierName";
                cmbSupplier.ValueMember = "ID";
                cmbSupplier.DataSource = bc.getSupplierList();

                dgvMaterial.AutoGenerateColumns = false;
                dgvMaterial.DataSource = bc.getMaterialList();
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
            cmbSupplier.Text = string.Empty;
            cmbType.Text = "C";
            cmbCategory.Text = string.Empty;
            rtbDesc.Text = string.Empty;
            tsslMsg.Text = string.Empty;

            mv.ID = -1;
            txtCode.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text.IsNullOrEmpty())
                {
                    tsslMsg.Text = "请填写物料编号";
                    txtCode.Focus();
                    return;
                }

                mv.Code = txtCode.Text.Trim();
                mv.ItemName = txtName.Text.Trim();
                if (!cmbSupplier.Text.Trim().IsNullOrEmpty())
                {
                    if (cmbSupplier.SelectedValue != null)
                    {
                        mv.SupplierID = int.Parse(cmbSupplier.SelectedValue.ToString());
                    }
                }
                mv.Type = cmbType.Text.Trim();
                if(!cmbCategory.Text.Trim().IsNullOrEmpty())
                {
                    if (cmbCategory.SelectedNode != null)
                    {
                        mv.CategoryID = int.Parse(cmbCategory.SelectedNode.Name);
                    }
                }
                mv.Description = rtbDesc.Text.Trim();
                mv.CreatedBy = PubInfoUtil.accountEasyVO.Code;
                mv.UpdatedBy = PubInfoUtil.accountEasyVO.Code;

                bc.saveMaterial(mv);
                tsmiRefresh_Click(sender, e);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmiAddCategory_Click(object sender, EventArgs e)
        {
            try
            {
                //if (null ==tvItemCategory.SelectedNode)
                //{
                //    return;
                //}

                ItemCategoryVO icv = new ItemCategoryVO();
                icv.ID = -1;
                icv.Status = 1;
                icv.CreatedBy = PubInfoUtil.accountEasyVO.Code;

                Fm_ItemCategory frmCategory = new Fm_ItemCategory(icv,"");
                frmCategory.ShowDialog();

                tsmiRefreshCategory_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmiSubAddCategory_Click(object sender, EventArgs e)
        {
            if (null == tvItemCategory.SelectedNode)
            {
                return;
            }

            ItemCategoryVO icv = new ItemCategoryVO();
            icv.ID = -1;
            icv.Status = 1;
            icv.ParentID = Int32.Parse(tvItemCategory.SelectedNode.Name);
            icv.CreatedBy = PubInfoUtil.accountEasyVO.Code;

            string parentCategory = tvItemCategory.SelectedNode.Text; 

            Fm_ItemCategory frmCategory = new Fm_ItemCategory(icv, parentCategory);
            frmCategory.ShowDialog();

            tsmiRefreshCategory_Click(sender, e);
        }

        private void tsmiEditCategory_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == tvItemCategory.SelectedNode)
                {
                    return;
                }
                ItemCategoryVO selectCategory = tvItemCategory.SelectedNode.Tag as ItemCategoryVO;

                string parentCategory = tvItemCategory.SelectedNode.Parent == null ? "" : tvItemCategory.SelectedNode.Parent.Text;

                Fm_ItemCategory frmCategory = new Fm_ItemCategory(selectCategory, parentCategory);
                frmCategory.ShowDialog();

                tsmiRefreshCategory_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmiEnableCategory_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == tvItemCategory.SelectedNode)
                {
                    return;
                }

                bc.enableAndDisableItemCategoryByItemCategoryID((tvItemCategory.SelectedNode.Tag as ItemCategoryVO).ID, 1, PubInfoUtil.accountEasyVO.Code);
                tsmiRefresh_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmiDisableCategory_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == tvItemCategory.SelectedNode)
                {
                    return;
                }

                bc.enableAndDisableItemCategoryByItemCategoryID((tvItemCategory.SelectedNode.Tag as ItemCategoryVO).ID, 0, PubInfoUtil.accountEasyVO.Code);
                tsmiRefresh_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmiRefreshCategory_Click(object sender, EventArgs e)
        {
            try
            {
                List<ItemCategoryVO> lst = bc.getItemCategoryList();
                bindItemCategory(lst);
                PubMethods.loadItemCategoryWithTreeView(tvItemCategory, lst);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bindItemCategory(List<ItemCategoryVO> pLst)
        {
            cmbCategory.Nodes.Clear();
            var topDept = from m in pLst
                          where m.ParentID == null
                          select m;
            foreach (var item in topDept)//顶层组织
            {
                TreeNode topTN = new TreeNode();
                topTN.Name = item.ID.ToString();
                topTN.Text = item.ItemCategoryName;
                topTN.ImageIndex = item.Status == 0 ? 1 : 0;
                topTN.Tag = item;
                //加载子节点
                bindSubNode(ref topTN, item.ID, pLst);

                cmbCategory.Nodes.Add(topTN);
            }

        }

        private static void bindSubNode(ref TreeNode pTopNode, int pNodeId, List<ItemCategoryVO> pLst)
        {
            var items = from m in pLst
                        where m.ParentID == pNodeId
                        select m;
            foreach (var item in items)
            {
                TreeNode subNode = new TreeNode();
                subNode.Name = item.ID.ToString();
                subNode.Text = item.ItemCategoryName;
                subNode.ImageIndex = item.Status == 0 ? 1 : 0;
                subNode.Tag = item;
                //递归加载节点
                bindSubNode(ref subNode, item.ID, pLst);

                pTopNode.Nodes.Add(subNode);
            }
        }
    }
}
