using DBH.Models.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Models.EntityViews
{
    /// <summary>
    /// 提交表单接收参数
    /// </summary>
    public class RequestFSService
    {
        /// <summary>
        /// 数据主键，通常情况：>0表示修改，=0表示新增
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 数据库类型，1=SqlServer，2=MySql
        /// </summary>
        public int DBCategory {  get; set; }
        
        /// <summary>
        /// 数据库地址
        /// </summary>
        public string DBAddress { get; set; }

        /// <summary>
        /// 数据库地址的端口号
        /// </summary>
        public int DBPort { get; set; }

        /// <summary>
        /// 数据库来源
        /// </summary>
        public int DBSource { get; set; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DBName { get; set; }

        /// <summary>
        /// 数据库登录名
        /// </summary>
        public string DBLoginName { get; set; }

        /// <summary>
        /// 数据库登录密码
        /// </summary>
        public string DBLoginPassword { get; set; }

        /// <summary>
        /// 数据库简介
        /// </summary>
        public string DBIntro { get; set; }

        /// <summary>
        /// 转换对应的Fs_ServiceEntity类
        /// </summary>
        public FS_ServicesEntity ToFSServiceEntity
        {
            get
            {
                FS_ServicesEntity fs = new FS_ServicesEntity()
                {
                    ID = this.ID,
                    ServerType = this.DBCategory,
                    ServerAddress = this.DBAddress,
                    ServerPortNo = this.DBPort,
                    LoginName = this.DBLoginName,
                    LoginPassword = this.DBLoginPassword,
                    DataBaseName = this.DBName,
                    DataBaseIntro = this.DBIntro,
                    SourceID = this.DBSource,
                    CreateTime = DateTime.Now,
                };
                return fs;
            }
        }

    }
}
