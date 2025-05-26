using RestWithASP.Model.Context;
using RestWithASP.Business;
using RestWithASP.Business.Implementations;
using Microsoft.EntityFrameworkCore;
using RestWithASP.Repository;
using Serilog;
using EvolveDb;
using Microsoft.Data.SqlClient;
using RestWithASP.Repository.Generic;
using Microsoft.Net.Http.Headers;
using RestWithASP.Hypermedia.Filters;
using RestWithASP.Hypermedia.Enricher;

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

            if (builder.Environment.IsDevelopment())
            {
                MigrateDatabase(connection);
            }

            builder.Services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;

                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            })
            .AddXmlSerializerFormatters();

            //Config HETEAOAS
            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
            filterOptions.ContentResponseEnricherList.Add(new BookEnricher());

            builder.Services.AddSingleton(filterOptions);

            //Vertioning API
            builder.Services.AddApiVersioning();

            //Dependency Injection
            builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
            builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllerRoute("DefaultApi", "{controller=values}/v{version=apiVersion}/{id?}");

            app.MapControllers();

            app.Run();

            void MigrateDatabase(string connection)
            {
                try
                {
                    var evolveConnection = new SqlConnection(connection);
                    var evolve = new Evolve(evolveConnection, Log.Information)
                    {
                        Locations = new List<string> { "db/migrations", "db/dataset" },
                        IsEraseDisabled = true,
                    };
                    evolve.Migrate();
                }
                catch (Exception ex)
                {
                    Log.Error("Database migration failed", ex);
                    throw;
                }
            }
        }

    }
}