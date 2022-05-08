using FiltersAttributes.Entities;
using FiltersAttributes.Helpers;
using System.Text.Json.Serialization;
using FiltersAttributes.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    var services = builder.Services;
    var env = builder.Environment;

    services.AddDbContext<DataContext>();
    services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase(databaseName: "InMemoryDb"),
                            ServiceLifetime.Scoped,
                            ServiceLifetime.Scoped);

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
    var users = new List<User>
    {
        new User { Id = 1, Name ="admin@company.com" ,DepartmentName ="Accounting-Admin"},
        new User { Id = 2, Name ="member@company.com",DepartmentName ="Accounting-Member" }
    };

    var roles = new List<Role>
    {
        new Role { Id = 1, Name ="admin"},
        new Role { Id = 2, Name ="member" }
    };

    var roleUsers = new List<User_Role>
    {
        new User_Role {Id=1, RoleId = 1, UserId =1},
        new User_Role {Id=1, RoleId = 2,  UserId =2}
    };

    var permission = new List<Permission>
    {
        new Permission {Id=1, RoleId = 1, Name ="View" ,Type = FiltersAttributes.Entities.Type.View},
        new Permission {Id=2, RoleId = 1, Name ="Add" ,Type = FiltersAttributes.Entities.Type.Add},
        new Permission {Id=3, RoleId = 1, Name ="Edit" ,Type = FiltersAttributes.Entities.Type.Edit},
        new Permission {Id=4, RoleId = 1, Name ="Delete" ,Type = FiltersAttributes.Entities.Type.Delete}
    };


    using var scope = app.Services.CreateScope();


    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();

    dataContext.Users.AddRange(users);
    dataContext.Roles.AddRange(roles);
    dataContext.User_Roles.AddRange(roleUsers);
    dataContext.Permissions.AddRange(permission);

    dataContext.SaveChanges();
}

app.Run("http://localhost:4000");
