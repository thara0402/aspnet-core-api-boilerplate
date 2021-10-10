using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApplication.Infrastructure.Sql.Models
{
    public partial class ShopContext : DbContext
    {
        public ShopContext()
        {
        }

        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Basket> Baskets { get; set; }
        public virtual DbSet<BasketItem> BasketItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Basket>(entity =>
            {
                entity.ToTable("Basket");

                entity.Property(e => e.BasketId).HasColumnName("BasketID");

                entity.Property(e => e.LastUpdate).HasColumnType("datetime");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("TS");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("UserID");
            });

            modelBuilder.Entity<BasketItem>(entity =>
            {
                entity.ToTable("BasketItem");

                entity.Property(e => e.BasketItemId).HasColumnName("BasketItemID");

                entity.Property(e => e.BasketId).HasColumnName("BasketID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("TS");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(28, 6)");

                entity.HasOne(d => d.Basket)
                    .WithMany(p => p.BasketItems)
                    .HasForeignKey(d => d.BasketId)
                    .HasConstraintName("FK_BasketItem_Basket_Shop_Basket");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.BasketItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BasketItem_Product_Shop_Product");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.LastUpdate).HasColumnType("datetime");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("TS");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("UserID");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("OrderItem");

                entity.Property(e => e.OrderItemId).HasColumnName("OrderItemID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("TS");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(28, 6)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItem_Order_Shop_Order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItem_Product_Shop_Product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Desc)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Price).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("TS");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
