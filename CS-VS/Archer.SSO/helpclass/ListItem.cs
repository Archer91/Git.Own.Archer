using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archer.SSO.helpclass
{
    public class ListItem
    {
        public string Name { get; private set; }
        public string Value { get; private set; }

        public ListItem() { }
        public ListItem(string pName, string pValue)
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
