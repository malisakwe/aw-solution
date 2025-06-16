namespace Store.Domain.Entities.Identity
{
    public class User: BaseEntity
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; } = true;

        public string Role { get; set; }
        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiry { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; }
        // Navigation properties can be added here if needed, e.g., for roles or claims
        
    }
}
