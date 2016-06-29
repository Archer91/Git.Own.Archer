using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Lib.vo.accept
{
    [DataContract]
    public class AcceptCheckSplitVO
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int CheckID { get; set; }
        [DataMember]
        public string ReelID { get; set; }
        [DataMember]
        public int Qty { get; set; }
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
