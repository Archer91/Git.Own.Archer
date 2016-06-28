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
using WMS.Client.util;
using Extension.Util.Archer;

namespace WMS.Client.acceptance
{
    public partial class Fm_AcceptReceiving : Form
    {
        public Fm_AcceptReceiving()
        {
            InitializeComponent();
        }

        AcceptanceClient ac = null;

        private void Fm_AcceptReceiving_Load(object sender, EventArgs e)
        {
            ac = new AcceptanceClient();
            tsmiRefresh_Click(sender, e);
        }

        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            try
            {
                AcceptReceivingVO arv = new AcceptReceivingVO();
                arv.ID = -1;
                arv.CompanyID = PubInfoUtil.CurrentCompanyId;
                arv.No = string.Empty;
                arv.Status = 1;
                arv.CreatedBy = PubInfoUtil.accountEasyVO.Code;
                Fm_AcceptReceivingInfo frmInfo = new Fm_AcceptReceivingInfo(arv);
                frmInfo.ShowDialog();

                tsmiRefresh_Click(sender, e);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == dgvReceiving.CurrentRow)
                {
                    return;
                }

                AcceptReceivingVO arv = new AcceptReceivingVO();
                arv.ID = dgvReceiving.CurrentRow.GetInt32("ID");
                arv.CompanyID = dgvReceiving.CurrentRow.GetInt32("CompanyID");
                arv.No = dgvReceiving.CurrentRow.GetString("No");
                arv.TrackingNumber = dgvReceiving.CurrentRow.GetString("TrackingNumber");
                arv.Names = dgvReceiving.CurrentRow.GetString("Names");
                arv.ShipAddress = dgvReceiving.CurrentRow.GetString("ShipAddress");
                arv.Shipper = dgvReceiving.CurrentRow.GetString("Shipper");
                arv.Unit = dgvReceiving.CurrentRow.GetString("Unit");
                arv.Pieces = dgvReceiving.CurrentRow.GetInt32("Pieces");
                arv.Weight = dgvReceiving.CurrentRow.GetDecimal("Weight");
                arv.DeliveryCompany = dgvReceiving.CurrentRow.GetString("DeliveryCompany");
                arv.DeliveryMan = dgvReceiving.CurrentRow.GetString("DeliveryMan");
                arv.DeliveryMode = dgvReceiving.CurrentRow.GetString("DeliveryMode");
                arv.ReceivingDate = dgvReceiving.CurrentRow.GetDateTimeNullable("ReceivingDate");
                arv.ReceivingPerson = dgvReceiving.CurrentRow.GetString("ReceivingPerson");
                arv.IsNotice = dgvReceiving.CurrentRow.GetInt32("IsNotice");
                arv.Remark = dgvReceiving.CurrentRow.GetString("Remark");
                arv.Status = dgvReceiving.CurrentRow.GetInt32("Status");
                arv.UpdatedBy = PubInfoUtil.accountEasyVO.Code;

                Fm_AcceptReceivingInfo frmInfo = new Fm_AcceptReceivingInfo(arv,true);
                frmInfo.ShowDialog();

                tsmiRefresh_Click(sender, e);
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
                if (null == dgvReceiving.CurrentRow)
                {
                    return;
                }

                ac.enableAndDisableReceivingByReceiveID(dgvReceiving.CurrentRow.GetInt32("ID"), 1, PubInfoUtil.accountEasyVO.Code);
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
                if (null == dgvReceiving.CurrentRow)
                {
                    return;
                }

                ac.enableAndDisableReceivingByReceiveID(dgvReceiving.CurrentRow.GetInt32("ID"), 0, PubInfoUtil.accountEasyVO.Code);
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
                dgvReceiving.AutoGenerateColumns = false;
                dgvReceiving.DataSource = ac.getAcceptReceivingList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvSupplier_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tsmiEdit_Click(sender, e);
        }
    }
}
