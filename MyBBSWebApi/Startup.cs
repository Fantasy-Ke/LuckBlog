using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MyBBS.IRepository;
using MyBBS.Repository;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MyBBSWebApi
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

            services.AddControllers();
            //���ÿ���������������Դ��
            services.AddCors(options =>
            {
                options.AddPolicy("all", builder =>
                {
                    builder.AllowAnyOrigin() //�����κ���Դ����������
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            services.AddSwaggerGen(c =>
            {
                //����ĵ���Ϣ
                c.SwaggerDoc("One", new OpenApiInfo
                {
                    Title = "APIServiceOne",
                    Version = "v1"
                });
                //��controller���ע����ӵ�swaggerui��
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var CommentsFileName = @"MyBBSWebApi.xml";
                var CommentsFile = Path.Combine(baseDirectory, CommentsFileName);
                //��ע�͵�Xml�ĵ���ӵ�swaggerUi��
                c.IncludeXmlComments(CommentsFile);
            });
            #region SqlSugarIOC

            services.AddSqlSugar(new IocConfig()
            {
                ConnectionString = this.Configuration["SqlConn"],
                DbType = IocDbType.SqlServer,
                IsAutoCloseConnection = true//�Զ��ͷ�
            });
            #endregion
            #region IOC����ע��
            services.AddCustomIOC();
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }
            //���Swagger�м��
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/One/swagger.json", "APIService One");
            });

            app.UseRouting();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        
    }
     static class IOCExtend
    {
            public static IServiceCollection AddCustomIOC(this IServiceCollection services)
            {
                services.AddScoped<IBlogNewsRepository, BlogNewsRepository>();
                services.AddScoped<IBlogNewsService, BlogNewsService>();

                services.AddScoped<ITypeInfoRepository, TypeInfoRepository>();
                services.AddScoped<ITypeInfoService, TypeInfoService>();

                services.AddScoped<IWriterInfoRepository, WriterInfoRepostitory>();
                services.AddScoped<IWriterInfoService, WriterInfoService>();

                return services;
             }
    }
}
