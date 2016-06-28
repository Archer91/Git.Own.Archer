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
using WMS.Client.SysInfoSvr;
using WMS.Client.util;
using Extension.Util.Archer;

namespace WMS.Client.basic
{
    public partial class Fm_Warehouse : Form
    {
        public Fm_Warehouse()
        {
            InitializeComponent();
        }

        SysInfoClient sic = null;
        BasicClient bc = null;
        WarehouseVO wv = null;

        private void Fm_Warehouse_Load(object sender, EventArgs e)
        {
            sic = new SysInfoClient();
            bc = new BasicClient();
            wv = new WarehouseVO();

            tsmiRefresh_Click(sender, e);
        }

        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            resetText();
            splitContainer1.Panel1Collapsed = false;
        }

        private void tsmiSubAdd_Click(object sender, EventArgs e)
        {
            if (null == tvWarehouse.SelectedNode)
            {
                return;
            }
            resetText();
            txtParentWarehouse.Text = tvWarehouse.SelectedNode.Text;
            wv.ParentID = Int32.Parse(tvWarehouse.SelectedNode.Name);
            splitContainer1.Panel1Collapsed = false;
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == tvWarehouse.SelectedNode)
                {
                    return;
                }
                WarehouseVO selectWare = tvWarehouse.SelectedNode.Tag as WarehouseVO;
                txtCode.Text = selectWare.Code;
                txtName.Text = selectWare.WareName;
                rtbDesc.Text = selectWare.Description;
                txtParentWarehouse.Text = tvWarehouse.SelectedNode.Parent == null ? "" : tvWarehouse.SelectedNode.Parent.Text;
                cmbCompany.SelectedValue = selectWare.CompanyID;

                wv.ID = Int32.Parse(tvWarehouse.SelectedNode.Name);
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
                if (null == tvWarehouse.SelectedNode)
                {
                    return;
                }

                bc.enableAndDisableWarehouseByWareID((tvWarehouse.SelectedNode.Tag as WarehouseVO).ID, 1, PubInfoUtil.accountEasyVO.Code);
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
                if (null == tvWarehouse.SelectedNode)
                {
                    return;
                }

                bc.enableAndDisableWarehouseByWareID((tvWarehouse.SelectedNode.Tag as WarehouseVO).ID, 0, PubInfoUtil.accountEasyVO.Code);
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

                List<WarehouseVO> lst = bc.getWarehouseList();
                PubMethods.loadWarehouseWithTreeView(tvWarehouse, lst);

                dgvLocation.DataSource = null;
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
            cmbCompany.Text = string.Empty;
            txtParentWarehouse.Text = string.Empty;
            rtbDesc.Text = string.Empty;
            tsslMsg.Text = string.Empty;

            wv.ID = -1;
            wv.ParentID = null;
            txtCode.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCode.Text))
                {
                    tsslMsg.Text = "请填写仓库编号";
                    return;
                }

                wv.Code = txtCode.Text.Trim();
                wv.WareName = txtName.Text.Trim();
                if (!cmbCompany.Text.Trim().IsNullOrEmpty())
                {
                    if (cmbCompany.SelectedValue != null)
                    {
                        wv.CompanyID = Int32.Parse(cmbCompany.SelectedValue.ToString());
                    }
                }
                wv.Description = rtbDesc.Text.Trim();
                wv.CreatedBy = PubInfoUtil.accountEasyVO.Code;
                wv.UpdatedBy = PubInfoUtil.accountEasyVO.Code;

                bc.saveWarehouse(wv);
                tsmiRefresh_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tvWarehouse_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (null == tvWarehouse.SelectedNode)
                {
                    return;
                }

                dgvLocation.AutoGenerateColumns = false;
                dgvLocation.DataSource = bc.getLocationByWarehouseID(Int32.Parse(tvWarehouse.SelectedNode.Name)); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmiAddLocation_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == tvWarehouse.SelectedNode)
                {
                    return;
                }

                LocationVO lv = new LocationVO();
                lv.ID = -1;
                lv.Status = 1;
                lv.StatusDesc = "启用";
                lv.WarehouseDesc = tvWarehouse.SelectedNode.Text;
                lv.WarehouseID = Int32.Parse(tvWarehouse.SelectedNode.Name);
                lv.CreatedBy = PubInfoUtil.accountEasyVO.Code;

                Fm_Location frmLocation = new Fm_Location(lv);
                frmLocation.ShowDialog();

                tsmiRefreshLocation_Click(sender, e);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmiEditLocation_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == tvWarehouse.SelectedNode)
                {
                    return;
                }
                if (null == dgvLocation.CurrentRow)
                {
                    return;
                }

                LocationVO lv = new LocationVO();
                lv.ID = dgvLocation.CurrentRow.GetInt32("ID");
                lv.Code = dgvLocation.CurrentRow.GetString("Code");
                lv.LocationName = dgvLocation.CurrentRow.GetString("LocationName");
                lv.Description = dgvLocation.CurrentRow.GetString("Description");
                lv.WarehouseID = dgvLocation.CurrentRow.GetInt32("WarehouseID");
                lv.WarehouseDesc = dgvLocation.CurrentRow.GetString("WarehouseDesc");
                lv.UpdatedBy = PubInfoUtil.accountEasyVO.Code;

                Fm_Location frmLocation = new Fm_Location(lv);
                frmLocation.ShowDialog();

                tsmiRefreshLocation_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmiEnableLocation_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == tvWarehouse.SelectedNode)
                {
                    return;
                }
                if (null == dgvLocation.CurrentRow)
                {
                    return;
                }

                bc.enableAndDisableLocationByLocationID(dgvLocation.CurrentRow.GetInt32("ID"), 1, PubInfoUtil.accountEasyVO.Code);
                tsmiRefreshLocation_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmiDisableLocation_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == tvWarehouse.SelectedNode)
                {
                    return;
                }
                if(null == dgvLocation.CurrentRow)
                {
                    return;
                }

                bc.enableAndDisableLocationByLocationID(dgvLocation.CurrentRow.GetInt32("ID"), 0, PubInfoUtil.accountEasyVO.Code);
                tsmiRefreshLocation_Click(sender, e);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmiRefreshLocation_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == tvWarehouse.SelectedNode)
                {
                    return;
                }

                dgvLocation.AutoGenerateColumns = false;
                dgvLocation.DataSource = null;
                dgvLocation.DataSource = bc.getLocationByWarehouseID(Int32.Parse(tvWarehouse.SelectedNode.Name));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvLocation_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tsmiEditLocation_Click(sender, e);
        }

       
    }
}
