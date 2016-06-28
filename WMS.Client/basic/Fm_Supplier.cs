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
    public partial class Fm_Supplier : Form
    {
        public Fm_Supplier()
        {
            InitializeComponent();
        }

        BasicClient bc = null;
        SupplierVO sv = null;

        private void Fm_Supplier_Load(object sender, EventArgs e)
        {
            bc = new BasicClient();
            sv = new SupplierVO();
            tsmiRefresh_Click(sender, e);
        }

        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            resetText();
            splitContainer1.Panel1Collapsed = false;
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            if (dgvSupplier.SelectedRows.Count <= 0)
            {
                return;
            }
            txtCode.Text = dgvSupplier.SelectedRows[0].GetString("Code");
            txtName.Text = dgvSupplier.SelectedRows[0].GetString("SupplierName");
            txtContacts.Text = dgvSupplier.SelectedRows[0].GetString("Contacts");
            txtTel.Text = dgvSupplier.SelectedRows[0].GetString("Tel");
            txtEmail.Text = dgvSupplier.SelectedRows[0].GetString("Email");
            txtFax.Text = dgvSupplier.SelectedRows[0].GetString("Fax");
            txtAddress.Text = dgvSupplier.SelectedRows[0].GetString("Address");
            rtbDesc.Text = dgvSupplier.SelectedRows[0].GetString("Description");

            sv.ID = dgvSupplier.SelectedRows[0].GetInt32("ID");
            txtCode.Focus();
            splitContainer1.Panel1Collapsed = false;
        }

        private void tsmiEnable_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSupplier.SelectedRows.Count <= 0)
                {
                    return;
                }
                bc.enableAndDisableSupplierBySupplierID(dgvSupplier.SelectedRows[0].GetInt32("ID"), 1, PubInfoUtil.accountEasyVO.Code);
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
                if (dgvSupplier.SelectedRows.Count <= 0)
                {
                    return;
                }
                bc.enableAndDisableSupplierBySupplierID(dgvSupplier.SelectedRows[0].GetInt32("ID"), 0, PubInfoUtil.accountEasyVO.Code);
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

                dgvSupplier.AutoGenerateColumns = false;
                dgvSupplier.DataSource = bc.getSupplierList();
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
            txtContacts.Text = string.Empty;
            txtTel.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtFax.Text = string.Empty;
            txtAddress.Text = string.Empty;
            rtbDesc.Text = string.Empty;
            tsslMsg.Text = string.Empty;

            sv.ID = -1;
            txtCode.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text.IsNullOrEmpty())
                {
                    tsslMsg.Text = "请填写供应商编号";
                    txtCode.Focus();
                    return;
                }

                sv.Code = txtCode.Text.Trim();
                sv.SupplierName = txtName.Text.Trim();
                sv.Contacts = txtContacts.Text.Trim();
                sv.Tel = txtTel.Text.Trim();
                sv.Email = txtEmail.Text.Trim();
                sv.Fax = txtFax.Text.Trim();
                sv.Address = txtAddress.Text.Trim();
                sv.Description = rtbDesc.Text.Trim();
                sv.CreatedBy = PubInfoUtil.accountEasyVO.Code;
                sv.UpdatedBy = PubInfoUtil.accountEasyVO.Code;

                bc.saveSupplier(sv);
                tsmiRefresh_Click(sender, e);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
