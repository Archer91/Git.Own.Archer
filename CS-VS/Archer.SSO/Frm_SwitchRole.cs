using Archer.SSO.helpclass;
using SSO.Lib.VO.Basic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Archer.SSO
{
    public partial class Frm_SwitchRole : Form
    {
        public Frm_SwitchRole()
        {
            InitializeComponent();
        }

        public Frm_SwitchRole(List<RoleRVO> pLst):this()
        {
            foreach (var item in pLst)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Name = item.RID.ToString();
                lvi.Text = item.RName;
                lvi.ImageIndex = 0;
                lstRoles.Items.Add(lvi);
            }
        }

        private void lstRoles_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ListView lv = sender as ListView;
                List<MenuVO> lst = PublicInfo.basicImpl.getMenuByRole(new Guid(lv.FocusedItem.Name));
                PublicInfo.switchRoleForm = this;
                PublicInfo.CurrentRoleID = lv.FocusedItem.Name;
                PublicInfo.CurrentRoleName = lv.FocusedItem.Text;
                Frm_Main mainForm = new Frm_Main(lst);
                this.Hide();
                PublicInfo.mainForm = mainForm;
                mainForm.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Frm_SwitchRole_FormClosed(object sender, FormClosedEventArgs e)
        {
            PublicInfo.loginForm.Show();
        }
    }
}
