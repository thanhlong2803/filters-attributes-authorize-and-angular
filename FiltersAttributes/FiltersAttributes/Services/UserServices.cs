using FiltersAttributes.Entities;
using FiltersAttributes.Helpers;

namespace FiltersAttributes.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }
        public List<User> GetAllUser()
        {
            var getUsers = _context.Users.ToList();
            return getUsers;
        }
    }
}
