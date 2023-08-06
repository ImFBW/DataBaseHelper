using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Models.Common
{
    /// <summary>
    /// 定义查出的返回结果类型，目前仅支持表[U]、存储过程[P]、表值函数[TF]
    /// 即系统表：sys.sysobjects的[xtype]字段
    /// </summary>
    public enum DBObjectType
    {
        [Description("用户表")]
        U = 1,
        /// <summary>
        /// 通过搜索字段找到的表
        /// </summary>
        [Description("用户表")]
        U_C = 4,
        [Description("存储过程")]
        P = 2,
        [Description("表值函数")]
        TF = 3,
    }
}
