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
using Extension.Util.Archer;
using WMS.Client.util;

namespace WMS.Client.basic
{
    public partial class Fm_SerialNumber : Form
    {
        public Fm_SerialNumber()
        {
            InitializeComponent();
        }

        SysInfoClient sic = null;
        BasicClient bc = null;
        SerialNumberVO snv = null;

        private void Fm_SerialNumber_Load(object sender, EventArgs e)
        {
            sic = new SysInfoClient();
            bc = new BasicClient();
            snv = new SerialNumberVO();

            tsmiRefresh_Click(sender, e);
        }

        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            resetText();
            splitContainer1.Panel1Collapsed = false;
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            if (dgvSerialNumber.SelectedRows.Count <= 0)
            {
                return;
            }
            cmbType.Text = dgvSerialNumber.SelectedRows[0].GetString("Type");
            cmbCompany.Text = dgvSerialNumber.SelectedRows[0].GetString("CompanyDesc");
            txtPrefix.Text = dgvSerialNumber.SelectedRows[0].GetString("Prefix");
            cmbYMD.Text = dgvSerialNumber.SelectedRows[0].GetString("YYMMDD");
            txtSuffix.Text = dgvSerialNumber.SelectedRows[0].GetString("Suffix");
            tcbSeqLength.Value = dgvSerialNumber.SelectedRows[0].GetInt32("SeqLength");
            nudSeqStep.Value = dgvSerialNumber.SelectedRows[0].GetInt32("SeqStep");
            rtbDesc.Text = dgvSerialNumber.SelectedRows[0].GetString("Description");

            snv.ID = dgvSerialNumber.SelectedRows[0].GetInt32("ID");
            cmbType.Focus();
            splitContainer1.Panel1Collapsed = false;
        }

        private void tsmiEnable_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSerialNumber.SelectedRows.Count <= 0)
                {
                    return;
                }
                bc.enableAndDisableSerialNumberBySerialID(dgvSerialNumber.SelectedRows[0].GetInt32("ID"), 1, PubInfoUtil.accountEasyVO.Code);
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
                if (dgvSerialNumber.SelectedRows.Count <= 0)
                {
                    return;
                }
                bc.enableAndDisableSerialNumberBySerialID(dgvSerialNumber.SelectedRows[0].GetInt32("ID"), 0, PubInfoUtil.accountEasyVO.Code);
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

                dgvSerialNumber.AutoGenerateColumns = false;
                dgvSerialNumber.DataSource = bc.getSerialNumberList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void resetText()
        {
            cmbType.Text = string.Empty;
            cmbCompany.Text = string.Empty;
            txtPrefix.Text = string.Empty;
            cmbYMD.Text = string.Empty;
            txtSuffix.Text = string.Empty;
            tcbSeqLength.Value = 1;
            nudSeqStep.Value = 1;
            rtbDesc.Text = string.Empty;
            tsslMsg.Text = string.Empty;

            snv.ID = -1;
            cmbType.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbType.Text.IsNullOrEmpty())
                {
                    tsslMsg.Text = "请填写类型名";
                    cmbType.Focus();
                    return;
                }
                if (cmbCompany.Text.IsNullOrEmpty())
                {
                    tsslMsg.Text = "请选择所属公司";
                    cmbCompany.Focus();
                    return;
                }

                snv.Type = cmbType.Text.Trim();
                if (cmbCompany.SelectedValue != null)
                {
                    snv.CompanyID = Int32.Parse(cmbCompany.SelectedValue.ToString());
                }

                snv.Prefix = txtPrefix.Text.Trim();
                snv.YYMMDD = cmbYMD.Text.Trim();
                snv.Suffix = txtSuffix.Text.Trim();
                snv.SeqLength = tcbSeqLength.Value;
                snv.SeqStep = Int32.Parse(nudSeqStep.Value.ToString());
                snv.Description = rtbDesc.Text.Trim();
                snv.CreatedBy = PubInfoUtil.accountEasyVO.Code;
                snv.UpdatedBy = PubInfoUtil.accountEasyVO.Code;

                bc.saveSerialNumber(snv);
                tsmiRefresh_Click(sender, e);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tcbSeqLength_Scroll(object sender, EventArgs e)
        {
            txtSeqLength.Text = tcbSeqLength.Value.ToString();
        }

        private void tcbSeqLength_ValueChanged(object sender, EventArgs e)
        {
            txtSeqLength.Text = tcbSeqLength.Value.ToString();
        }
    }
}
