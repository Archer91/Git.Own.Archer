using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace WMS.Lib.vo.sys
{
    [DataContract]
    public class SysAccountVO
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string AccountName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public int? StructureID { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string EnglishName { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Tel { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public int? Age { get; set; }
        [DataMember]
        public string Sex { get; set; }
        [DataMember]
        public string Education { get; set; }
        [DataMember]
        public string Marriage { get; set; }
        [DataMember]
        public DateTime? HireDate { get; set; }
        [DataMember]
        public string EmergencyContacts { get; set; }
        [DataMember]
        public string EmergencyPhone { get; set; }
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
        public string StructureDesc { get; set; }
    }
}
