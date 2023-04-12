using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Test.Models
{
    public partial class tradeContext : DbContext
    {
        private static tradeContext _context;
        public tradeContext()
        {
        }

        public static tradeContext GetContext() { return _context ?? (_context = new tradeContext()); }

        public tradeContext(DbContextOptions<tradeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Manufacturer> Manufacturer { get; set; }
        public virtual DbSet<Orderproduct> Orderproduct { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Orderstatus> Orderstatus { get; set; }
        public virtual DbSet<Point> Point { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Producttype> Producttype { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Suplier> Suplier { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;user=root;password=1111;database=tradedemo", x => x.ServerVersion("8.0.32-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.HasKey(e => e.Idmanufacturer)
                    .HasName("PRIMARY");

                entity.ToTable("manufacturer");

                entity.Property(e => e.Idmanufacturer).HasColumnName("idmanufacturer");

                entity.Property(e => e.Manufacname)
                    .HasColumnName("manufacname")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Orderproduct>(entity =>
            {
                entity.HasKey(e => e.Idorderproduct)
                    .HasName("PRIMARY");

                entity.ToTable("orderproduct");

                entity.HasIndex(e => e.OrderId)
                    .HasName("g_idx");

                entity.HasIndex(e => e.ProductArticleNumber)
                    .HasName("h_idx");

                entity.Property(e => e.Idorderproduct).HasColumnName("idorderproduct");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductArticleNumber)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8mb3_general_ci");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Orderproduct)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("g");

                entity.HasOne(d => d.ProductArticleNumberNavigation)
                    .WithMany(p => p.Orderproduct)
                    .HasForeignKey(d => d.ProductArticleNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("h");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PRIMARY");

                entity.ToTable("orders");

                entity.HasIndex(e => e.Idorderstatus)
                    .HasName("d_idx");

                entity.HasIndex(e => e.Idpoint)
                    .HasName("f_idx");

                entity.HasIndex(e => e.Iduser)
                    .HasName("e_idx");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Idorderstatus).HasColumnName("idorderstatus");

                entity.Property(e => e.Idpoint).HasColumnName("idpoint");

                entity.Property(e => e.Iduser).HasColumnName("iduser");

                entity.Property(e => e.OrderDeliveryDate).HasColumnType("date");

                entity.Property(e => e.OrderShipDate).HasColumnType("date");

                entity.HasOne(d => d.IdorderstatusNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Idorderstatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("d");

                entity.HasOne(d => d.IdpointNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Idpoint)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("f");

                entity.HasOne(d => d.IduserNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Iduser)
                    .HasConstraintName("e");
            });

            modelBuilder.Entity<Orderstatus>(entity =>
            {
                entity.HasKey(e => e.Idorderstatus)
                    .HasName("PRIMARY");

                entity.ToTable("orderstatus");

                entity.Property(e => e.Idorderstatus).HasColumnName("idorderstatus");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Point>(entity =>
            {
                entity.HasKey(e => e.Idpoint)
                    .HasName("PRIMARY");

                entity.ToTable("point");

                entity.Property(e => e.Idpoint).HasColumnName("idpoint");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Index).HasColumnName("index");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductArticleNumber)
                    .HasName("PRIMARY");

                entity.ToTable("product");

                entity.HasIndex(e => e.Idmanufacturer)
                    .HasName("a_idx");

                entity.HasIndex(e => e.Idproducttype)
                    .HasName("b_idx");

                entity.HasIndex(e => e.Idsupplier)
                    .HasName("c_idx");

                entity.Property(e => e.ProductArticleNumber)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8mb3_general_ci");

                entity.Property(e => e.Idmanufacturer).HasColumnName("idmanufacturer");

                entity.Property(e => e.Idproducttype).HasColumnName("idproducttype");

                entity.Property(e => e.Idsupplier).HasColumnName("idsupplier");

                entity.Property(e => e.ProductCost).HasColumnType("decimal(19,2)");

                entity.Property(e => e.ProductDescription)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ProductMeasure)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ProductPhoto)
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ProductStatus)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.IdmanufacturerNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.Idmanufacturer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("a");

                entity.HasOne(d => d.IdproducttypeNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.Idproducttype)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("b");

                entity.HasOne(d => d.IdsupplierNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.Idsupplier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("c");
            });

            modelBuilder.Entity<Producttype>(entity =>
            {
                entity.HasKey(e => e.Idproducttype)
                    .HasName("PRIMARY");

                entity.ToTable("producttype");

                entity.Property(e => e.Idproducttype).HasColumnName("idproducttype");

                entity.Property(e => e.Producttypename)
                    .HasColumnName("producttypename")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Suplier>(entity =>
            {
                entity.HasKey(e => e.Idsuplier)
                    .HasName("PRIMARY");

                entity.ToTable("suplier");

                entity.Property(e => e.Idsuplier).HasColumnName("idsuplier");

                entity.Property(e => e.Suppliername)
                    .HasColumnName("suppliername")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.UserRole)
                    .HasName("UserRole");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserLogin)
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UserPassword)
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UserPatronymic)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UserSurname)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.UserRoleNavigation)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.UserRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
