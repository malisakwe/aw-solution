using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces;
using Store.Domain.Entities.Identity;
using Store.Infrastructure.Data;

namespace Store.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly AdventureWorksDbContext _context;

        public UserRepository(AdventureWorksDbContext context)
        {
            _context = context; 
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByUsername(string username)
        {
            return await _context.Users
                .AnyAsync(u => u.Username == username);
        }

        public async Task<bool> ExistsByEmail(string email)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email);
        }
    }
}
