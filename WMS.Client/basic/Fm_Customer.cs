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
using WMS.Client.util;
using Extension.Util.Archer;

namespace WMS.Client.basic
{
    public partial class Fm_Customer : Form
    {
        public Fm_Customer()
        {
            InitializeComponent();
        }

        BasicClient bc = null;
        CustomerVO cv = null;

        private void Fm_Customer_Load(object sender, EventArgs e)
        {
            bc = new BasicClient();
            cv = new CustomerVO();
            tsmiRefresh_Click(sender, e);
        }

        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            resetText();
            splitContainer1.Panel1Collapsed = false;
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            if (dgvCustomer.SelectedRows.Count <= 0)
            {
                return;
            }
            txtCode.Text = dgvCustomer.SelectedRows[0].GetString("Code");
            txtName.Text = dgvCustomer.SelectedRows[0].GetString("CustName");
            txtContacts.Text = dgvCustomer.SelectedRows[0].GetString("Contacts");
            txtTel.Text = dgvCustomer.SelectedRows[0].GetString("Tel");
            txtEmail.Text = dgvCustomer.SelectedRows[0].GetString("Email");
            txtFax.Text = dgvCustomer.SelectedRows[0].GetString("Fax");
            txtAddress.Text = dgvCustomer.SelectedRows[0].GetString("Address");
            rtbDesc.Text = dgvCustomer.SelectedRows[0].GetString("Description");

            cv.ID = dgvCustomer.SelectedRows[0].GetInt32("ID");
            txtCode.Focus();
            splitContainer1.Panel1Collapsed = false;
        }

        private void tsmiEnable_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCustomer.SelectedRows.Count <= 0)
                {
                    return;
                }
                bc.enableAndDisableCustomerByCustomerID(dgvCustomer.SelectedRows[0].GetInt32("ID"), 1, PubInfoUtil.accountEasyVO.Code);
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
                if (dgvCustomer.SelectedRows.Count <= 0)
                {
                    return;
                }
                bc.enableAndDisableCustomerByCustomerID(dgvCustomer.SelectedRows[0].GetInt32("ID"), 0, PubInfoUtil.accountEasyVO.Code);
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

                dgvCustomer.AutoGenerateColumns = false;
                dgvCustomer.DataSource = bc.getCustomerList();
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

            cv.ID = -1;
            txtCode.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text.IsNullOrEmpty())
                {
                    tsslMsg.Text = "请填写客户编号";
                    txtCode.Focus();
                    return;
                }

                cv.Code = txtCode.Text.Trim();
                cv.CustName = txtName.Text.Trim();
                cv.Contacts = txtContacts.Text.Trim();
                cv.Tel = txtTel.Text.Trim();
                cv.Email = txtEmail.Text.Trim();
                cv.Fax = txtFax.Text.Trim();
                cv.Address = txtAddress.Text.Trim();
                cv.Description = rtbDesc.Text.Trim();
                cv.CreatedBy = PubInfoUtil.accountEasyVO.Code;
                cv.UpdatedBy = PubInfoUtil.accountEasyVO.Code;

                bc.saveCustomer(cv);
                tsmiRefresh_Click(sender, e);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
