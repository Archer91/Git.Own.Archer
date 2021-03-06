﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Lib.VO.Basic
{
    public class UdcVO
    {
        public Guid ID { get; set; }
        public string SysCode { get; set; }
        public string Category { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateOn { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateOn { get; set; }
    }
}
