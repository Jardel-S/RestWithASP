using RestWithASP.Model.Context;
using RestWithASP.Business;
using RestWithASP.Business.Implementations;
using Microsoft.EntityFrameworkCore;
using RestWithASP.Repository;
using RestWithASP.Repository.Implementations;

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

            //Vertioning API
            builder.Services.AddApiVersioning();

            //Dependency Injection
            builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
            builder.Services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}