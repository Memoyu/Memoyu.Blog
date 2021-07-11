using AspNetCoreRateLimit;
using Autofac.Extensions.DependencyInjection;
using Blog.Core.Common.Configs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Blog
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
#if DEBUG
            Serilog.Debugging.SelfLog.Enable(msg =>
                Debug.WriteLine(msg)
            );
#endif


            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Appsettings.Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            try
            {
                Log.Information("init main");
                IHost webHost = CreateWebHostBuilder(args).Build();
                try
                {
                    using var scope = webHost.Services.CreateScope();
                    // get the ClientPolicyStore instance
                    var clientPolicyStore = scope.ServiceProvider.GetRequiredService<IClientPolicyStore>();

                    // seed Client data from appsettings
                    await clientPolicyStore.SeedAsync();

                    // get the IpPolicyStore instance
                    var ipPolicyStore = scope.ServiceProvider.GetRequiredService<IIpPolicyStore>();

                    // seed IP data from appsettings
                    await ipPolicyStore.SeedAsync();
                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, "IIpPolicyStore RUN Error");
                }
                await webHost.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory()) //添加Autofac服务工厂
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
#if DEBUG
                        .UseUrls("http://*:10088");
#endif
                    ;
                })
                .UseSerilog(); //构建Serilog;

    }
}
