using api.Data;
using Microsoft.EntityFrameworkCore;
using api.Models.Entities;
using System;

namespace api.Data.Seeders;

public static class ProductSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Products.AnyAsync())
        {
            return;
        }

        var categoryIdsByName = await context.Categories
            .AsNoTracking()
            .ToDictionaryAsync(c => c.Name, c => c.Id);

        var products = new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryIdsByName["Laptop"],
                SKU = "IT1001",
                Name = "Dell XPS 13 Laptop",
                Description = "13-inch high performance laptop, Intel Core i7, 16GB RAM, 512GB SSD.",
                Price = 39900m,
                ImageUrl = "https://picsum.photos/seed/it1001/600/400",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder",
            },
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryIdsByName["Laptop"],
                SKU = "IT1002",
                Name = "Apple MacBook Pro 14\"",
                Description = "Apple M2 Pro, 16GB RAM, 1TB SSD, Space Gray.",
                Price = 74900m,
                ImageUrl = "https://picsum.photos/seed/it1002/600/400",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder",
            },
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryIdsByName["Input Device"],
                SKU = "IT1003",
                Name = "Logitech MX Master 3S Mouse",
                Description = "Wireless performance mouse, ergonomic, rechargeable battery.",
                Price = 2790m,
                ImageUrl = "https://picsum.photos/seed/it1003/600/400",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder",
            },
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryIdsByName["Monitor"],
                SKU = "IT1004",
                Name = "Samsung 27\" 4K Monitor",
                Description = "UHD IPS Monitor, USB-C, height adjustable stand.",
                Price = 9900m,
                ImageUrl = "https://picsum.photos/seed/it1004/600/400",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder",
            },
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryIdsByName["Storage"],
                SKU = "IT1005",
                Name = "Kingston NV2 1TB SSD",
                Description = "1TB NVMe PCIe 4.0 SSD, high speed for laptops and PCs.",
                Price = 2550m,
                ImageUrl = "https://picsum.photos/seed/it1005/600/400",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder",
            },
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryIdsByName["Input Device"],
                SKU = "IT1006",
                Name = "Corsair K70 RGB Mechanical Keyboard",
                Description = "Mechanical gaming keyboard with RGB backlighting and Cherry MX switches.",
                Price = 4490m,
                ImageUrl = "https://picsum.photos/seed/it1006/600/400",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder",
            },
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryIdsByName["Laptop"],
                SKU = "IT1007",
                Name = "Microsoft Surface Pro 9",
                Description = "2-in-1 detachable laptop, Intel Evo i5, 16GB RAM, 256GB SSD.",
                Price = 45900m,
                ImageUrl = "https://picsum.photos/seed/it1007/600/400",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder",
            },
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryIdsByName["Laptop"],
                SKU = "IT1008",
                Name = "Acer Aspire 5 Laptop",
                Description = "15.6\" FHD, AMD Ryzen 7, 8GB RAM, 512GB SSD.",
                Price = 15900m,
                ImageUrl = "https://picsum.photos/seed/it1008/600/400",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder",
            },
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryIdsByName["Tablet"],
                SKU = "IT1009",
                Name = "Apple iPad Air 5th Gen",
                Description = "10.9-inch display, M1 chip, 64GB Wi-Fi.",
                Price = 20900m,
                ImageUrl = "https://picsum.photos/seed/it1009/600/400",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder",
            },
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryIdsByName["Networking"],
                SKU = "IT1010",
                Name = "TP-Link Archer AX55 Wi-Fi 6 Router",
                Description = "Dual Band Gigabit, OFDMA, MU-MIMO, HomeShield.",
                Price = 3990m,
                ImageUrl = "https://picsum.photos/seed/it1010/600/400",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder",
            },
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryIdsByName["Input Device"],
                SKU = "IT1011",
                Name = "Razer DeathAdder V2 Mouse",
                Description = "Wired optical gaming mouse, 20K DPI sensor.",
                Price = 1890m,
                ImageUrl = "https://picsum.photos/seed/it1011/600/400",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder",
            },
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryIdsByName["Storage"],
                SKU = "IT1012",
                Name = "Western Digital Blue 4TB HDD",
                Description = "3.5'' SATA Hard Disk, 5400 RPM, 256MB cache.",
                Price = 3990m,
                ImageUrl = "https://picsum.photos/seed/it1012/600/400",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder",
            },
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryIdsByName["Monitor"],
                SKU = "IT1013",
                Name = "BenQ GW2480 24\" IPS Monitor",
                Description = "Full HD, Low Blue Light, Flicker-Free.",
                Price = 4290m,
                ImageUrl = "https://picsum.photos/seed/it1013/600/400",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder",
            },
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryIdsByName["Laptop"],
                SKU = "IT1014",
                Name = "ASUS ROG Strix G15 Gaming Laptop",
                Description = "AMD Ryzen 7, RTX 3060, 16GB RAM, 512GB SSD.",
                Price = 37900m,
                ImageUrl = "https://picsum.photos/seed/it1014/600/400",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder",
            },
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryIdsByName["Printer"],
                SKU = "IT1015",
                Name = "HP DeskJet Ink Advantage 4175",
                Description = "All-in-One Printer, Wi-Fi, ADF.",
                Price = 2690m,
                ImageUrl = "https://picsum.photos/seed/it1015/600/400",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder",
            },
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryIdsByName["Component"],
                SKU = "IT1016",
                Name = "Kingston FURY Beast 16GB DDR4 RAM",
                Description = "3200MHz memory module for desktops.",
                Price = 2290m,
                ImageUrl = "https://picsum.photos/seed/it1016/600/400",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder",
            },
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryIdsByName["Camera"],
                SKU = "IT1017",
                Name = "GoPro HERO11 Black",
                Description = "5.3K60 Ultra HD Waterproof Action Camera.",
                Price = 15300m,
                ImageUrl = "https://picsum.photos/seed/it1017/600/400",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder",
            },
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryIdsByName["Power"],
                SKU = "IT1018",
                Name = "Anker PowerCore 20000mAh Power Bank",
                Description = "Portable charger, high-capacity, fast charging.",
                Price = 1490m,
                ImageUrl = "https://picsum.photos/seed/it1018/600/400",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder",
            },
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryIdsByName["Tablet"],
                SKU = "IT1019",
                Name = "Xiaomi Pad 6",
                Description = "11-inch, 8GB RAM, 128GB, Snapdragon 870, Dolby Atmos.",
                Price = 9990m,
                ImageUrl = "https://picsum.photos/seed/it1019/600/400",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder",
            },
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryIdsByName["Audio"],
                SKU = "IT1020",
                Name = "Jabra Evolve2 65 Wireless Headset",
                Description = "Stereo Bluetooth Headset, with USB-C dongle.",
                Price = 8390m,
                ImageUrl = "https://picsum.photos/seed/it1020/600/400",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder",
            }
        };

        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }
}