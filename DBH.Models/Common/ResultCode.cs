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
    public enum ResultCode
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

    /// <summary>
    /// 定义操作实体类的一些结果状态码
    /// </summary>
    public enum EntityCode
    {
        [Description("成功")]
        Success = 1000,

        [Description("失败")]
        Fail = 1001,

        [Description("已存在")]
        Exists = 1002,

        [Description("不存在")]
        NotExists = 1003,

        [Description("不唯一")]
        NotOnly = 1004,

        [Description("唯一")]
        OnlyOne = 1005,

        [Description("系统异常")]
        SysError = 1007,

        [Description("参数缺失")]
        PramIsNull = 1008,

        [Description("未知")]
        Unknown = 1009,
    }
}
