using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Models.EntityViews
{
    /// <summary>
    /// 定义：表的列名、列的数据类型、列的说明
    /// </summary>
    public class ColumnTypeDescView
    {
        /// <summary>
        /// 列的名称
        /// </summary>
        public string ColumnName { get;set; }

        /// <summary>
        /// 列的解释说明
        /// </summary>
        public string  ColumnDesc { get; set; }
        /// <summary>
        /// 列的数据类型int/char/varachar/nvarchar/decimal/bit......
        /// </summary>
        public string ColumnDataType { get; set; }
    }
}
