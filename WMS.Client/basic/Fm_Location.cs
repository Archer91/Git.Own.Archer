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

namespace WMS.Client.basic
{
    public partial class Fm_Location : Form
    {
        public Fm_Location()
        {
            InitializeComponent();
        }

        public Fm_Location(LocationVO pLV)
            : this()
        {
            lv = pLV;
        }

        BasicClient bc = null;
        LocationVO lv = null;
        private void Fm_Location_Load(object sender, EventArgs e)
        {
            bc = new BasicClient();

            txtWarehouse.Text = lv.WarehouseDesc;
            txtCode.Text = lv.Code;
            txtName.Text = lv.LocationName;
            rtbDesc.Text = lv.Description;

            txtCode.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtCode.Text.Trim()))
                {
                    txtCode.Focus();
                    return;
                }

                lv.Code = txtCode.Text.Trim();
                lv.LocationName = txtName.Text.Trim();
                lv.Description = rtbDesc.Text.Trim();
                lv.CreatedBy = PubInfoUtil.accountEasyVO.Code;
                lv.UpdatedBy = PubInfoUtil.accountEasyVO.Code;

                bc.saveLocation(lv);

                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
