using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WMS.Client
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool isNew;
            Mutex m = new Mutex(false, "Archer.WMS", out isNew);
            if (isNew)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Fm_Login());
            }
            else
            {
                MessageBox.Show("WMS系统已在运行！", "WMS-Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
