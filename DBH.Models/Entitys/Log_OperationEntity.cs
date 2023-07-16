using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Models.Entitys
{
    /// <summary>
    /// 操作日志表
    /// </summary>
    public class Log_OperationEntity: BaseEntity
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public new int ID { get; set; } = 0;

        /// <summary>
        /// 操作者的用户ID
        /// </summary>
        public int UserID { get; set; } = 0;

        /// <summary>
        /// 操作的服务器数据库ID
        /// </summary>
        public int ServiceID { get; set; } = 0;

        /// <summary>
        /// 操作的内容说明
        /// </summary>
        public string OperaContent { get; set; } = "";

        /// <summary>
        /// 创建时间
        /// </summary>
        public new DateTime? Createtime { get; set; } = null;
    }
}
