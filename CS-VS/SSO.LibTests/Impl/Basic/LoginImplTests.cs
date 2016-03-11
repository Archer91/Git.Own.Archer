using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSO.Lib.Impl.Basic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSO.Lib.VO.Basic;


namespace SSO.Lib.Impl.Basic.Tests
{
    [TestClass()]
    public class LoginImplTests
    {
        [TestMethod()]
        public void checkLoginTest()
        {
            LoginImpl loginImpl = new LoginImpl();
            AccountVO accountVO = loginImpl.checkLogin("admin", "admin");
            Assert.AreEqual("admin", accountVO.Code);
        }

        [TestMethod]
        public void changePasswordTest()
        {
            LoginImpl loginImpl = new LoginImpl();
            bool result = loginImpl.changePassword("admin", "123", "admin");
            Assert.IsTrue(result);
        }
    }
}
