using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Models.Entitys
{
    /// <summary>
    /// 服务器配置，服务器的来源，例如：阿里云、腾讯云、本地、公司内网、电信通.......
    /// </summary>
    public class FS_ServiceSourceEntity
    {

        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public string SourceName { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Createtime { get; set; } = null;

    }
}
