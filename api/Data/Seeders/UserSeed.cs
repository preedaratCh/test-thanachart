using api.Data;
using Microsoft.EntityFrameworkCore;
using api.Models.Entities;
using api.Helpers;

namespace api.Data.Seeders
{
    public static class UserSeed
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            var hasAdmin = await context.Users.AnyAsync(u => u.Email == "admin@email.com");
            if (hasAdmin)
            {
                return;
            }

            await context.Users.AddAsync(new User
            {
                Id = Guid.NewGuid(),
                Name = "Admin",
                Email = "admin@email.com",
                Password = AppHelper.HashPassword("password"),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder",
            });

            await context.SaveChangesAsync();
        }
    }
}