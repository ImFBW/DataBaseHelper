using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Core.MyAttribute
{
    /// <summary>
    /// Update字段忽略选择
    /// 在拼接Update Sql时，忽略该字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreUpdateAttribute : Attribute
    {
    }
}
