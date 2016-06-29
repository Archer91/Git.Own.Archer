using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Lib.intf;
using WMS.Lib.util;
using WMS.Lib.vo.accept;
using Common.Util.Archer;
using System.IO;

namespace WMS.Lib.impl
{
    public class AcceptanceImpl:IAcceptance
    {
        DataWMSDataContext wmsContext = new DataWMSDataContext();
        BasicImpl bi = new BasicImpl();
        public List<AcceptReceivingVO> getAcceptReceivingList()
        {
            var result = from m in wmsContext.T_Accept_Receiving
                         select m;
            List<AcceptReceivingVO> lst = new List<AcceptReceivingVO>();
            foreach (var item in result)
            {
                AcceptReceivingVO arv = new AcceptReceivingVO();
                arv.ID = item.ID;
                arv.CompanyID = item.CompanyID;
                arv.No = item.No;
                arv.Names = item.Names;
                arv.TrackingNumber = item.TrackingNumber;
                arv.ShipAddress = item.ShipAddress;
                arv.Shipper = item.Shipper;
                arv.Unit = item.Unit;
                arv.Pieces = item.Pieces;
                arv.Weight = item.Weight;
                arv.DeliveryCompany = item.DeliveryCompany;
                arv.DeliveryMan = item.DeliveryMan;
                arv.DeliveryMode = item.DeliveryMode;
                arv.ReceivingDate = item.ReceivingDate;
                arv.ReceivingPerson = item.ReceivingPerson;
                arv.StorageLocation = item.StorageLocation;
                arv.IsNotice = item.IsNotice;
                arv.Remark = item.Remark;
                arv.Status = item.Status;
                arv.CreatedBy = item.CreatedBy;
                arv.CreateTime = item.CreateTime;
                arv.UpdatedBy = item.UpdatedBy;
                arv.UpdateTime = item.UpdateTime;
                arv.StatusDesc = item.Status == 1 ? "启用" : "失效";
                arv.NoticeDesc = item.IsNotice == 0 ? "未通知检验" : "已通知检验";

                lst.Add(arv);
            }
            return lst;
        }

        public bool saveAcceptReceiving(AcceptReceivingVO pReceivingVO)
        {
            if(null == pReceivingVO)
            {
                throw new WMSException("ER001");
            }
            if(string.IsNullOrEmpty(pReceivingVO.No.Trim()))
            {
                //新增
                //自动获取No
                pReceivingVO.No = bi.getSN(SerialTypeEnum.ReceivingNO.ToString(),pReceivingVO.CompanyID);

                var result = (from m in wmsContext.T_Accept_Receiving
                              where m.CompanyID == pReceivingVO.CompanyID
                              && m.No.ToUpper().Equals(pReceivingVO.No.ToUpper())
                              select m).SingleOrDefault();
                if (null != result)
                {
                    throw new WMSException("ER031", pReceivingVO.No);
                }
                T_Accept_Receiving tar = new T_Accept_Receiving();
                tar.CompanyID = pReceivingVO.CompanyID;
                tar.No = pReceivingVO.No;
                tar.Names = pReceivingVO.Names;
                tar.TrackingNumber = pReceivingVO.TrackingNumber;
                tar.ShipAddress = pReceivingVO.ShipAddress;
                tar.Shipper = pReceivingVO.Shipper;
                tar.Unit = pReceivingVO.Unit;
                tar.Pieces = pReceivingVO.Pieces;
                tar.Weight = pReceivingVO.Weight;
                tar.DeliveryCompany = pReceivingVO.DeliveryCompany;
                tar.DeliveryMan = pReceivingVO.DeliveryMan;
                tar.DeliveryMode = pReceivingVO.DeliveryMode;
                if (pReceivingVO.ReceivingDate != null)
                {
                    tar.ReceivingDate = pReceivingVO.ReceivingDate;
                }
                tar.ReceivingPerson = pReceivingVO.ReceivingPerson;
                tar.StorageLocation = pReceivingVO.StorageLocation;
                tar.IsNotice = pReceivingVO.IsNotice;
                tar.Remark = pReceivingVO.Remark;
                tar.Status = 1;
                tar.CreatedBy = pReceivingVO.CreatedBy;
                tar.CreateTime = DateTime.Now;

                wmsContext.T_Accept_Receiving.InsertOnSubmit(tar);
                wmsContext.SubmitChanges();
            }
            else
            {
                //编辑
                var result = (from m in wmsContext.T_Accept_Receiving
                              where m.CompanyID == pReceivingVO.CompanyID
                                  && m.No.ToUpper().Equals(pReceivingVO.No.ToUpper())
                              select m).SingleOrDefault();
                if (null == result)
                {
                    throw new WMSException("ER032", pReceivingVO.No);
                }

                result.CompanyID = pReceivingVO.CompanyID;
                result.Names = pReceivingVO.Names;
                result.TrackingNumber = pReceivingVO.TrackingNumber;
                result.ShipAddress = pReceivingVO.ShipAddress;
                result.Shipper = pReceivingVO.Shipper;
                result.Unit = pReceivingVO.Unit;
                result.Pieces = pReceivingVO.Pieces;
                result.Weight = pReceivingVO.Weight;
                result.DeliveryCompany = pReceivingVO.DeliveryCompany;
                result.DeliveryMan = pReceivingVO.DeliveryMan;
                result.DeliveryMode = pReceivingVO.DeliveryMode;
                if (pReceivingVO.ReceivingDate != null)
                {
                    result.ReceivingDate = pReceivingVO.ReceivingDate;
                }
                result.ReceivingPerson = pReceivingVO.ReceivingPerson;
                result.StorageLocation = pReceivingVO.StorageLocation;
                result.IsNotice = pReceivingVO.IsNotice;
                result.Remark = pReceivingVO.Remark;
                result.UpdatedBy = pReceivingVO.UpdatedBy;
                result.UpdateTime = DateTime.Now;

                wmsContext.SubmitChanges();
            }

            try
            {
                //若勾选了通知检验，系统将自动推送邮件
                if (pReceivingVO.IsNotice == 1)
                {
                    sendMailWithReceiving(pReceivingVO);
                }
            }
            catch (Exception ex) { throw ex; }

            return true;
        }

        public bool enableAndDisableReceivingByReceiveID(int pReceiveID, int pStatus, string pAccount)
        {
            var result = (from m in wmsContext.T_Accept_Receiving
                          where m.ID == pReceiveID
                          select m).SingleOrDefault();
            if (null == result)
            {
                throw new WMSException("ER032", pReceiveID);
            }
            result.Status = pStatus;
            result.UpdatedBy = pAccount;
            result.UpdateTime = DateTime.Now;

            wmsContext.SubmitChanges();
            return true;
        }

        private void sendMailWithReceiving(AcceptReceivingVO arv)
        {
            string mailFrom = "complaint@moderndentallab.com";
            string mailTo = "xh.li@moderndentallab.com";
            string mailSubject = string.Format(@"仓库到货接收,编号{0},请尽快验收", arv.No);
            string mailBody = string.Empty;
            string mailAttch = string.Empty;
            string mailCode = string.Empty;
            string mailPriority = string.Empty;
            string mailCC = string.Empty;
            string resultMessage = string.Empty;

            string templetpath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"mailtemplate\receiving.txt");//模版路径
            NameValueCollection myCol = new NameValueCollection();
            myCol.Add("no", arv.No);
            myCol.Add("trackingnumber", arv.TrackingNumber);
            myCol.Add("names", arv.Names);
            myCol.Add("shipaddress", arv.ShipAddress);
            myCol.Add("shipper", arv.Shipper);
            myCol.Add("location", arv.StorageLocation);
            mailBody = MailUtil.BulidByFile(templetpath, myCol);

            MailUtil.SendNetMail(mailFrom, mailTo, mailSubject, mailBody, mailAttch, mailPriority, mailCC, out resultMessage);
        }

        public bool saveAcceptCheck(AcceptCheckVO pCheckVO)
        {
            if(null == pCheckVO)
            {
                throw new WMSException("ER001");
            }
            if(string.IsNullOrEmpty(pCheckVO.ReceiveNo) ||
                string.IsNullOrEmpty(pCheckVO.ItemCode))
            {
                throw new WMSException("ER001");
            }

            T_Accept_Check tac = new T_Accept_Check();
            tac.ReceiveNo = pCheckVO.ReceiveNo;
            tac.ItemCode = pCheckVO.ItemCode;
            tac.ItemName = pCheckVO.ItemName;
            tac.CustomerPartNo = pCheckVO.CustomerPartNo;
            tac.Qty = pCheckVO.Qty;
            tac.DateCode = pCheckVO.DataCode;
            tac.LotNo = pCheckVO.LotNo;
            tac.SupplierName = pCheckVO.SupplierName;
            tac.Description = pCheckVO.Description;
            tac.PurchaseOrder = pCheckVO.PurchaseOrder;
            tac.ConfirmQty = pCheckVO.ConfirmQty;
            tac.Remark = pCheckVO.Remark;
            tac.Operator = pCheckVO.Operator;
            tac.OperationTime = DateTime.Now;
            tac.Status = 1;
            tac.CreatedBy = pCheckVO.CreatedBy;
            tac.CreateTime = DateTime.Now;

            wmsContext.T_Accept_Check.InsertOnSubmit(tac);
            wmsContext.SubmitChanges();

            return true;
        }

        public bool saveAcceptCheckSplit(List<AcceptCheckSplitVO> pListSplitVO)
        {
            if(null == pListSplitVO || pListSplitVO.Count <= 0)
            {
                throw new WMSException("ER001");
            }
            List<T_Accept_CheckSplit> lst = new List<T_Accept_CheckSplit>();
            foreach (var item in pListSplitVO)
            {
                T_Accept_CheckSplit tacs = new T_Accept_CheckSplit();
                tacs.CheckID = item.CheckID;
                tacs.ReelID = item.ReelID;
                tacs.Qty = item.Qty;
                tacs.Description = item.Description;
                tacs.Status = 1;
                tacs.CreatedBy = item.CreatedBy;
                tacs.CreateTime = DateTime.Now;

                lst.Add(tacs);
            }

            wmsContext.T_Accept_CheckSplit.InsertAllOnSubmit(lst);
            wmsContext.SubmitChanges();
            return true;
        }

        public List<AcceptCheckVO> getAcceptCheckByReceiveNo(string pReceiveNo)
        {
            if(string.IsNullOrEmpty(pReceiveNo))
            {
                throw new WMSException("ER001");
            }

            var result = from m in wmsContext.T_Accept_Check
                         where m.ReceiveNo.ToUpper().Equals(pReceiveNo.Trim().ToUpper())
                         select m;
            List<AcceptCheckVO> lst = new List<AcceptCheckVO>();
            foreach (var item in result)
            {
                AcceptCheckVO acv = new AcceptCheckVO();
                //TODO

                var details = from n in wmsContext.T_Accept_CheckSplit
                              where n.CheckID == item.ID
                              select n;
                List<AcceptCheckSplitVO> lst2 = new List<AcceptCheckSplitVO>();
                foreach (var item2 in details)
                {
                    AcceptCheckSplitVO acsv = new AcceptCheckSplitVO();
                    //TODO

                    lst2.Add(acsv);
                }
                acv.SplitDetails = lst2;

                lst.Add(acv);
            }

            return lst;
        }
    }
}
