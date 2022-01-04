using Authorization.Basics.Data;
using Authorization.Basics.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Basics
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using(var scope =  host.Services.CreateScope())
            {
                Databaseinitializer.Init(scope.ServiceProvider);
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
    
    public static class Databaseinitializer
    {
        internal static void Init(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            var user = new ApplicationUser
            {
                UserName = "User",
                FirstName = "Artem",
                LastName = "Naumov",
            };

            var result = userManager.CreateAsync(user, "pass123").GetAwaiter().GetResult();

            if(!result.Succeeded)
            {

            }

            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
