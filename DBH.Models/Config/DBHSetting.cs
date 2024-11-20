using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Models.Config
{
    /// <summary>
    /// 自定义的配置对象
    /// </summary>
    public class DBHSetting
    {
        /// <summary>
        /// 导出文件的相对路径
        /// </summary>
        public string ExportFilePath { get; set; }

        /// <summary>
        /// 访问的地址
        /// </summary>
        public string ExportFileHttpURL { get; set; }
    }
}
