using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data;
using System.Collections;

namespace Archer.SSO.helpclass
{
    public class ReadSystemSet
    {
        static XElement root = XElement.Load(@"config\SystemSet.xml");

        /// <summary>
        /// 获取系统标题
        /// </summary>
        /// <returns></returns>
        public static string getSystemTitle()
        {
            XElement xl = root.Element("SysTitle");
            return xl.Value;
        }

        /// <summary>
        /// 获取厂别
        /// </summary>
        /// <returns></returns>
        public static List<ListItem> getFactories()
        {
            List<ListItem> lst = new List<ListItem>();
            var items = root.Elements("Factories").Elements("Factory");
            foreach (var item in items)
            {
                ListItem li = new ListItem(item.Element("Name").Value, item.Element("Code").Value);
                lst.Add(li);
            }
            return lst;
        }
    }
}
