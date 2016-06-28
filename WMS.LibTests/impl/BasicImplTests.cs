using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Lib.impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMS.Lib.util;

namespace WMS.Lib.impl.Tests
{
    [TestClass()]
    public class BasicImplTests
    {
        [TestMethod()]
        public void getSNTest()
        {
            BasicImpl bi = new BasicImpl();
            string tmp = bi.getSN(SerialTypeEnum.ReceivingNO.ToString(), 1000);
        }
    }
}
