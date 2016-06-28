using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WMS.Client.SysInfoSvr;


namespace WMS.Client.util
{
    public class PubInfoUtil
    {
        public static Fm_Login loginForm = null;
        public static Fm_SwitchRole switchRoleForm = null;
        public static Fm_Main mainForm = null;

        public static string HostName
        {
            get
            {
                return Dns.GetHostName();
            }
        }

        public static string HostIP
        {
            get
            {
                return Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
            }
        }

        public static int CurrentRoleId { get; set; }
        public static string CurrentRoleName { get; set; }
        public static int CurrentCompanyId { get; set; }
        public static string CurrentCompanyName { get; set; }


        public static SysAccountEasyVO accountEasyVO = null;
    }
}
