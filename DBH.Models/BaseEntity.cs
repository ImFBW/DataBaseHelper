using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Models
{
    /// <summary>
    /// 定义所有类的基类
    /// 所有数据库表的映射类，都应该继承此类
    /// </summary>
    public class BaseEntity
    {
        //规范：所有实体类（数据表）都应该有ID
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }

        //规范：所有实体类（数据表）都应该有创建时间
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
