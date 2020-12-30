using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using GIS.Authority.Common;
using GIS.Authority.Service;
using GIS.Authority.Entity;
namespace GIS.Authority.NetCore
{
    public class Startup
    {
        /// <summary>
        /// cors跨域标签
        /// </summary>
        private const string CORS_TAG = "cors";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule<ServiceModule>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //cors 跨域配置
            services.AddCors(corsoption =>
            {
                corsoption.AddPolicy(CORS_TAG, policy =>
                {
                    policy.SetIsOriginAllowed(x => true);
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithOrigins("*").AllowCredentials();
                });
            });

            services.AddHttpClient<GlobalClientService>(option => {
                option.DefaultRequestVersion = new Version(2, 0);
            });

            // signalr 设置signal 集群
            services.AddSignalR(hubconifg =>
            {
                // 超时60s
                hubconifg.KeepAliveInterval = TimeSpan.FromSeconds(15);
                hubconifg.ClientTimeoutInterval = TimeSpan.FromSeconds(60);
                hubconifg.EnableDetailedErrors = true;
            }).AddRedis(ConfigurationData.RedisConnectionStrings);

            services.AddSendService(action => action.UseSms());

            // automapper
            services.AddAutoMapper(typeof(BasicProfile));

            // cookies
            services.Configure<CookiePolicyOptions>(option =>
            {
                option.CheckConsentNeeded = context => true;
                option.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });

            // webapi
            services.AddControllers(option =>
            {
                option.Filters.Add(new BasicExceptionFilter());
            }).AddJsonOptions(option =>
            {
                //小写
                option.JsonSerializerOptions.PropertyNamingPolicy = new LowercasePolicy();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //及时通信消息
            services.AddSingleton(typeof(GlobalHubMessage<MessageHub>));
            services.AddScoped<IMapper, Mapper>();

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "COREAPI", Version = "v1" });

                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "GIS.Authority.NetCore.xml");
                c.IncludeXmlComments(xmlPath);
            }

            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHttpContextAccessor accessor, IMapper mapper, IWebHostEnvironment env)
        {
            HttpContextExtension.httpContextAccessor = accessor;
            MapperExtension.Mapper = mapper;

            //获取远程ip
            app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //位置 介于 router之后 endpoint之前
            app.UseCors(CORS_TAG);

            app.UseCookiePolicy();

            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //配置signalr
                endpoints.MapHub<MessageHub>("/Eir/Duplex", config =>
                {
                    config.ApplicationMaxBufferSize = 300 * 1024 * 1024;
                });
                endpoints.MapControllers();
            });

            app.UseSwagger();

            // 配置SwaggerUI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoreWebApi");
            });
        }
    }
}