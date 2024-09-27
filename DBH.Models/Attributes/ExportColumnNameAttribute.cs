using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Models.Attributes
{
    public class ExportColumnNameAttribute : Attribute
    {
        /// <summary>
        /// 导出时，转换为Excel的列名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 字段排序，决定导出的结果中列的显示顺序,从小到大，展示为从左到右
        /// </summary>
        public int ColumnIndex { get; set; }

        public ExportColumnNameAttribute(string columnName, int columnIndex = 999)
        {
            ColumnName = columnName;
            ColumnIndex = columnIndex;
        }
    }
}
