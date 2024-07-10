using RestAPI_AspNet.Services.Implementations;
using RestAPI_AspNet.Services;
using RestAPI_AspNet.Model.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// Configure Data base connection.

var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];

// Configure Data base context.
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 36))));


//Versioning API
builder.Services.AddApiVersioning();


//Dependency Injection
builder.Services.AddScoped<IPersonService, PersonServiceImplementations>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
