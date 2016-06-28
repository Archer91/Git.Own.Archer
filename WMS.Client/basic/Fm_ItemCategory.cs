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
    public partial class Fm_ItemCategory : Form
    {
        public Fm_ItemCategory()
        {
            InitializeComponent();
        }

        public Fm_ItemCategory(ItemCategoryVO pICV,string pParentCategory)
            : this()
        {
            icv = pICV;
            txtParentCategory.Text = pParentCategory;
        }

        BasicClient bc = null;
        ItemCategoryVO icv = null;

        private void Fm_ItemCategory_Load(object sender, EventArgs e)
        {
            bc = new BasicClient();

            txtName.Text = icv.ItemCategoryName;
            rtbDesc.Text = icv.Description;

            txtName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtName.Text.Trim()))
                {
                    txtName.Focus();
                    return;
                }

                icv.ItemCategoryName = txtName.Text.Trim();
                icv.Description = rtbDesc.Text.Trim();
                icv.CreatedBy = PubInfoUtil.accountEasyVO.Code;
                icv.UpdatedBy = PubInfoUtil.accountEasyVO.Code;

                bc.saveItemCategory(icv);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
