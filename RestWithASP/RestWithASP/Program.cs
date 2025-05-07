using RestWithASP.Model.Context;
using RestWithASP.Services;
using RestWithASP.Services.Implementations;
using Microsoft.EntityFrameworkCore;

namespace RestWithASP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            var connection = builder.Configuration["SQLServerConnection:SQLServerConnectionString"];
            builder.Services.AddDbContext<SQLServerContext>(options => options.UseSqlServer(connection));

            //Dependency Injection
            builder.Services.AddScoped<IPersonService, PersonServiceImplementation>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}