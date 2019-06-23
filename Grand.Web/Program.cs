using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;

namespace Grand.Web
{
   public class Program
   {
      public static IHostingEnvironment HostingEnvironment { get; set; }
      public static void Main(string[] args)
      {
         try
         {
            CreateWebHostBuilder(args).Build().Run();
         }
         catch (Exception ex)
         {
            Console.WriteLine($"{ex.Message}");
         }
      }
      public static IWebHostBuilder CreateWebHostBuilder(string[] args)
      {
         var curDir = Directory.GetCurrentDirectory();
         var host = WebHost.CreateDefaultBuilder(args)
               .CaptureStartupErrors(true)
               .UseSetting(WebHostDefaults.PreventHostingStartupKey, "true")
               .UseStartup<Startup>()
               .ConfigureKestrel((context, options) =>
               {
                  var httpPort = context.Configuration.GetValue<int>("Hosting:ListeningPorts:Http");
                  var httpsPort = context.Configuration.GetValue<int>("Hosting:ListeningPorts:Https");

                  options.Listen(IPAddress.Any, httpPort);
               })
              ;
         return host;
      }
   }
}