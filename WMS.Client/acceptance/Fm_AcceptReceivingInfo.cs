using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMS.Client.AcceptSvr;
using Extension.Util.Archer;
using WMS.Client.util;
using Common.Util.Archer;



namespace WMS.Client.acceptance
{
    public partial class Fm_AcceptReceivingInfo : Form
    {
        public Fm_AcceptReceivingInfo()
        {
            InitializeComponent();
        }

        public Fm_AcceptReceivingInfo(AcceptReceivingVO pARVO, bool pIsEdit = false)
            : this()
        {
            arv = pARVO;
            isEdit = pIsEdit;
        }

        AcceptanceClient ac = null;
        AcceptReceivingVO arv = null;
        bool isEdit = false;

        private void Fm_AcceptReceivingInfo_Load(object sender, EventArgs e)
        {
            ac = new AcceptanceClient();

            if(isEdit)
            {
                tableLayoutPanel1.Enabled = false;
                btnOK.Enabled = false;
            }
            else
            {
                tableLayoutPanel1.Enabled = true;
                btnOK.Enabled = true;
            }

            txtNo.Text = arv.No;
            txtTrackingNumber.Text = arv.TrackingNumber;
            txtNames.Text = arv.Names;
            txtShipAddress.Text = arv.ShipAddress;
            txtShipper.Text = arv.Shipper;
            cmbUnit.Text = arv.Unit;
            nudPieces.Value = arv.Pieces.IsNullOrEmpty() ? 0 : int.Parse(arv.Pieces.ToString());
            txtWeight.Text = arv.Weight.IsNullOrEmpty() ? "" : arv.Weight.ToString();
            cmbDeliveryCompany.Text = arv.DeliveryCompany;
            txtDeliveryMan.Text = arv.DeliveryMan;
            cmbDeliveryMode.Text = arv.DeliveryMode;
            dtpReceiveDate.Value = arv.ReceivingDate.IsNullOrEmpty() ? DateTime.Now : DateTime.Parse(arv.ReceivingDate.ToString());
            txtReceivePerson.Text = arv.ReceivingPerson;
            rtbRemark.Text = arv.Remark;
            chkNotice.Checked=arv.IsNotice.IsNullOrEmpty()?false:(arv.IsNotice==0?false:true);

            txtReceivePerson.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtNames.Text.Trim().IsNullOrEmpty())
                {
                    txtNames.Focus();
                    return;
                }

                arv.Names = txtNames.Text.Trim();
                arv.TrackingNumber = txtTrackingNumber.Text.Trim();
                arv.ShipAddress = txtShipAddress.Text.Trim();
                arv.Shipper = txtShipper.Text.Trim();
                arv.Unit = cmbUnit.Text.Trim();
                arv.Pieces = int.Parse(nudPieces.Value.ToString());
                arv.Weight = txtWeight.Text.Trim().IsNullOrEmpty() ? 0 : decimal.Parse(txtWeight.Text.Trim());
                arv.DeliveryCompany = cmbDeliveryCompany.Text.Trim();
                arv.DeliveryMan = txtDeliveryMan.Text.Trim();
                arv.DeliveryMode = cmbDeliveryMode.Text.Trim();
                arv.ReceivingDate = dtpReceiveDate.Value;
                arv.ReceivingPerson = txtReceivePerson.Text.Trim();
                arv.StorageLocation = txtStorageLocation.Text.Trim();
                arv.Remark = rtbRemark.Text.Trim();
                arv.IsNotice = chkNotice.Checked ? 1 : 0;
                arv.CreatedBy = PubInfoUtil.accountEasyVO.Code;
                arv.UpdatedBy = PubInfoUtil.accountEasyVO.Code;

                ac.saveAcceptReceiving(arv);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            //验证重量输入内容为浮点数
            if(txtWeight.Text.Trim().IsNullOrEmpty())
            {
                return;
            }
            if(!RegexUtil.IsFloatNumber(txtWeight.Text.Trim()))
            {
                txtWeight.Text = string.Empty;
            }
        }
    }
}
