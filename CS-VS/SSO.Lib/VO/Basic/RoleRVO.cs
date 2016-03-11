using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Lib.VO.Basic
{
    public class RoleRVO
    {
        public Guid ID { get; set; }
        public Guid RoleID { get; set; } // 不只是角色ID，还有可能是用户ID，部门ID
        public Guid RID { get; set; } //有可能是角色ID，也有可能是菜单ID
        public string RName { get; set; }
        public string Status { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateOn { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateOn { get; set; }
    }
}
