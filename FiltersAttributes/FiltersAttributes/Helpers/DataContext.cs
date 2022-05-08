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

        private readonly IConfiguration Configuration;
        
        public DataContext(IConfiguration configuration) 
        {
            Configuration = configuration;
        }
    }
}
