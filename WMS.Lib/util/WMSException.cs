using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extension.Util.Archer;

namespace WMS.Lib.util
{
    [Serializable]
    public class WMSException:ApplicationException
    {
        ErrorInfo errorInfo;
        string message;
        public override string Message
        {
            get
            {
                return message;
            }
        }

         /// <summary>
        /// WMS自定义异常
        /// </summary>
        /// <param name="errCode">异常Code</param>
        /// <param name="errParams">异常参数值</param>
        public WMSException(string errCode, params object[] errParams)
            : base()
        {
            //根据异常代码获取Message
            message = errCode + "-" + getMsg(errCode, errParams);
        }

        private string getMsg(string errCode, params object[] errParams)
        {
            if (errorInfo.IsNullOrEmpty())
            {
                errorInfo = new ErrorInfo();
            }

            return string.Format(errorInfo.Properties[errCode].IsNullOrEmpty() ? "未定义的异常" : errorInfo.Properties[errCode].DefaultValue.ToString(), errParams);
        }
    }
}
