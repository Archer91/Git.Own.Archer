using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace WMS.Lib.vo.sys
{
    [DataContract]
    public class SysRoleEasyVO
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string RoleName { get; set; }
    }
}
