using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SelfServiceVSC.DAL;

namespace SelfServiceVSC.DAL;

public partial class BestpriceprotectionContext : DbContext
{
    public BestpriceprotectionContext()
    {
    }

    public BestpriceprotectionContext(DbContextOptions<BestpriceprotectionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomersSwitched> CustomersSwitched { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       // => optionsBuilder.UseSqlServer("Server=1055-0000\\SQLEXPRESS;Database=bestpriceprotection;User Id=sa;Password=aloha@123;TrustServerCertificate=True;");
   //=> optionsBuilder.UseSqlServer("Server=bestpriceprotection.c3k4u2e2609t.us-east-2.rds.amazonaws.com\\SQLEXPRESS;Database=bestpriceprotection;User Id=admin;Password=Aloha!123456;TrustServerCertificate=True;");
   => optionsBuilder.UseSqlServer("Server=bestpriceprotection.c3k4u2e2609t.us-east-2.rds.amazonaws.com;Database=bestpriceprotection;User Id=admin;Password=Aloha!123456;TrustServerCertificate=True;");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.City)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Coverage)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Email)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Street1)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Street2)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Vin)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("VIN");
            entity.Property(e => e.Zip)
                .HasMaxLength(9)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CustomersSwitched>(entity =>
        {
            entity.ToTable("Customers_Switched");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.City)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Coverage)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Email)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Street1)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Street2)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Vin)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("VIN");
            entity.Property(e => e.Zip)
                .HasMaxLength(9)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
