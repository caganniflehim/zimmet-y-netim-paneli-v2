using envanter.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Yeni eklendi
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace envanter.Models;

// IdentityDbContext<IdentityUser> kullanarak kullanıcı tablolarını da dahil ediyoruz
public partial class EnvanterDbContext : IdentityDbContext<ApplicationUser>
{
    public EnvanterDbContext()
    {
    }

    public EnvanterDbContext(DbContextOptions<EnvanterDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cihazlar> Cihazlars { get; set; }

    public virtual DbSet<Personel> Personels { get; set; }

    public virtual DbSet<Zimmet> Zimmets { get; set; }

   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ÖNEMLİ: Identity tablolarının veritabanında düzgün oluşabilmesi için base çağrısı en başta olmalıdır!
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cihazlar>(entity =>
        {
            entity.HasKey(e => e.CihazId).HasName("PK__cihazlar__B560B487DCBEF0DE");

            entity.ToTable("cihazlar");

            entity.HasIndex(e => e.SeriNo, "UQ__cihazlar__1A24D142C838711C").IsUnique();

            entity.Property(e => e.CihazId).HasColumnName("CihazID");
            entity.Property(e => e.Durum)
                .HasMaxLength(20)
                .HasDefaultValue("depoda");
            entity.Property(e => e.Kategori).HasMaxLength(200);
            entity.Property(e => e.MarkaModel).HasMaxLength(100);
            entity.Property(e => e.SeriNo).HasMaxLength(100);
        });

        modelBuilder.Entity<Personel>(entity =>
        {
            entity.HasKey(e => e.PersonelId).HasName("PK__Personel__0F0C5751F34836ED");

            entity.ToTable("Personel");

            entity.Property(e => e.PersonelId).HasColumnName("PersonelID");
            entity.Property(e => e.AdSoyad).HasMaxLength(50);
            entity.Property(e => e.Departman).HasMaxLength(50);
            entity.Property(e => e.Durum)
                .HasMaxLength(20)
                .HasDefaultValue("Aktif");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.KayitTarihi)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Zimmet>(entity =>
        {
            entity.HasKey(e => e.ZimmetId).HasName("PK__zimmet__B89A48E1BAEE53AF");

            entity.ToTable("zimmet");

            entity.Property(e => e.ZimmetId).HasColumnName("ZimmetID");
            entity.Property(e => e.CihazId).HasColumnName("CihazID");
            entity.Property(e => e.PersonelId).HasColumnName("PersonelID");

            entity.HasOne(d => d.Cihaz).WithMany(p => p.Zimmets)
                .HasForeignKey(d => d.CihazId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__zimmet__CihazID__0B91BA14");

            entity.HasOne(d => d.Personel).WithMany(p => p.Zimmets)
                .HasForeignKey(d => d.PersonelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__zimmet__Personel__0C85DE4D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}