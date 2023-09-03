using Auth.Data;
using Auth.Model;
using Auth.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Auth.Services
{
    public class UserService : IUserService
    {
        private readonly MyDbContext _context;

        public UserService(MyDbContext context)
        {
            _context = context;
        }
        public Task<string> DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
            return Task.FromResult("User Deleted Successfully");
        }

        public async Task<User> GetUserByEmail(string email)
        {
           return _context.Users.Where(x => x.email == email).FirstOrDefault();
            
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
           return await _context.Users.ToListAsync();
        }

        public async Task<string> Register(User user)
        {
           _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return "User Registered Successfully";

        }

        public Task<string> UpdateUser(User user)
        {
           _context.Users.Update(user);
            _context.SaveChanges();
            return Task.FromResult("User Updated Successfully");
        }
    }
}
