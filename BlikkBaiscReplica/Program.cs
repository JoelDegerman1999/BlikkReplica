using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BlikkBaiscReplica
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var host = CreateHostBuilder(args).Build();
            
            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    try
            //    {
            //        var context = services.GetRequiredService<ApplicationDbContext>();
            //        context.Database.Migrate();
            //        Seeder.SeedUsers(context);
            //    }
            //    catch (Exception e)
            //    {
                    
            //    }
            //}
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
