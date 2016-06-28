using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Lib.vo.accept
{
    [DataContract]
    public class AcceptReceivingVO
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int CompanyID { get; set; }
        [DataMember]
        public string No { get; set; }
        [DataMember]
        public string Names { get; set; }
        [DataMember]
        public string TrackingNumber { get; set; }
        [DataMember]
        public string ShipAddress { get; set; }
        [DataMember]
        public string Shipper { get; set; }
        [DataMember]
        public string Unit { get; set; }
        [DataMember]
        public int? Pieces { get; set; }
        [DataMember]
        public decimal? Weight { get; set; }
        [DataMember]
        public string DeliveryCompany { get; set; }
        [DataMember]
        public string DeliveryMan { get; set; }
        [DataMember]
        public string DeliveryMode { get; set; }
        [DataMember]
        public DateTime? ReceivingDate { get; set; }
        [DataMember]
        public string ReceivingPerson { get; set; }
        [DataMember]
        public string StorageLocation { get; set; }
        [DataMember]
        public int? IsNotice { get; set; }
        [DataMember]
        public string Remark { get; set; }
        [DataMember]
        public int? Status { get; set; }
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
        public string NoticeDesc { get; set; }
    }
}
