using FiltersAttributes.Entities;
using Microsoft.EntityFrameworkCore;

namespace FiltersAttributes.Helpers
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User_Role> User_Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: "Test").Options;
            using (var context = new DataContext(options))
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
                        new User_Role {Id=2, RoleId = 2,  UserId =2}
                    };

                var permission = new List<Permission>
                    {
                        new Permission {Id=1, RoleId = 1, Name ="View" ,Type = Type.View},
                        new Permission {Id=2, RoleId = 1, Name ="Add" ,Type = Type.Add},
                        new Permission {Id=3, RoleId = 1, Name ="Edit" ,Type = Type.Edit},
                        new Permission {Id=4, RoleId = 1, Name ="Delete" ,Type = Type.Delete}
                    };



                context.Users.AddRange(users);
                context.Roles.AddRange(roles);
                context.User_Roles.AddRange(roleUsers);
                context.Permissions.AddRange(permission);

                context.SaveChanges();
            }
        }
    }
}
