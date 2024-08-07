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



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


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