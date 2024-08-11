using RestAPI_AspNet.Repository.Implementations;
using RestAPI_AspNet.Repository;
using RestAPI_AspNet.Model.Context;
using Microsoft.EntityFrameworkCore;
using RestAPI_AspNet.Business;
using RestAPI_AspNet.Business.Implementations;
using MySqlConnector;
using Serilog;
using EvolveDb;
using RestAPI_AspNet.Repository.Generic;
using System.Net.Http.Headers;
using RestAPI_AspNet.Hypermedia.Enricher;
using RestAPI_AspNet.Hypermedia.Filters;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Rewrite;


var builder = WebApplication.CreateBuilder(args);
var appName = "REST API's RESTful From 0 to Azure with ASP.NET Core 8 and Docker";
var appVersion = "v1";
var appDescription = $"REST API RESTful developed in course '{appName}'";

// Add services to the container.

builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add Cors
builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyMethod();

}));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// Add Swagger
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
                Name = "Mauro Elias",
                Url = new Uri("https://github.com/Mauro-Benitez")
            }
        });
});


// Configure Data base connection.

var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];

// Configure Data base context.
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 36))));


if (builder.Environment.IsDevelopment())
{
    MigrateDatabase(connection);
}


//Formatters XML and Json
builder.Services.AddMvc(options =>
{
    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
    options.FormatterMappings.SetMediaTypeMappingForFormat("json", "application/json");
})
.AddXmlSerializerFormatters();


var filterOptions = new HyperMediaFilterOptions();
filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
filterOptions.ContentResponseEnricherList.Add(new BookEnricher());

builder.Services.AddSingleton(filterOptions);

//Versioning API
builder.Services.AddApiVersioning();


//Dependency Injection
builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementations>();


builder.Services.AddScoped<IBookBusiness,  BookBusinessImplementations>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

//Configure Cors
app.UseCors();

//Configure Swagger
app.UseSwagger();

app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json",
        $"{appName} - {appName}");
});

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute("DefaultApi", "{controller=values}/v{version=ApiVersion}/{id?}");


app.Run();

void MigrateDatabase(string connetion)
{
    try
    {
        var evolveConnection = new MySqlConnection(connetion);
        var evolve = new Evolve(evolveConnection, Log.Information)
        {
            Locations = new List<string> { "db/migrations", "db/dataset" },
            IsEraseDisabled = true
        };

        evolve.Migrate();

    }
    catch (Exception ex)
    {
        Log.Error("Database migrations failed", ex);
        throw;
    }
}