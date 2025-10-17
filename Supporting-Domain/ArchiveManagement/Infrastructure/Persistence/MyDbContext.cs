using System;
using System.Collections.Generic;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ArchiveItem> ArchiveItems { get; set; }

    public virtual DbSet<ArchiveItemSetting> ArchiveItemSettings { get; set; }

    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<DefaultArchive> DefaultArchives { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArchiveItem>(entity =>
        {
            entity.ToTable("ArchiveItem", tb => tb.HasComment("مشخصات فایل یا فولدر"));

            entity.Property(e => e.CategoryId).HasComment("دسته بندی");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FileExtension)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FileMimeType)
                .HasMaxLength(2048)
                .IsUnicode(false);
            entity.Property(e => e.IsZiped).HasComment("فایل فشرده شده است یا نه");
            entity.Property(e => e.MinIoUrl)
                .HasMaxLength(2048)
                .HasColumnName("MinIO_URL");
            entity.Property(e => e.ParentId).HasComment("شناسه آرشیو پدر که در اصل میشود پوشه ای که این آرشیو داخل آن قرار دارد");
            entity.Property(e => e.Size).HasComment("اندازه فایل بر حسب بایت");
            entity.Property(e => e.Title)
                .HasMaxLength(1024)
                .HasComment("عنوان فایل");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('none')")
                .HasComment("نوع آرشیو: file یا folder");

            entity.HasOne(d => d.Category).WithMany(p => p.ArchiveItems)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_ArchiveItem_Category");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_ArchiveItem_ArchiveItem");
        });

        modelBuilder.Entity<ArchiveItemSetting>(entity =>
        {
            entity.ToTable(tb => tb.HasComment("تنظیمات فایلها"));

            entity.Property(e => e.FileExtension)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("پسوند فایل بدون نقطه. مثلا png, jpg, pdf");
            entity.Property(e => e.Icon)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasDefaultValueSql("('<i></i>')")
                .HasComment("آیکن معادل هر پسوند در قالب تگ html");
            entity.Property(e => e.MaxUploadLimit).HasComment("حداکثر حجم قابل قبول برای هر نوع فایل-اگر null باشد یعنی محدودیتی نداریم.");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category", tb => tb.HasComment("دسته بندی فایل ها"));

            entity.Property(e => e.Active).HasDefaultValueSql("((1))");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(1024);
            entity.Property(e => e.MaxCapacity).HasComment("ظرفیت حجمی هر دسته بندی بر حسب بایت");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<DefaultArchive>(entity => {
            entity.ToTable("DefaultArchive", tb => tb.HasComment("آرشیو پیشفرض"));
            entity.HasKey("CategoryId", "ProjectId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
