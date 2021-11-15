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
            //配置跨域处理，允许所有来源：
            services.AddCors(options =>
            {
                options.AddPolicy("all", builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            services.AddSwaggerGen(c =>
            {
                //添加文档信息
                c.SwaggerDoc("One", new OpenApiInfo
                {
                    Title = "APIServiceOne",
                    Version = "v1"
                });
                //将controller层的注释添加的swaggerui中
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var CommentsFileName = @"MyBBSWebApi.xml";
                var CommentsFile = Path.Combine(baseDirectory, CommentsFileName);
                //将注释的Xml文档添加到swaggerUi中
                c.IncludeXmlComments(CommentsFile);
            });
            #region SqlSugarIOC

            services.AddSqlSugar(new IocConfig()
            {
                ConnectionString = this.Configuration["SqlConn"],
                DbType = IocDbType.SqlServer,
                IsAutoCloseConnection = true//自动释放
            });
            #endregion
            #region IOC依赖注入
            services.AddCustomIOC();
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }
            //添加Swagger中间件
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
