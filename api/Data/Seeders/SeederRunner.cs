using api.Data;
using Microsoft.EntityFrameworkCore;

namespace api.Data.Seeders;

public static class SeederRunner
{
    public static async Task SeedAsync(AppDbContext db)
    {
        await db.Database.MigrateAsync();

        await UserSeed.SeedAsync(db);
        await CategorySeed.SeedAsync(db);
        await ProductSeed.SeedAsync(db);
        await InventorySeed.SeedAsync(db);
        await CustomerSeed.SeedAsync(db);
        await CustomerAddressSeed.SeedAsync(db);
    }

    public static async Task ReseedAsync(AppDbContext db)
    {
        await db.Database.EnsureDeletedAsync();
        await db.Database.MigrateAsync();

        await UserSeed.SeedAsync(db);
        await CategorySeed.SeedAsync(db);
        await ProductSeed.SeedAsync(db);
        await InventorySeed.SeedAsync(db);
        await CustomerSeed.SeedAsync(db);
        await CustomerAddressSeed.SeedAsync(db);
    }
}
