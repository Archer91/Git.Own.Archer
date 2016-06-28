using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Lib.vo.basic
{
    [DataContract]
    public class SerialNumberDetailVO
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int CompanyID { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string Prefix { get; set; }
        [DataMember]
        public string YYMMDD { get; set; }
        [DataMember]
        public int? SeqLength { get; set; }
        [DataMember]
        public int? SeqStep { get; set; }
        [DataMember]
        public string Suffix { get; set; }
        [DataMember]
        public int? CurVal { get; set; }
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
        [DataMember]
        public string CompanyDesc { get; set; }
    }
}
