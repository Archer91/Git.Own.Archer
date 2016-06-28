using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Client.util
{
    public class ListItemUtil
    {
        public string Name { get; private set; }
        public string Value { get; private set; }

        public ListItemUtil() { }
        public ListItemUtil(string pName, string pValue)
        {
            Name = pName;
            Value = pValue;
        }

        public override string ToString()
        {
            return string.Format(@"{0}-{1}", Value, Name);
        }
    }
}
