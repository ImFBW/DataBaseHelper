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

            #region ����AutoFac
            builder.Host.ConfigureContainer<ContainerBuilder>(autofac_builder =>
            {
                //ע�����ݷ��ʲ�ģ��
                autofac_builder.RegisterModule<AutofacDALServiceModule>();
                //ע��ҵ�����ģ��
                autofac_builder.RegisterModule<AutofacBLLServcieModule>();
            });
            #endregion

            #region �Զ�������ģ��
            builder.Services.Configure<DBHSetting>(builder.Configuration.GetSection("DBHSetting"));
            builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection("JWT"));
            #endregion

            var jwtSettings = builder.Configuration.GetSection("JWT").Get<JWTOptions>();
            builder.Services.AddSingleton(new JWTHelper(jwtSettings));

            #region ����JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {                
                byte[] keyBytes = Encoding.UTF8.GetBytes(jwtSettings.SecurityKey);
                var secKey = new SymmetricSecurityKey(keyBytes);
                opt.TokenValidationParameters = new()
                {
                    ValidateIssuer = jwtSettings.Issuer,//�Ƿ���֤Issuer
                    ValidateAudience = jwtSettings.Audience,//�Ƿ���֤Audience
                    ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.FromSeconds(30),//����ʱ���ݴ�ֵ�������������ʱ�䲻ͬ������
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

            app.UseAuthentication();//��֤����¼��
            app.UseAuthorization();//��Ȩ

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