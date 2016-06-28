using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Lib.impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMS.Lib.vo.accept;

namespace WMS.Lib.impl.Tests
{
    [TestClass()]
    public class AcceptanceImplTests
    {
        [TestMethod()]
        public void sendMailWithReceivingTest()
        {
            AcceptanceImpl ai = new AcceptanceImpl();
            AcceptReceivingVO arv = new AcceptReceivingVO();
            arv.No = "SR16060000002";
            arv.Names = "东北苹果";
            arv.TrackingNumber = "SF23456789";
            arv.ShipAddress = "吉林松花江";
            arv.Shipper = "laowang";
            arv.StorageLocation = "A1001";

            ai.sendMailWithReceiving(arv);
        }
    }
}
