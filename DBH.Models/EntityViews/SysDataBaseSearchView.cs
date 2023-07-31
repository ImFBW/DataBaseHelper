using DBH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Models.EntityViews
{
    /// <summary>
    /// 搜索的数据库系统表返回的结果集实体类
    /// </summary>
    public class SysDataBaseSearchView
    {
        /// <summary>
        /// 对应类型的名称，比如表名、存储过程名、表值函数名
        /// </summary>
        public  string TypeName { get; set; }

        /// <summary>
        /// 搜索的类型，比如有U=用户表，P=存储过程，TF=表值函数
        /// </summary>
        public DBObjectType DBObjectType { get; set; }

        /// <summary>
        /// 定义的内容，比如存储过程的内容，比如表值函数的内容
        /// 按行查出，一行是一条记录，所以会是List
        /// </summary>
        public List<Definition> Definition { get; set; }
    }

    /// <summary>
    /// 存储过程或表值函数的定义内容
    /// </summary>
    public class Definition
    {
        public string Content { get; set; }
    }

    
}
