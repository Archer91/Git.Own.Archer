using SSO.Lib.Impl.Basic;
using SSO.Lib.VO.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Archer.SSO.helpclass
{
    public  class PublicInfo
    {
        public static Frm_Login loginForm = null;
        public static Frm_SwitchRole switchRoleForm = null;
        public static Frm_Main mainForm = null;

        public static LoginImpl loginImpl = new LoginImpl();
        public static BasicImpl basicImpl = new BasicImpl();

        public static AccountVO accountVO = null;

        public static string HostName
        {
            get { 
                return Dns.GetHostName(); 
            }
        }

        public static string HostIP
        {
            get { 
                return Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString(); 
            }
        }

        public static string CurrentRoleID
        {
            get;
            set;
        }

        public static string CurrentRoleName
        {
            get;
            set;
        }

    }
}
