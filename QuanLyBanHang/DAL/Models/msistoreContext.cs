﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL.Models
{
    public partial class msistoreContext : DbContext
    {
        public msistoreContext()
        {
        }

        public msistoreContext(DbContextOptions<msistoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Brand> Brands { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Orderitem> Orderitems { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Statusorder> Statusorders { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Userinfo> Userinfos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=ADMIN-PC;Initial Catalog=msistore;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("brand", "msistoredb");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category", "msistoredb");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("image", "msistoredb");

                entity.HasIndex(e => e.ProductId, "msistore_image_product_id_20c1b923_fk_msistore_product_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.File)
                    .HasMaxLength(100)
                    .HasColumnName("file");

                entity.Property(e => e.Preview).HasColumnName("preview");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("image$msistore_image_product_id_20c1b923_fk_msistore_product_id");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order", "msistoredb");

                entity.HasIndex(e => e.UserId, "msistore_order_user_id_e1bb818f_fk_msistore_userinfo_user_id");

                entity.HasIndex(e => e.Uuid, "order$uuid")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("createdAt");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("date")
                    .HasColumnName("updatedAt");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(32)
                    .HasColumnName("uuid")
                    .IsFixedLength();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("order$msistore_order_user_id_e1bb818f_fk_msistore_userinfo_user_id");
            });

            modelBuilder.Entity<Orderitem>(entity =>
            {
                entity.ToTable("orderitem", "msistoredb");

                entity.HasIndex(e => e.OrderId, "msistore_orderitem_order_id_fd7cd6f3_fk_msistore_order_id");

                entity.HasIndex(e => e.ProductId, "msistore_orderitem_product_id_98f2f5c8_fk_msistore_product_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity)
                    .HasMaxLength(3)
                    .HasColumnName("quantity");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Orderitems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orderitem$msistore_orderitem_order_id_fd7cd6f3_fk_msistore_order_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orderitems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orderitem$msistore_orderitem_product_id_98f2f5c8_fk_msistore_product_id");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product", "msistoredb");

                entity.HasIndex(e => e.BrandId, "msistore_product_brand_id_31330f4e_fk_msistore_brand_id");

                entity.HasIndex(e => e.CategoryId, "msistore_product_category_id_fbae0197_fk_msistore_category_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BrandId).HasColumnName("brand_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("createdAt");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("description");

                entity.Property(e => e.Detail).HasColumnName("detail");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.NewPrice)
                    .HasColumnType("decimal(6, 2)")
                    .HasColumnName("newPrice");

                entity.Property(e => e.OldPrice)
                    .HasColumnType("decimal(6, 2)")
                    .HasColumnName("oldPrice");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("date")
                    .HasColumnName("updatedAt");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("product$msistore_product_brand_id_31330f4e_fk_msistore_brand_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product$msistore_product_category_id_fbae0197_fk_msistore_category_id");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role", "msistoredb");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Statusorder>(entity =>
            {
                entity.ToTable("statusorder", "msistoredb");

                entity.HasIndex(e => e.OrderId, "msistore_statusorder_order_id_2a6131fd_fk_msistore_order_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("createdAt");

                entity.Property(e => e.DeliveryMethod)
                    .HasMaxLength(50)
                    .HasColumnName("deliveryMethod");

                entity.Property(e => e.DeliveryStage)
                    .HasMaxLength(50)
                    .HasColumnName("deliveryStage");

                entity.Property(e => e.IsAaid).HasColumnName("isAaid");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(50)
                    .HasColumnName("paymentMethod");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("date")
                    .HasColumnName("updatedAt");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Statusorders)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("statusorder$msistore_statusorder_order_id_2a6131fd_fk_msistore_order_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user", "msistoredb");

                entity.HasIndex(e => e.RoleId, "msistore_user_role_id_ebd2668b_fk_msistore_role_id");

                entity.HasIndex(e => e.Username, "user$username")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(100)
                    .HasColumnName("avatar");

                entity.Property(e => e.DateJoined)
                    .HasPrecision(6)
                    .HasColumnName("dateJoined");

                entity.Property(e => e.Email)
                    .HasMaxLength(254)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(150)
                    .HasColumnName("firstName");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsStaff).HasColumnName("isStaff");

                entity.Property(e => e.IsSuperuser).HasColumnName("isSuperuser");

                entity.Property(e => e.LastLogin)
                    .HasPrecision(6)
                    .HasColumnName("lastLogin");

                entity.Property(e => e.LastName)
                    .HasMaxLength(150)
                    .HasColumnName("lastName");

                entity.Property(e => e.Password)
                    .HasMaxLength(128)
                    .HasColumnName("password");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Username)
                    .HasMaxLength(150)
                    .HasColumnName("username");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user$msistore_user_role_id_ebd2668b_fk_msistore_role_id");
            });

            modelBuilder.Entity<Userinfo>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_userinfo_user_id");

                entity.ToTable("userinfo", "msistoredb");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("user_id");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .HasColumnName("city");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .HasColumnName("country");

                entity.Property(e => e.HomeNumber)
                    .HasMaxLength(50)
                    .HasColumnName("homeNumber");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.Street)
                    .HasMaxLength(50)
                    .HasColumnName("street");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Userinfo)
                    .HasForeignKey<Userinfo>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userinfo$msistore_userinfo_user_id_aec5a4e8_fk_msistore_user_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
