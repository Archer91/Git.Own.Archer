using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Lib.VO.Basic
{
    public class AccountVO
    {
        public Guid ID { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string EngName { get; set; }
        public Guid? DeptID { get; set; }
        public string DeptCode { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Status { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateOn { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateOn { get; set; }

    }
}
