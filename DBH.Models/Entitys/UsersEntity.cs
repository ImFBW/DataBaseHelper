using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Models.Entitys
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class UsersEntity
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public new int ID { get; set; } = 0;

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; } = "";

        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; } = "";

        /// <summary>
        /// 登录密码
        /// </summary>
        public string LoginPwd { get; set; } = "";

        /// <summary>
        /// 是否管理员，1是
        /// </summary>
        public bool IsAdmin { get; set; } = false;

        /// <summary>
        /// 上次登陆时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; } = null;

        /// <summary>
        /// 是否有效，1是
        /// </summary>
        public bool IsValid { get; set; } = false;

        /// <summary>
        /// 是否删除，1已删除，0正常
        /// </summary>
        public new int IsDel { get; set; } = 0;

        /// <summary>
        /// 创建时间
        /// </summary>
        public new DateTime? Createtime { get; set; } = null;
    }
}
