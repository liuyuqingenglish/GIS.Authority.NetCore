using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using GIS.Authority.Common;
using System.Net.Sockets;
using System.Net;
namespace GIS.Authority.NetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
           // NTPClient client = new NTPClient("localhost");
           // client.Connect(true);
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Æô¶¯web
        /// </summary>
        /// <param name="args">²ÎÊý</param>
        /// <returns>builder</returns>
        public static IHostBuilder CreateWebHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(build =>
            {
                build.UseKestrel().UseUrls($"http://*:{ConfigurationData.ProgramPort}").UseStartup<Startup>();
            })
         .UseServiceProviderFactory(new AutofacServiceProviderFactory());

             // ÅäÖÃHttp2
            // return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(build =>
            // {
            //     build.UseKestrel().ConfigureKestrel((conttext,option) => {
            //         option.ListenLocalhost(ConfigurationData.ProgramPort, list =>
            //         {
            //             list.Protocols = HttpProtocols.Http2;
            //         });

            //     }).UseStartup<Startup>();
            // })
            //.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        }
    }
}