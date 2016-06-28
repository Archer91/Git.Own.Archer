using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Lib.vo.sys
{
    [DataContract]
    public class SysRoleMenuVO
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int RoleID { get; set; }
        [DataMember]
        public int MenuID { get; set; }
    }
}
