using Autofac;
using Autofac.Extensions.DependencyInjection;
using DBH.BLLService;
using DBH.DALServices;
using DBH.Models.Config;

namespace DataBaseHelper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

            builder.Services.AddControllersWithViews();
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            #region 配置AutoFac
            builder.Host.ConfigureContainer<ContainerBuilder>(autofac_builder =>
            {
                //注册数据访问层模块
                autofac_builder.RegisterModule<AutofacDALServiceModule>();
                //注册业务处理层模块
                autofac_builder.RegisterModule<AutofacBLLServcieModule>();
            });
            #endregion
            builder.Services.Configure<DBHSetting>(builder.Configuration.GetSection("DBHSetting"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=database}/{action=Index}/{id?}");
            });
            app.Run();
        }
    }
}