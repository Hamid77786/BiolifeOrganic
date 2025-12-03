using BiolifeOrganic.Dll.DataContext.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BiolifeOrganic.Dll.DataContext;

public class AppDbContext: IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<AdminContact> AdminContacts { get; set; }= null!;
    public DbSet<Contact> Contacts { get; set; } = null!;
    public DbSet<Logo> Logos { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
    public DbSet<Organization> Organizations { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductImage> ProductImages { get; set; } = null!;
    public DbSet<Promocode> Promocodes { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<Slider> Sliders { get; set; } = null!;
    public DbSet<WebContact> WebContacts { get; set; } = null!;
    public DbSet<Wishlist> Wishlists { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.Entity<Order>()
        .Property(o => o.TotalAmount)
        .HasColumnType("decimal(18,2)");

        // ShippingContact — CASCADE
        builder.Entity<Order>()
            .HasOne(o => o.ShippingContact)
            .WithMany()
            .HasForeignKey(o => o.ShippingContactId)
            .OnDelete(DeleteBehavior.Cascade);

        // BillingContact — NO ACTION
        builder.Entity<Order>()
            .HasOne(o => o.BillingContact)
            .WithMany()
            .HasForeignKey(o => o.BillingContactId)
            .OnDelete(DeleteBehavior.NoAction);

        // Organization — NO ACTION
        builder.Entity<Order>()
            .HasOne(o => o.Organization)
            .WithMany()
            .HasForeignKey(o => o.OrganizationId)
            .OnDelete(DeleteBehavior.NoAction);


        builder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Product>()
           .Property(p => p.OriginalPrice)
           .HasPrecision(18, 2);

        builder.Entity<ProductImage>()
            .HasOne(pi => pi.Product)
            .WithMany(p => p.ProductImages)
            .HasForeignKey(pi => pi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Promocode>()
            .Property(pc => pc.Discount)
            .HasPrecision(5, 2);

        builder.Entity<Promocode>()
            .HasOne(pc => pc.AppUser)
            .WithMany(u => u.Promocodes)
            .HasForeignKey(pc => pc.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Contact>()
            .HasOne(c => c.AppUser)
            .WithMany(u => u.Contacts)
            .HasForeignKey(c => c.AppUserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<Review>()
             .HasOne(r => r.AppUser)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.AppUserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<Wishlist>()
            .HasOne(w => w.AppUser)
            .WithMany(u => u.Wishlists)
            .HasForeignKey(w => w.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);





        base.OnModelCreating(builder);
    }

        
        
    
}
