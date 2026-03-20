using api.Data;
using api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Data.Seeders;

public static class CustomerAddressSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var customer = await context.Customers
            .FirstOrDefaultAsync(c => c.Email == "customer1@email.com");
        if (customer == null)
        {
            return;
        }

        var addresses = new List<CustomerAddress>
        {
            new()
            {
                Id = Guid.NewGuid(),
                CustomerId = customer.Id,
                Customer = null!,
                Address = "123 ถนนสุขุมวิท",
                SubDistrict = "คลองเตยเหนือ",
                District = "วัฒนา",
                Province = "กรุงเทพมหานคร",
                PostalCode = "10110",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder",
            },
            new()
            {
                Id = Guid.NewGuid(),
                CustomerId = customer.Id,
                Customer = null!,
                Address = "88 ถนนนิมมานเหมินท์",
                SubDistrict = "สุเทพ",
                District = "เมืองเชียงใหม่",
                Province = "เชียงใหม่",
                PostalCode = "50200",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder",
            },
            new()
            {
                Id = Guid.NewGuid(),
                CustomerId = customer.Id,
                Customer = null!,
                Address = "45 ถนนพัทยาใต้",
                SubDistrict = "หนองปรือ",
                District = "บางละมุง",
                Province = "ชลบุรี",
                PostalCode = "20150",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder",
            },
        };

        var existingAddresses = await context.CustomerAddresses
            .Where(a => a.CustomerId == customer.Id)
            .Select(a => a.Address)
            .ToHashSetAsync();

        var newAddresses = addresses
            .Where(a => !existingAddresses.Contains(a.Address))
            .ToList();

        if (newAddresses.Count == 0)
        {
            return;
        }

        await context.CustomerAddresses.AddRangeAsync(newAddresses);
        await context.SaveChangesAsync();
    }
}
