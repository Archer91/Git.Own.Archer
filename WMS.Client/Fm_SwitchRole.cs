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
using WMS.Client.util;

namespace WMS.Client
{
    public partial class Fm_SwitchRole : Form
    {
        public Fm_SwitchRole()
        {
            InitializeComponent();
        }

        SysInfoClient sic = null;
        private void Fm_SwitchRole_Load(object sender, EventArgs e)
        {
            try
            {
                sic = new SysInfoClient();
                List<SysRoleEasyVO> lst = sic.getRoleByAccount(PubInfoUtil.accountEasyVO.Code);
                foreach (var item in lst)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Name = item.ID.ToString();
                    lvi.Text = item.RoleName;
                    lvi.ImageIndex = 0;
                    lstRoles.Items.Add(lvi);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Fm_SwitchRole_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ListView lv = sender as ListView;
                PubInfoUtil.switchRoleForm = this;
                PubInfoUtil.CurrentRoleId = Int32.Parse(lv.FocusedItem.Name);
                PubInfoUtil.CurrentRoleName = lv.FocusedItem.Text;
                Fm_Main mainForm = new Fm_Main();
                this.Hide();
                PubInfoUtil.mainForm = mainForm;
                mainForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Fm_SwitchRole_FormClosed(object sender, FormClosedEventArgs e)
        {
            PubInfoUtil.loginForm.Show();
        }

        private void Fm_SwitchRole_Shown(object sender, EventArgs e)
        {
            if (lstRoles.Items.Count == 1)
            {
                PubInfoUtil.switchRoleForm = this;
                PubInfoUtil.CurrentRoleId = Int32.Parse(lstRoles.Items[0].Name);
                PubInfoUtil.CurrentRoleName = lstRoles.Items[0].Text;
                Fm_Main mainForm = new Fm_Main();
                this.Hide();
                PubInfoUtil.mainForm = mainForm;
                mainForm.Show(); 
            }
        } 
    }
}
