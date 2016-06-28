using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Lib.impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMS.Lib.vo.sys;


namespace WMS.Lib.impl.Tests
{
    [TestClass()]
    public class SysInfoImplTests
    {

        [TestMethod()]
        public void getRoleByAccountTest()
        {
            SysInfoImpl sii = new SysInfoImpl();
            sii.getRoleByAccount("10001");
        }

        [TestMethod()]
        public void getRoleListTest()
        {
            SysInfoImpl sii = new SysInfoImpl();
            sii.getRoleList();
        }

    }
}
