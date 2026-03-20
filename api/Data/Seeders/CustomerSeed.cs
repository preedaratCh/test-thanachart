using api.Data;
using api.Helpers;
using api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Data.Seeders;

public static class CustomerSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        Guid customerId = Guid.Parse("b8717653-c981-414a-a4d7-e3a2e677e321");
        const string customerEmail = "customer1@email.com";

        var hasCustomer = await context.Customers.AnyAsync(c => c.Email == customerEmail && c.Id == customerId);
        if (hasCustomer)
        {
            await context.Customers.Where(c => c.Id == customerId).ExecuteDeleteAsync();
            await context.SaveChangesAsync();
            return;
        }

        await context.Customers.AddAsync(new Customer
        {
            Id = customerId,
            Name = "Customer One",
            Email = customerEmail,
            Password = AppHelper.HashPassword("password"),
            Phone = "0812345678",
            CreatedAt = DateTime.UtcNow,
        });

        await context.SaveChangesAsync();
    }
}
