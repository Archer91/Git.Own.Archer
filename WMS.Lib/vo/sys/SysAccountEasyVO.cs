using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Lib.vo.sys
{
    [DataContract]
    public class SysAccountEasyVO
    {
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string AccountName { get; set; }

    }
}
