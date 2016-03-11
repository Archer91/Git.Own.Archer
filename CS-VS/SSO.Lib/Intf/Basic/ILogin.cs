using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSO.Lib.VO.Basic;

namespace SSO.Lib.Intf.Basic
{
    interface ILogin
    {
        AccountVO checkLogin(string pAccount, string pPwd);
        bool changePassword(string pAccount, string pOldPwd, string pNewPwd="123456");
        bool resetPassword(string pAccount, string pNewPwd = "123456");

    }
}
