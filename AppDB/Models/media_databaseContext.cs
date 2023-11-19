using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AppDB.Models;

public partial class media_databaseContext : DbContext
{
    public media_databaseContext()
    {
    }

    public media_databaseContext(DbContextOptions<media_databaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Media> Media { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TagAlias> TagAliases { get; set; }

    public virtual DbSet<TagType> TagTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=media_database.db;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Media>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_Media_Id").IsUnique();

            entity.HasIndex(e => e.Location, "IX_Media_Location").IsUnique();
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.ToTable("Tag");

            entity.HasIndex(e => e.Id, "IX_Tag_Id").IsUnique();

            entity.HasIndex(e => e.Tag1, "IX_Tag_Tag").IsUnique();

            entity.Property(e => e.Tag1).HasColumnName("Tag");

            entity.HasOne(d => d.TagType).WithMany(p => p.Tags).HasForeignKey(d => d.TagTypeId);

            entity.HasMany(d => d.Media).WithMany(p => p.Tags)
                .UsingEntity<Dictionary<string, object>>(
                    "MediaTag",
                    r => r.HasOne<Media>().WithMany().HasForeignKey("MediaId"),
                    l => l.HasOne<Tag>().WithMany().HasForeignKey("TagId"),
                    j =>
                    {
                        j.HasKey("TagId", "MediaId");
                        j.ToTable("MediaTag");
                    });
        });

        modelBuilder.Entity<TagAlias>(entity =>
        {
            entity.ToTable("TagAlias");

            entity.HasIndex(e => e.Alias, "IX_TagAlias_Alias").IsUnique();

            entity.HasIndex(e => e.Id, "IX_TagAlias_Id").IsUnique();

            entity.HasOne(d => d.Tag).WithMany(p => p.TagAliases).HasForeignKey(d => d.TagId);
        });

        modelBuilder.Entity<TagType>(entity =>
        {
            entity.ToTable("TagType");

            entity.HasIndex(e => e.Id, "IX_TagType_Id").IsUnique();

            entity.HasIndex(e => e.TypeName, "IX_TagType_TypeName").IsUnique();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
