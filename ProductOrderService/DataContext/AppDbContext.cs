using Microsoft.EntityFrameworkCore;
using ProductOrderService.DataContext.Entities;

namespace ProductOrderService.DataContext;

public class AppDbContext : DbContext
{
    public virtual DbSet<OrderEntity> Orders { get; init; }
    public virtual DbSet<OrderProductEntity> OrderProducts { get; init; }
    public virtual DbSet<ProductEntity> Products { get; init; }
    
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
    }
    
    public AppDbContext()
    {
    }
    
    public override int SaveChanges()
    {
        ChangeTracker.DetectChanges();

        var added = ChangeTracker.Entries().Where(w => w.State == EntityState.Added).Select(s => s.Entity).ToList();

        foreach (var entry in added)
        {
            if (entry is not IEntity entity)
            {
                continue;
            }
            
            entity.CreatedDate = DateTime.UtcNow;
            entity.ModificationDate = DateTime.UtcNow;
        }

        var updated = ChangeTracker.Entries().Where(w => w.State == EntityState.Modified).Select(s => s.Entity)
            .ToList();

        foreach (var entry in updated)
        {
            if (entry is IEntity entity)
            {
                entity.ModificationDate = DateTime.UtcNow;
            }
        }

        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        return Task.Run(SaveChanges, cancellationToken);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductEntity>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)"); 
        
        modelBuilder.Entity<OrderProductEntity>()
            .HasKey(op => new { op.OrderId, op.ProductId });

        modelBuilder.Entity<OrderProductEntity>()
            .HasOne(op => op.Order)
            .WithMany(o => o.OrderProducts)
            .HasForeignKey(op => op.OrderId);

        modelBuilder.Entity<OrderProductEntity>()
            .HasOne(op => op.Product)
            .WithMany(p => p.OrderProducts)
            .HasForeignKey(op => op.ProductId);
    }
}