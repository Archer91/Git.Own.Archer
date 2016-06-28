using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WMS.Lib.vo.sys
{
    [DataContract]
    public class SysRoleAccountVO
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int RoleID { get; set; }
        [DataMember]
        public int AccountID { get; set; }
    }
}
