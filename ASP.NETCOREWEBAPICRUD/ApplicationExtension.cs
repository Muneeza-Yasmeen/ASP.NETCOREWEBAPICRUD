using System.Runtime.CompilerServices;
using Serilog;
namespace ASP.NETCOREWEBAPICRUD
{
    public static class ApplicationExtension
    {
        public static void ConfigureSerilog(this IHostBuilder host)
        {
            host.UseSerilog((ctx, lc) =>
            {
                lc.WriteTo.Console();
                lc.WriteTo.Seq("https://localhost:7280").
                CreateLogger();
            });
        }
    }
}
