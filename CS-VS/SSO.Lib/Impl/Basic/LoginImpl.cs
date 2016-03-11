using SSO.Lib.Intf.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSO.Lib.VO.Basic;

namespace SSO.Lib.Impl.Basic
{
    public class LoginImpl:ILogin
    {
        CS_SystemDataContext DBContext = new CS_SystemDataContext();

        public AccountVO checkLogin(string pAccount, string pPwd)
        {
            if (string.IsNullOrEmpty(pAccount) || string.IsNullOrEmpty(pPwd))
            {
                throw new ArgumentNullException("E-0001", "参数为空");
            }

            var result = (from m in DBContext.TB_Account
                          where m.Code.ToUpper().Equals(pAccount.ToUpper())
                          select m).FirstOrDefault();
            if (null == result)
            {
                throw new Exception("用户不存在");
            }

            if (!result.Password.Equals(pPwd))
            {
                throw new Exception("密码错误");
            }

            if (result.Status.Equals("0"))
            {
                throw new Exception("用户已失效");
            }

            AccountVO accountVO = new AccountVO();
            accountVO.ID = result.ID;
            accountVO.Code = result.Code;
            accountVO.Name = result.Name;
            accountVO.EngName = result.EngName;
            //accountVO.Password = result.Password;
            accountVO.Status = result.Status;
            accountVO.DeptID = result.DeptID;
            accountVO.DeptCode = result.DeptCode;

            return accountVO;
        }

        public bool changePassword(string pAccount, string pOldPwd, string pNewPwd = "123456")
        {
            if(string.IsNullOrEmpty(pAccount) ||
                string.IsNullOrEmpty(pOldPwd))
            {
                throw new ArgumentNullException("E-0001", "参数为空");
            }
            
            var result = (from m in DBContext.TB_Account
                          where m.Code.ToUpper().Equals(pAccount.ToUpper())
                          select m).FirstOrDefault();
            if(null == result)
            {
                throw new Exception(string.Format(@"不存在编号为【{0}】的用户",pAccount));
            }
            if (!result.Password.Equals(pOldPwd))
            {
                throw new Exception("旧密码不正确");
            }
            result.Password = pNewPwd;
            result.UpdateBy = pAccount;
            result.UpdateOn = DateTime.Now;
            
            DBContext.SubmitChanges();
            return true;
        }

        public bool resetPassword(string pAccount, string pNewPwd = "123456")
        {
            throw new NotImplementedException();
        }

    }
}
