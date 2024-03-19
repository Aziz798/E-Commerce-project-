using E_Commerce.Server.Entities;

namespace E_Commerce.Server.Services.Interfaces
{
    public interface IUserRepository
    {
         public Task<User> RegisterAsync(User user);
        public Task<User> LoginAsync(string password, string email);
        public Task<IEnumerable<User>> GetAllUsersAsync();
        public Task DeleteUserAsync(int id);
        public Task<User> GetUserByIdAsync(int id);
        public Task<User> UpdateUserAsync(User user);
    }
}
