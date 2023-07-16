using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Models.Common
{
    /// <summary>
    /// 定义本项目使用的各种状态码
    /// </summary>
    public enum StatusCode
    {
        [Description("成功")]
        Success = 1000,

        [Description("失败")]
        Fail = 1001,

        [Description("警告")]
        Warning = 1002,

        [Description("错误")]
        Error = 1009,

    }
}
