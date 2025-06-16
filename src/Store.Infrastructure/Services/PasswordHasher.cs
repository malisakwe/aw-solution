using Store.Application.Services;
namespace Store.Infrastructure.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int WorkFactor = 12;

        public PasswordHasher()
        {
            
        }
        public PasswordHasher(string salt)
        {
            // If you need to use a specific salt, you can implement it here
            // However, BCrypt.Net does not require a salt to be passed explicitly
            // as it generates its own salt when hashing the password.
        }
        public string Hash(string password)
        {
            // Fix: Use the correct method name from the BCrypt.Net library
            return BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
        }

        public bool Verify(string password, string passwordHash)
        {
            // Fix: Use the correct method name from the BCrypt.Net library
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
