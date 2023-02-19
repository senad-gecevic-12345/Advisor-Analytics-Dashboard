using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using fullstack_1.Database;
using fullstack_1.Controllers;

namespace fullstack_1
{
    public class Startup
    {
        private static IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            services.AddControllers();
            services.AddMediatR(typeof(Startup).Assembly);
            //services.AddDbContext<DatabaseContext>(options =>
            //options.UseSqlServer(ConfigurationExtensions.GetConnectionString(_configuration, "DB")));
            //Database.SQL.Instance.init();

                    }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
