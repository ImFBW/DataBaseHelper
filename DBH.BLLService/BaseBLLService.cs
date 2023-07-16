using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.BLLService
{
    public abstract class BaseBLLService
    {
        /// <summary>
        /// 定义一个logger，用来记录日志,子类继承该方法的时候已自动实现分类
        /// </summary>
        internal ILogger Logger { get; set; }


    }
}
