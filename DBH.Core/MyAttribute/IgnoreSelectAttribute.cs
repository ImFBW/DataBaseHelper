using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Core.MyAttribute
{
    /// <summary>
    /// 忽略查询时的字段选择
    /// 用于类的字段（对应表字段）可以在Select  Sql拼接时，忽略该字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreSelectAttribute : Attribute
    {
    }
}
