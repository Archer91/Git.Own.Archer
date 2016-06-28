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
    public partial class Fm_SysCompany : Form
    {
        public Fm_SysCompany()
        {
            InitializeComponent();
        }

        SysInfoClient sic = null;
        SysCompanyVO scv = null;

        private void Fm_SysCompany_Load(object sender, EventArgs e)
        {
            sic = new SysInfoClient();
            scv = new SysCompanyVO();
            tsmiRefresh_Click(sender, e);
        }

        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            resetText();
            splitContainer1.Panel1Collapsed = false;
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            if(dgvCompany.SelectedRows.Count <= 0)
            {
                return;
            }
            txtCode.Text = dgvCompany.SelectedRows[0].GetString("Code");
            txtName.Text = dgvCompany.SelectedRows[0].GetString("CompanyName");
            txtRegistrationNo.Text = dgvCompany.SelectedRows[0].GetString("RegistrationNo");
            txtCorporation.Text = dgvCompany.SelectedRows[0].GetString("Corporation");
            txtEmail.Text = dgvCompany.SelectedRows[0].GetString("Email");
            txtTel.Text = dgvCompany.SelectedRows[0].GetString("Tel");
            txtFax.Text = dgvCompany.SelectedRows[0].GetString("Fax");
            txtAddress.Text = dgvCompany.SelectedRows[0].GetString("Address");
            txtContacts.Text = dgvCompany.SelectedRows[0].GetString("Contacts");
            rtbDesc.Text = dgvCompany.SelectedRows[0].GetString("Description");

            scv.ID = dgvCompany.SelectedRows[0].GetInt32("ID");
            txtCode.Focus();
            splitContainer1.Panel1Collapsed = false;
        }

        private void tsmiEnable_Click(object sender, EventArgs e)
        {
            try
            {
                if(dgvCompany.SelectedRows.Count <= 0)
                {
                    return;
                }
                sic.enableAndDisableByCompanyId(dgvCompany.SelectedRows[0].GetInt32("ID"), 1, PubInfoUtil.accountEasyVO.Code);
                tsmiRefresh_Click(sender, e);
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
                if (dgvCompany.SelectedRows.Count <= 0)
                {
                    return;
                }
                sic.enableAndDisableByCompanyId(dgvCompany.SelectedRows[0].GetInt32("ID"), 0, PubInfoUtil.accountEasyVO.Code);
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

                dgvCompany.AutoGenerateColumns=false;
                dgvCompany.DataSource = sic.getCompanyList();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void resetText()
        {
            txtName.Text = string.Empty;
            txtCode.Text = string.Empty;
            txtRegistrationNo.Text = string.Empty;
            txtCorporation.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTel.Text = string.Empty;
            txtFax.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtContacts.Text = string.Empty;
            rtbDesc.Text = string.Empty;
            tsslMsg.Text = string.Empty;

            scv.ID = -1;
            txtCode.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtCode.Text.Trim())||
                    string.IsNullOrEmpty(txtName.Text.Trim()))
                {
                    tsslMsg.Text = "请填写公司编号和公司名称";
                    txtCode.Focus();
                    return;
                }

                scv.Code = txtCode.Text.Trim();
                scv.CompanyName = txtName.Text.Trim();
                scv.RegistrationNo = txtRegistrationNo.Text.Trim();
                scv.Corporation = txtCorporation.Text.Trim();
                scv.Email = txtEmail.Text.Trim();
                scv.Fax = txtFax.Text.Trim();
                scv.Tel = txtTel.Text.Trim();
                scv.Address = txtAddress.Text.Trim();
                scv.Contacts = txtContacts.Text.Trim();
                scv.Description = rtbDesc.Text.Trim();
                scv.CreatedBy = PubInfoUtil.accountEasyVO.Code;
                scv.UpdatedBy = PubInfoUtil.accountEasyVO.Code;

                sic.saveCompany(scv);
                tsmiRefresh_Click(sender, e);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
