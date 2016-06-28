using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Lib.vo.basic
{
    [DataContract]
    public class MaterialVO
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int? CategoryID { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int? SupplierID { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public int? Status { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public DateTime? CreateTime { get; set; }
        [DataMember]
        public string UpdatedBy{get;set;}
        [DataMember]
        public DateTime? UpdateTime{get;set;}
        [DataMember]
        public string StatusDesc{get;set;}
        [DataMember]
        public string CategoryDesc { get; set; }
        [DataMember]
        public string SupplierDesc { get; set; }
    }
}
