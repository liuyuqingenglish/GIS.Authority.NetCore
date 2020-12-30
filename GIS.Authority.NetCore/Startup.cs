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
        /// cors�����ǩ
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
            //cors ��������
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

            // signalr ����signal ��Ⱥ
            services.AddSignalR(hubconifg =>
            {
                // ��ʱ60s
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
                //Сд
                option.JsonSerializerOptions.PropertyNamingPolicy = new LowercasePolicy();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //��ʱͨ����Ϣ
            services.AddSingleton(typeof(GlobalHubMessage<MessageHub>));
            services.AddScoped<IMapper, Mapper>();

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "COREAPI", Version = "v1" });

                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//��ȡӦ�ó�������Ŀ¼�����ԣ����ܹ���Ŀ¼Ӱ�죬������ô˷�����ȡ·����
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

            //��ȡԶ��ip
            app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //λ�� ���� router֮�� endpoint֮ǰ
            app.UseCors(CORS_TAG);

            app.UseCookiePolicy();

            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //����signalr
                endpoints.MapHub<MessageHub>("/Eir/Duplex", config =>
                {
                    config.ApplicationMaxBufferSize = 300 * 1024 * 1024;
                });
                endpoints.MapControllers();
            });

            app.UseSwagger();

            // ����SwaggerUI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoreWebApi");
            });
        }
    }
}