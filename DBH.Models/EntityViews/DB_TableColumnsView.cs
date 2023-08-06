using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Models.EntityViews
{
    /// <summary>
    /// 查询的Table 字段列表数据实体【自定义】
    /// </summary>
    public class DB_TableColumnsView
    {
        /// <summary>
        /// 行号，序号
        /// </summary>
        public int RowNumber { get; set; }

        /// <summary>
        /// 所属表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 表的说明
        /// </summary>
        public string TableDesc { get; set; }

        /// <summary>
        /// 字段列名
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 字段说明
        /// </summary>
        public string ColumnDesc { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string ColumnDataType { get; set; }
        /// <summary>
        /// 数据长度
        /// </summary>
        public int ColumnDataLength { get; set; }
        /// <summary>
        /// 是否自增长，Identity
        /// 1=是，0=否
        /// </summary>
        public int IsIdentity { get; set; }
        /// <summary>
        /// 是否主键，1=是，0=否
        /// </summary>
        public int IsKey { get; set; }
        /// <summary>
        /// 是否可空，1=是，0=否
        /// </summary>
        public int IsNullable { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }
    }
}
