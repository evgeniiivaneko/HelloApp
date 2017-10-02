using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Text;

namespace HelloApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDirectoryBrowser();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseOwin(pipeline => {
                pipeline(next => SendResponceAsync);
            });

        }

        public Task SendResponceAsync(IDictionary<String, Object> enviroment)
        {
            String responseText = "Hello ASP.NET Core";
            Byte[] responseBytes = Encoding.UTF8.GetBytes(responseText);

            var responseStream = (Stream)enviroment["owin.ResponseBody"];

            return responseStream.WriteAsync(responseBytes, 0, responseBytes.Length);
        }
    }
}
