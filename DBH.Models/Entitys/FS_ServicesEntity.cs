using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Models.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    public class FS_ServicesEntity : BaseEntity
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public new int ID { get; set; } = 0;

        /// <summary>
        /// 服务器地址，如123.123.123.123
        /// </summary>
        public string ServerAddress { get; set; } = "";

        /// <summary>
        /// 指向同一个服务器的第二个备用地址
        /// </summary>
        public string ServerAddress2 { get; set; } = "";

        /// <summary>
        /// 端口号
        /// </summary>
        public int ServerPortNo { get; set; } = 0;

        /// <summary>
        /// 登陆用户名
        /// </summary>
        public string LoginName { get; set; } = "";

        /// <summary>
        /// 登陆密码
        /// </summary>
        public string LoginPassword { get; set; } = "";

        /// <summary>
        /// 数据库类型，1SqlServer，2MySql
        /// </summary>
        public int ServerType { get; set; } = 0;

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DataBaseName { get; set; } = "";

        /// <summary>
        /// 数据库简介
        /// </summary>
        public string DataBaseIntro { get; set; } = "";

        /// <summary>
        /// 来源,1电信通,2阿里云,3腾讯云,4华为云...
        /// </summary>
        public int TypeSource { get; set; } = 0;

        /// <summary>
        /// 是否使用中，1是，0否
        /// </summary>
        public int IsInUse { get; set; } = 0;

        ///// <summary>
        ///// 创建时间
        ///// </summary>
        //public new DateTime? Createtime { get; set; } = null;

    }
}
