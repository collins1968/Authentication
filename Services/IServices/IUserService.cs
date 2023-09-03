using Auth.Model;

namespace Auth.Services.IServices
{
    public interface IUserService
    {
        Task<string> Register(User user);
        Task<User> GetUserByEmail(string email);
        Task<string> UpdateUser(User user);
        Task<string> DeleteUser(User user);
        Task<IEnumerable<User>> GetUsers();
    }
}
