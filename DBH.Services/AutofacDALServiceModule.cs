using Autofac;
using DBH.DALProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBH.DALServices
{
    /// <summary>
    /// 注册所有Service类
    /// </summary>
    public class AutofacDALServiceModule : Autofac.Module
    {
        /// <summary>
        /// 重写父级Load方法
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            //注册数据库连接服务
            //builder.RegisterType<DBConnectionService>().As<IDBConnectionProvider>()
            //    .InstancePerLifetimeScope().PropertiesAutowired();

            //获取当前程序集下的所有类=>非abstract类并且类名以“DALService”结尾的类
            var needRegTypes = typeof(BaseDALService).Assembly.GetTypes().Where(p => !p.IsAbstract && p.Name.EndsWith("DALService"));
            //var registTypes = typeof(BaseDALService).Assembly;
            //var registTypes = Assembly.Load("DBH.DALServices");
            builder.RegisterTypes(needRegTypes.ToArray())//.As()
               // .Where(p => !p.IsAbstract && p.Name.EndsWith("DALService"))
                .AsImplementedInterfaces()  //以接口形式注入
                .PropertiesAutowired()      //属性自动注入
                .SingleInstance()        //所有访问DB的服务类都应该是单例的
                .OnActivated(x =>
                 {
                     Console.WriteLine("Debug[A001]:"+x.Instance.ToString());
                     
                 });
        }
    }
}
