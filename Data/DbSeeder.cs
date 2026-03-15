using Mounret.API.Data;
using Mounret.API.Models;

public static class DbSeeder
{
    public static void SeedAdmin(ApplicationDbContext context)
    {
        if (!context.Users.Any(x => x.Role == "Admin"))
        {
            var admin = new User
            {
                Name = "Admin",
                Email = "admin@mounret.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123"),
                Role = "Admin"
            };

            context.Users.Add(admin);
            context.SaveChanges();
        }
    }
}