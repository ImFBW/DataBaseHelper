using Autofac;
using Autofac.Extensions.DependencyInjection;
using DataBaseHelper.Common;
using DBH.BLLService;
using DBH.Core.Setting;
using DBH.DALServices;
using DBH.Models.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            #region 自定义配置模块
            builder.Services.Configure<DBHSetting>(builder.Configuration.GetSection("DBHSetting"));
            builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection("JWT"));
            #endregion

            var jwtSettings = builder.Configuration.GetSection("JWT").Get<JWTOptions>();
            builder.Services.AddSingleton(new JWTHelper(jwtSettings));

            #region 配置JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {                
                byte[] keyBytes = Encoding.UTF8.GetBytes(jwtSettings.SecurityKey);
                var secKey = new SymmetricSecurityKey(keyBytes);
                opt.TokenValidationParameters = new()
                {
                    ValidateIssuer = jwtSettings.Issuer,//是否验证Issuer
                    ValidateAudience = jwtSettings.Audience,//是否验证Audience
                    ValidateLifetime = true,//是否验证失效时间
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.FromSeconds(30),//过期时间容错值，解决服务器端时间不同步问题
                    IssuerSigningKey = secKey,
                    RequireExpirationTime = true,
                };
            });
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();//认证（登录）
            app.UseAuthorization();//授权

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=database}/{action=Index}/{id?}");
            });


            app.Run();
        }
    }
}