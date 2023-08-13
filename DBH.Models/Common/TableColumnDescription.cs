using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Models.Common
{
    /// <summary>
    /// 更新表、列的字段说明
    /// </summary>
    public class TableColumnDescription
    {
        /// <summary>
        /// 1=表，2=列
        /// </summary>
        public int TypeID { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        public string TableColumn { get; set; }

        /// <summary>
        /// 说明内容
        /// </summary>
        public string Description { get; set; }
    }
}
