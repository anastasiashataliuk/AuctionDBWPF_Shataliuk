using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DALEF.Models
{
    public partial class AuctionDBContext : DbContext
    {
        public AuctionDBContext()
        {
        }

        public AuctionDBContext(DbContextOptions<AuctionDBContext> options)
            : base(options)
        {
        }

        public AuctionDBContext(string connectionString)
        {
        }

        public virtual DbSet<TblAuction> Auction { get; set; }
        public virtual DbSet<TblProduct> Product { get; set; }
        public virtual DbSet<TblAuctionProduct> AuctionProduct { get; set; }
        public virtual DbSet<TblUser> Users { get; set; } // Add DbSet for Users

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Data Source=DESKTOP-7L66KNT\\SQLEXPRESS;Initial Catalog=AuctionDB;Integrated Security=True;TrustServerCertificate=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAuction>(entity =>
            {
                entity.HasKey(e => e.Auction_Id);
                entity.ToTable("tblAuction");

                entity.Property(e => e.Starting_Price).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.Buyout_Price).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.Status).HasMaxLength(20);
            });

            modelBuilder.Entity<TblProduct>(entity =>
            {
                entity.HasKey(e => e.Product_Id);
                entity.ToTable("tblProduct");

                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Category).HasMaxLength(100);
            });

            modelBuilder.Entity<TblAuctionProduct>(entity =>
            {
                entity.HasKey(e => new { e.Auction_Id, e.Product_Id });

                entity.Property(e => e.Auction_Id).HasColumnName("Auction_Id");
                entity.Property(e => e.Product_Id).HasColumnName("Product_Id");

                entity.HasOne(d => d.Auction)
                    .WithMany(p => p.AuctionProduct)
                    .HasForeignKey(d => d.Auction_Id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_AuctionProduct_Auction");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.AuctionProduct)
                    .HasForeignKey(d => d.Product_Id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_AuctionProduct_Product");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.User_Id); // Define primary key
                entity.ToTable("tblUser"); // Map to the database table

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256); // Ensure sufficient length for hashed passwords

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(50); // E.g., Admin, User, etc.
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
