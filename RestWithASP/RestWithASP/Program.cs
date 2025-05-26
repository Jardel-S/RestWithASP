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
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Rewrite;

namespace RestWithASP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var appName  = "REST API's RESTful From 0 to Azure with ASP.NET Core 8 and Docker";
            var appVersion  = "v1";
            var appDescription  = $"REST API RESTful developed in course '{appName}'";

            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            // Add services to the container.

            builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            builder.Services.AddControllers();

            //Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(appVersion,
                    new OpenApiInfo
                    {
                        Title = appName,
                        Version = appVersion,
                        Description = appDescription,
                        Contact = new OpenApiContact
                        {
                            Name = "Jardel Moreno",
                            Url = new Uri("https://github.com/Jardel-S")
                        }
                    });
            });

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

            app.UseCors();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{appName} - {appVersion}");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

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