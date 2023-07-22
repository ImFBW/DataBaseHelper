using DBH.Models.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Models.EntityViews
{
    /// <summary>
    /// 用于FS_ServicesEntity类的一些视图类
    /// </summary>
    public class FS_ServicesView : FS_ServicesEntity
    {
        /// <summary>
        /// 综合计算得出的最终服务器地址
        /// </summary>
        public string ServiceAddressFormat
        {
            get
            {
                string reValue = this.ServerAddress;
                if (this.ServerPortNo > 0)
                    reValue += ":" + this.ServerPortNo;
                if (!string.IsNullOrEmpty(this.ServerAddress2))
                {
                    reValue += "(" + this.ServerAddress2 + ")";
                }
                return reValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SourceName { get; set; }

    }
}
