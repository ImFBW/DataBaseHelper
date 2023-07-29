using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Models.Common
{
    /// <summary>
    /// 返回数据的通用对象
    /// </summary>
    public class ResultModel
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public ResultCode Code { get; set; }
        
        private string _message = "";
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message
        {
            get
            {
                if (string.IsNullOrEmpty(_message))
                    return "";
                else
                    return _message;
            }
            set
            {
                _message = value;
            }
        }

        /// <summary>
        /// 结果状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 返回内容，一个结果
        /// </summary>
        public object Result { get; set; }
    }
}
