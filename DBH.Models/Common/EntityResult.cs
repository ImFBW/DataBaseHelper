using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Models.Common
{
    /// <summary>
    /// 操作数据的返回结果类
    /// </summary>
    public class EntityResult<T> where T:BaseEntity , new()
    {
        /// <summary>
        /// 用于保存主键，比如新增后返回主键ID，查询一个ID是否存在等
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 用于保存数据实体，比如新增后返回实体类
        /// </summary>
        public T GetEntity { get; set; }

        /// <summary>
        /// 一些状态码
        /// </summary>
        public EntityCode EntityCode { get; set; }

        /// <summary>
        /// 消息提示，错误或异常消息等
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// 操作数据的返回结果类
    /// </summary>
    public class EntityResult
    {
        /// <summary>
        /// 用于保存主键，比如新增后返回主键ID，查询一个ID是否存在等
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 一些状态码
        /// </summary>
        public EntityCode EntityCode { get; set; }

        /// <summary>
        /// 消息提示，错误或异常消息等
        /// </summary>
        public string Message { get; set; }
    }

}
