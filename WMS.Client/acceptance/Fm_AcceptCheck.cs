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

namespace WMS.Client.acceptance
{
    public partial class Fm_AcceptCheck : Form
    {
        public Fm_AcceptCheck()
        {
            InitializeComponent();
        }

        AcceptanceClient ac = null;

        private void Fm_AcceptCheck_Load(object sender, EventArgs e)
        {
            ac = new AcceptanceClient();


        }
    }
}
 