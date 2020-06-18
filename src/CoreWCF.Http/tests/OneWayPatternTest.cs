using CoreWCF.Configuration;
using Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace BasicHttp
{
    public class OneWayPatternTest
    {
        private ITestOutputHelper _output;
        public OneWayPatternTest(ITestOutputHelper output)
        {
            _output = output;
        }

#if NET472
        [Fact]
        public void InvokeTaskBasedOneway()
        {
            var host = ServiceHelper.CreateWebHostBuilder<Startup>(_output).Build();
            using (host)
            {
                host.Start();
                var httpBinding = ClientHelper.GetBufferedModeBinding();
                var factory = new System.ServiceModel.ChannelFactory<ClientContract.IOneWayContract>(httpBinding,
                    new System.ServiceModel.EndpointAddress(new Uri("http://localhost:8080/OneWayPattern/basichttp.svc")));
                var channel = factory.CreateChannel(); 
                Task task = channel.OneWay("Hello");
                task.Wait();
            }
        }
#endif
        internal class Startup
        {
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddServiceModelServices();
            }

#pragma warning disable IDE0060 // Remove unused parameter
            public void Configure(IApplicationBuilder app, IHostingEnvironment env)
#pragma warning restore IDE0060 // Remove unused parameter
            {
                app.UseServiceModel(builder =>
                {
                    builder.AddService<Services.OneWayService>();
                    builder.AddServiceEndpoint<Services.OneWayService, ServiceContract.IOneWayContract>(new CoreWCF.BasicHttpBinding(), "/OneWayPattern/basichttp.svc");
                });
            }
        }
    }
}
