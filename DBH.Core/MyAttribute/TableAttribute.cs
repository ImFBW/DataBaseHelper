using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Core.MyAttribute
{
    /// <summary>
    /// 标记类的Table信息，
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// 对应数据库的表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 对应数据库的主键的名称
        /// </summary>
        public string KeyName { get; set; }

        /// <summary>
        /// 表示是否自增
        /// </summary>
        public bool IsIdentity { get; set; }
    }
}
