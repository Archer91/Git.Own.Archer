using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WMS.Client.util
{
    public class ConfigUtil
    {
        static XElement root = XElement.Load(@"config\sysconfig.xml");

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
        /// 获取系统皮肤
        /// </summary>
        /// <returns></returns>
        public static string getSystemSkins()
        {
            XElement xl = root.Element("SysSkin");
            return xl.Value;
        }

    }
}
