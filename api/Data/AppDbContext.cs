using api.Models;
using api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Inventory> Inventories => Set<Inventory>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<CustomerAddress> CustomerAddresses => Set<CustomerAddress>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureTables(modelBuilder);
        ConfigurePrimaryKeys(modelBuilder);
        ConfigureForeignKeys(modelBuilder);
        ConfigureIndexesAndConstraints(modelBuilder);
    }

    private static void ConfigureTables(ModelBuilder builder)
    {
        builder.Entity<User>().ToTable("Users");
        builder.Entity<Product>().ToTable("Products");
        builder.Entity<Inventory>().ToTable("Inventories");
        builder.Entity<Category>().ToTable("Categories");
        builder.Entity<Customer>().ToTable("Customers");
        builder.Entity<CustomerAddress>().ToTable("CustomerAddresses");
        builder.Entity<Order>().ToTable("Orders", t =>
            t.HasCheckConstraint("CK_Orders_Status",
                $"Status IN ('{AppConstant.OrderStatus.PENDING}','{AppConstant.OrderStatus.PROCESSING}','{AppConstant.OrderStatus.COMPLETED}','{AppConstant.OrderStatus.CANCELLED}')"));
        builder.Entity<OrderItem>().ToTable("OrderItems");
    }

    private static void ConfigurePrimaryKeys(ModelBuilder builder)
    {
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<Product>().HasKey(p => p.Id);
        builder.Entity<Inventory>().HasKey(i => i.Id);
        builder.Entity<Category>().HasKey(c => c.Id);
        builder.Entity<Customer>().HasKey(c => c.Id);
        builder.Entity<CustomerAddress>().HasKey(ca => ca.Id);
        builder.Entity<Order>().HasKey(o => o.Id);
        builder.Entity<OrderItem>().HasKey(oi => oi.Id);
    }

    private static void ConfigureForeignKeys(ModelBuilder builder)
    {
        builder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Inventory>() 
            .HasOne(i => i.Product)
            .WithMany()
            .HasForeignKey(i => i.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<CustomerAddress>()
            .HasOne(ca => ca.Customer)
            .WithMany()
            .HasForeignKey(ca => ca.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany()
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigureIndexesAndConstraints(ModelBuilder builder)
    {
        builder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        builder.Entity<Customer>().HasIndex(c => c.Email).IsUnique();
        builder.Entity<Product>().HasIndex(p => p.SKU).IsUnique();
        builder.Entity<Inventory>().HasIndex(i => i.ProductId).IsUnique();

        builder.Entity<Order>().HasIndex(o => o.Status);

        builder.Entity<OrderItem>()
            .HasIndex(oi => new { oi.OrderId, oi.ProductId })
            .IsUnique()
            .HasDatabaseName("IX_OrderItems_OrderId_ProductId");

    }
}