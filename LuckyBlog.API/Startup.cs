using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using LuckyBlog.IRepository;
using LuckyBlog.Repository;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LuckyBlog.API
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;//测试一下下
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            //���ÿ�����������������Դ��
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
                //�����ĵ���Ϣ
                c.SwaggerDoc("One", new OpenApiInfo
                {
                    Title = "APIServiceOne",
                    Version = "v1"
                });
                //��controller���ע�����ӵ�swaggerui��
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var CommentsFileName = @"LuckyBlog.xml";
                var CommentsFile = Path.Combine(baseDirectory, CommentsFileName);
                //��ע�͵�Xml�ĵ����ӵ�swaggerUi��
                c.IncludeXmlComments(CommentsFile);
                #region Swaggerʹ�ü�Ȩ���
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "ֱ�����¿�������Bearer {token}��ע������֮����һ���ո�",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                  {
                    new OpenApiSecurityScheme
                    {
                      Reference=new OpenApiReference
                      {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                      }
                    },
                    new string[] {}
                  }
                });
                #endregion

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

            #region JWT��Ȩ
            services.AddCustomJWT();
            #endregion
        }
        /// <summary>
        /// �м��
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            //����Swagger�м��
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/One/swagger.json", "APIService One");
            });

            app.UseRouting();
            //ʹ�ùܵ�--��Ȩ
            app.UseAuthentication();
            //ʹ�ùܵ�--��Ȩ
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
    static class IOCExtend
    {
        /// <summary>
        /// IOCע��
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomIOC(this IServiceCollection services)
        {
            //������Ϣ
            services.AddScoped<IBlogNewsRepository, BlogNewsRepository>();
            services.AddScoped<IBlogNewsService, BlogNewsService>();
            //����
            services.AddScoped<ITypeInfoRepository, TypeInfoRepository>();
            services.AddScoped<ITypeInfoService, TypeInfoService>();
            //�û�
            services.AddScoped<IWriterInfoRepository, WriterInfoRepostitory>();
            services.AddScoped<IWriterInfoService, WriterInfoService>();

            return services;
        }
        /// <summary>
        /// jwt��Ȩ
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomJWT(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDMC-CJAS1-SAD-DFSFA-SADHJVF-VF")),
                    ValidateIssuer = true,
                    ValidIssuer = "http://localhost:6060",
                    ValidateAudience = true,
                    ValidAudience = "http://localhost:5000",
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(60)
                };
            });
            return services;
        }

    }
}
