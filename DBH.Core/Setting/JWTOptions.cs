using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Core.Setting
{
    /// <summary>
    /// JWT配置项
    /// </summary>
    public class JWTOptions
    {
        /// <summary>
        /// 秘钥
        /// HmacSha256模式：长度必须超过256位
        /// </summary>
        public string SecurityKey {  get; set; }
        /// <summary>
        /// 是否验证Issuer
        /// </summary>
        public bool Issuer { get; set; }
        /// <summary>
        /// 是否验证Audience
        /// </summary>
        public bool Audience { get; set; }

        /// <summary>
        /// token过期时间
        /// </summary>
        public int ExprieSeconds { get; set; }
    }
}
