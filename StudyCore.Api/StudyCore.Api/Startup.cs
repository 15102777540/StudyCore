using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using StudyCore.Model.Auth;
using StudyCore.Repository;
using Swashbuckle.AspNetCore.Filters;

namespace StudyCore.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppAuthenticationSettings>(appSettingsSection);
            // JWT
            var appSettings = appSettingsSection.Get<AppAuthenticationSettings>();
            services.AddJwtBearerAuthentication(appSettings);
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers().AddNewtonsoftJson();

            //Add-Migration InitialCreate ����Ǩ��
            //Update-Database �������ݿ�
            //Script-Migration ���� SQL �ű�
            //Remove-Migration ɾ��Ǩ��
            //b => b.MigrationsAssembly("StudyCore.Api") �ܹؼ�
            services.AddDbContext<StudyDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),b => b.MigrationsAssembly("StudyCore.Api")));


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                var basePath = PlatformServices.Default.Application.ApplicationBasePath; //var basePath = AppDomain.CurrentDomain.BaseDirectory;
                
                var xmlModelPath = Path.Combine(basePath, "StudyCore.Api.xml");
                c.IncludeXmlComments(xmlModelPath);
                //�������Model���xml�ļ���                                           
                //Core.Admin.webapi.xml���ҵ���Ŀ����XML�ĵ��ĺ�׺��,�����������ĿΪ��
                var xmlPath = Path.Combine(basePath, "StudyCore.Model.xml"); 
                //�ڶ�������Ϊtrue�Ļ���������ϵ�ע��Ҳ����ʾ(Ĭ��false)
                c.IncludeXmlComments(xmlPath, true);

                // ������ȨС��
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                // ��header�����token�����ݵ���̨
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                // Jwt Bearer ��֤�������� oauth2
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) ֱ�����¿�������Bearer {token}��ע������֮����һ���ո�\"",
                    Name = "Authorization",//jwtĬ�ϵĲ�������
                    In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
                    Type = SecuritySchemeType.ApiKey
                });
            });

            // ע����־
            services.AddLogging(config =>
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Log4net.config");
                config.AddLog4Net(path);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseFileServer();
            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
