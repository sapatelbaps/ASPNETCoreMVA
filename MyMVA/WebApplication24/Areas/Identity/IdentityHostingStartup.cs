using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(WebApplication24.Areas.Identity.IdentityHostingStartup))]
namespace WebApplication24.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}