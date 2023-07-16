using Autofac;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBH.BLLService
{
    /// <summary>
    /// 注册业务模块服务
    /// </summary>
    public class AutofacBLLServcieModule : Autofac.Module
    {
        /// <summary>
        /// 重写父级Load方法
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            //获取当前程序集下的所有类=>非abstract类并且类名以“BLLService”结尾的类
            var needRegTypes = typeof(BaseBLLService).Assembly.GetTypes().Where(p => !p.IsAbstract && p.Name.EndsWith("BLLService"));
            //var registTypes = typeof(BaseBLLService).Assembly;
            //var registTypes = Assembly.Load("DBH.BLLService");
            //var registTypes2 = Assembly.Load("DBH.BLLProvider");
            builder.RegisterTypes(needRegTypes.ToArray())
                //.Where(p => !p.IsAbstract && p.Name.EndsWith("BLLService"))
                .AsImplementedInterfaces()  //以接口形式注入
                .PropertiesAutowired()      //属性自动注入
                // .SingleInstance();          //
                .InstancePerLifetimeScope()    //
                .OnActivated(x =>
                 {
                     if (x.Instance is BaseBLLService baseService)
                     {
                         if (x.Context.TryResolve<ILoggerFactory>(out var loggerFactory))
                         {
                             baseService.Logger = loggerFactory.CreateLogger(x.Instance.GetType().Name);
                         }
                     }
                 });
        }
    }
}
