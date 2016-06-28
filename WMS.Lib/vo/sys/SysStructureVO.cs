using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace WMS.Lib.vo.sys
{
    [DataContract]
    public class SysStructureVO
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int? ParentID { get; set; }
        [DataMember]
        public int? CompanyID { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string StructureName { get; set; }
        [DataMember]
        public string Description { get; set; }
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
    }
}
