using System;
using CoreAPISample.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoreAPISample.Core.Common
{
    public partial class CoreAPISampleDBContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public static string ConnectionString;
        public CoreAPISampleDBContext()
        {
        }

        public CoreAPISampleDBContext(DbContextOptions<CoreAPISampleDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Delivery> Delivery { get; set; }
        public virtual DbSet<DeliveryDetails> DeliveryDetails { get; set; }
        public virtual DbSet<DeliveryItems> DeliveryItems { get; set; }
        public virtual DbSet<DeliverySerializeDetails> DeliverySerializeDetails { get; set; }
        public virtual DbSet<Items> Items { get; set; }
        public virtual DbSet<Manufacturer> Manufacturer { get; set; }
        public virtual DbSet<ManufacturerItems> ManufacturerItems { get; set; }
        public virtual DbSet<MaterialItems> MaterialItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.Property(e => e.DeliveryId).HasColumnName("DeliveryID");

                entity.Property(e => e.DeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<DeliveryDetails>(entity =>
            {
                entity.Property(e => e.DeliveryDetailsId).HasColumnName("DeliveryDetailsID");

                entity.Property(e => e.DeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.DeliveryId).HasColumnName("DeliveryID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Delivery)
                    .WithMany(p => p.DeliveryDetails)
                    .HasForeignKey(d => d.DeliveryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeliveryDetails_Delivery");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.DeliveryDetails)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeliveryDetails_Items");
            });

            modelBuilder.Entity<DeliveryItems>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DeliveryId).HasColumnName("DeliveryID");

                entity.Property(e => e.MaterialItemId).HasColumnName("MaterialItemID");

                entity.HasOne(d => d.MaterialItem)
                    .WithMany(p => p.DeliveryItems)
                    .HasForeignKey(d => d.MaterialItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeliveryItems_MaterialItems");
            });

            modelBuilder.Entity<DeliverySerializeDetails>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeliveryId).HasColumnName("DeliveryID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.Qty).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SerialNumber).HasMaxLength(50);

                entity.HasOne(d => d.Delivery)
                    .WithMany(p => p.DeliverySerializeDetails)
                    .HasForeignKey(d => d.DeliveryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeliverySerializeDetails_Delivery");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.DeliverySerializeDetails)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeliverySerializeDetails_Items");
            });

            modelBuilder.Entity<Items>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.SerialNumber).HasMaxLength(50);

                entity.HasOne(d => d.Manufacturer)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.ManufacturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Items_Manufacturer");
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DeliveryReceivedDate).HasColumnType("datetime");

                entity.Property(e => e.ManufacturerName).HasMaxLength(50);
            });

            modelBuilder.Entity<ManufacturerItems>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");

                entity.Property(e => e.MaterialItemId).HasColumnName("MaterialItemID");

                entity.HasOne(d => d.MaterialItem)
                    .WithMany(p => p.ManufacturerItems)
                    .HasForeignKey(d => d.MaterialItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ManufacturerItems_MaterialItems");
            });

            modelBuilder.Entity<MaterialItems>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ForecastDate).HasColumnType("datetime");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SerialNumber).HasMaxLength(50);
            });
        }
    }
}
