using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Lib.vo.accept
{
    [DataContract]
    public class AcceptCheckVO
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string ReceiveNo { get; set; }
        [DataMember]
        public string ItemCode { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string CustomerPartNo { get; set; }
        [DataMember]
        public int? Qty { get; set; }
        [DataMember]
        public DateTime? DataCode { get; set; }
        [DataMember]
        public string LotNo { get; set; }
        [DataMember]
        public string SupplierName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string PurchaseOrder { get; set; }
        [DataMember]
        public int? ConfirmQty { get; set;}
        [DataMember]
        public string Remark { get; set; }
        [DataMember]
        public string Operator { get; set; }
        [DataMember]
        public DateTime? OperationTime { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public DateTime? CreateTime { get; set; }
        [DataMember]
        public string UpdatedBy { get; set; }
        [DataMember]
        public DateTime? UpdateTime { get; set; }
        [DataMember]
        public string StatusDesc { get; set; }
        [DataMember]
        public List<AcceptCheckSplitVO> SplitDetails { get; set; }

    }
}
