using api.Data;
using api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Data.Seeders;

public static class CategorySeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var now = DateTime.UtcNow;
        var targetCategories = new List<Category>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Laptop",
                Description = "Notebook computers and ultrabooks.",
                CreatedAt = now,
                CreatedBy = "Seeder"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Tablet",
                Description = "Tablets and mobile productivity devices.",
                CreatedAt = now,
                CreatedBy = "Seeder"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Monitor",
                Description = "Display monitors for work and gaming.",
                CreatedAt = now,
                CreatedBy = "Seeder"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Storage",
                Description = "SSD and HDD storage devices.",
                CreatedAt = now,
                CreatedBy = "Seeder"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Input Device",
                Description = "Mice and keyboards.",
                CreatedAt = now,
                CreatedBy = "Seeder"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Networking",
                Description = "Routers and network equipment.",
                CreatedAt = now,
                CreatedBy = "Seeder"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Printer",
                Description = "Printers and printing devices.",
                CreatedAt = now,
                CreatedBy = "Seeder"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Component",
                Description = "Computer hardware components.",
                CreatedAt = now,
                CreatedBy = "Seeder"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Camera",
                Description = "Action cameras and imaging devices.",
                CreatedAt = now,
                CreatedBy = "Seeder"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Audio",
                Description = "Headsets and audio accessories.",
                CreatedAt = now,
                CreatedBy = "Seeder"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Power",
                Description = "Chargers and power accessories.",
                CreatedAt = now,
                CreatedBy = "Seeder"
            }
        };

        var existingNames = await context.Categories
            .Select(c => c.Name)
            .ToHashSetAsync();

        var categoriesToInsert = targetCategories
            .Where(c => !existingNames.Contains(c.Name))
            .ToList();

        if (categoriesToInsert.Count == 0)
        {
            return;
        }

        await context.Categories.AddRangeAsync(categoriesToInsert);
        await context.SaveChangesAsync();
    }
}
