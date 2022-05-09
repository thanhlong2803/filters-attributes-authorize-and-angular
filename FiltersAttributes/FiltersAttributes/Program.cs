using FiltersAttributes.Entities;
using FiltersAttributes.Helpers;
using System.Text.Json.Serialization;
using FiltersAttributes.Services;
using Microsoft.EntityFrameworkCore;
using Type = FiltersAttributes.Type;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    var services = builder.Services;
    var env = builder.Environment; 
    services.AddDbContext<DataContext>();

    services.AddCors();
    services.AddControllers().AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

    // configure DI for application services
    services.AddScoped<IUserService, UserService>();
}


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


// create hardcoded test users in db on startup
{  
    using var scope = app.Services.CreateScope();
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    dataContext.SaveChanges();
}

app.Run("http://localhost:4000");
