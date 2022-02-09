using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PizzaREAL.Models
{
    public partial class PizzaDbContext : DbContext
    {
        public PizzaDbContext()
        {
        }

        public PizzaDbContext(DbContextOptions<PizzaDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Dish> Dishes { get; set; } = null!;
        public virtual DbSet<DishType> DishTypes { get; set; } = null!;
        public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDish> OrderDishes { get; set; } = null!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.AspNetUserId).HasMaxLength(450);
            });

            modelBuilder.Entity<Dish>(entity =>
            {
                entity.ToTable("Dish");

                entity.HasIndex(e => e.DishTypeId, "IX_Dish_DishTypeId");

                entity.Property(e => e.DishId).HasColumnName("DishID");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DishName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.DishType)
                    .WithMany(p => p.Dishes)
                    .HasForeignKey(d => d.DishTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dish_DishType");

                entity.HasMany(d => d.Ingredients)
                    .WithMany(p => p.Dishes)
                    .UsingEntity<Dictionary<string, object>>(
                        "DishIngredient",
                        l => l.HasOne<Ingredient>().WithMany().HasForeignKey("IngredientId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_DishIngredient_Ingredient"),
                        r => r.HasOne<Dish>().WithMany().HasForeignKey("DishId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_DishIngredient_Dish"),
                        j =>
                        {
                            j.HasKey("DishId", "IngredientId");

                            j.ToTable("DishIngredient");

                            j.HasIndex(new[] { "IngredientId" }, "IX_DishIngredient_IngredientID");

                            j.IndexerProperty<int>("DishId").HasColumnName("DishID");

                            j.IndexerProperty<int>("IngredientId").HasColumnName("IngredientID");
                        });
            });

            modelBuilder.Entity<DishType>(entity =>
            {
                entity.ToTable("DishType");

                entity.Property(e => e.DishTypeId).HasColumnName("DishTypeID");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("Ingredient");

                entity.Property(e => e.IngredientId).HasColumnName("IngredientID");

                entity.Property(e => e.IngredientName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.HasIndex(e => e.CustomerId, "IX_Order_CustomerID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Customer");
            });

            modelBuilder.Entity<OrderDish>(entity =>
            {
                entity.HasKey(e => new { e.DishId, e.OrderId });

                entity.ToTable("OrderDish");

                entity.HasIndex(e => e.OrderId, "IX_OrderDish_OrderID");

                entity.Property(e => e.DishId).HasColumnName("DishID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Amount).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.OrderDishes)
                    .HasForeignKey(d => d.DishId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDish_Dish");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDishes)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDish_Order");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
