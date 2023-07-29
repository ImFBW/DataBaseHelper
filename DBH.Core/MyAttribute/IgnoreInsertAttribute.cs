using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Core.MyAttribute
{
    /// <summary>
    /// 忽略Insert时选择
    /// 拼接Insert Sql时忽略该字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreInsertAttribute : Attribute
    {
    }
}
