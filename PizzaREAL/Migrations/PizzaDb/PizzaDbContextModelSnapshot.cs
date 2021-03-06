// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PizzaREAL.Models;

#nullable disable

namespace PizzaREAL.Migrations.PizzaDb
{
    [DbContext(typeof(PizzaDbContext))]
    partial class PizzaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DishIngredient", b =>
                {
                    b.Property<int>("DishId")
                        .HasColumnType("int")
                        .HasColumnName("DishID");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int")
                        .HasColumnName("IngredientID");

                    b.HasKey("DishId", "IngredientId");

                    b.HasIndex(new[] { "IngredientId" }, "IX_DishIngredient_IngredientID");

                    b.ToTable("DishIngredient", (string)null);
                });

            modelBuilder.Entity("PizzaREAL.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"), 1L, 1);

                    b.Property<string>("AspNetUserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("BonusPoints")
                        .HasColumnType("int");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer", (string)null);
                });

            modelBuilder.Entity("PizzaREAL.Models.Dish", b =>
                {
                    b.Property<int>("DishId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DishID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DishId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("DishName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("DishTypeId")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("DishId");

                    b.HasIndex(new[] { "DishTypeId" }, "IX_Dish_DishTypeId");

                    b.ToTable("Dish", (string)null);
                });

            modelBuilder.Entity("PizzaREAL.Models.DishType", b =>
                {
                    b.Property<int>("DishTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DishTypeID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DishTypeId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("DishTypeId");

                    b.ToTable("DishType", (string)null);
                });

            modelBuilder.Entity("PizzaREAL.Models.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("IngredientID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IngredientId"), 1L, 1);

                    b.Property<string>("IngredientName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("IngredientId");

                    b.ToTable("Ingredient", (string)null);
                });

            modelBuilder.Entity("PizzaREAL.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    b.Property<bool>("Delivered")
                        .HasColumnType("bit");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime");

                    b.Property<int>("TotalSum")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex(new[] { "CustomerId" }, "IX_Order_CustomerID");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("PizzaREAL.Models.OrderDish", b =>
                {
                    b.Property<int>("DishId")
                        .HasColumnType("int")
                        .HasColumnName("DishID");

                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    b.Property<int>("Amount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((1))");

                    b.HasKey("DishId", "OrderId");

                    b.HasIndex(new[] { "OrderId" }, "IX_OrderDish_OrderID");

                    b.ToTable("OrderDish", (string)null);
                });

            modelBuilder.Entity("DishIngredient", b =>
                {
                    b.HasOne("PizzaREAL.Models.Dish", null)
                        .WithMany()
                        .HasForeignKey("DishId")
                        .IsRequired()
                        .HasConstraintName("FK_DishIngredient_Dish");

                    b.HasOne("PizzaREAL.Models.Ingredient", null)
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .IsRequired()
                        .HasConstraintName("FK_DishIngredient_Ingredient");
                });

            modelBuilder.Entity("PizzaREAL.Models.Dish", b =>
                {
                    b.HasOne("PizzaREAL.Models.DishType", "DishType")
                        .WithMany("Dishes")
                        .HasForeignKey("DishTypeId")
                        .IsRequired()
                        .HasConstraintName("FK_Dish_DishType");

                    b.Navigation("DishType");
                });

            modelBuilder.Entity("PizzaREAL.Models.Order", b =>
                {
                    b.HasOne("PizzaREAL.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("FK_Order_Customer");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("PizzaREAL.Models.OrderDish", b =>
                {
                    b.HasOne("PizzaREAL.Models.Dish", "Dish")
                        .WithMany("OrderDishes")
                        .HasForeignKey("DishId")
                        .IsRequired()
                        .HasConstraintName("FK_OrderDish_Dish");

                    b.HasOne("PizzaREAL.Models.Order", "Order")
                        .WithMany("OrderDishes")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK_OrderDish_Order");

                    b.Navigation("Dish");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("PizzaREAL.Models.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("PizzaREAL.Models.Dish", b =>
                {
                    b.Navigation("OrderDishes");
                });

            modelBuilder.Entity("PizzaREAL.Models.DishType", b =>
                {
                    b.Navigation("Dishes");
                });

            modelBuilder.Entity("PizzaREAL.Models.Order", b =>
                {
                    b.Navigation("OrderDishes");
                });
#pragma warning restore 612, 618
        }
    }
}
