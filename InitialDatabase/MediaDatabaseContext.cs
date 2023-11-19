using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InitialDatabase;

public partial class MediaDatabaseContext : DbContext
{
    public MediaDatabaseContext()
    {
    }

    public MediaDatabaseContext(DbContextOptions<MediaDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GroupTable> GroupTables { get; set; }

    public virtual DbSet<GroupedOrderedMedium> GroupedOrderedMedia { get; set; }

    public virtual DbSet<MediaTable> MediaTables { get; set; }

    public virtual DbSet<RatingTable> RatingTables { get; set; }

    public virtual DbSet<TagAlias> TagAliases { get; set; }

    public virtual DbSet<TagTable> TagTables { get; set; }

    public virtual DbSet<TagToImage> TagToImages { get; set; }

    public virtual DbSet<TagType> TagTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=media_database.db;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GroupTable>(entity =>
        {
            entity.ToTable("GroupTable");

            entity.HasIndex(e => e.Id, "IX_GroupTable_Id").IsUnique();
        });

        modelBuilder.Entity<GroupedOrderedMedium>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_GroupedOrderedMedia_Id").IsUnique();

            entity.HasOne(d => d.Group).WithMany(p => p.GroupedOrderedMedia).HasForeignKey(d => d.GroupId);

            entity.HasOne(d => d.Media).WithMany(p => p.GroupedOrderedMedia).HasForeignKey(d => d.MediaId);
        });

        modelBuilder.Entity<MediaTable>(entity =>
        {
            entity.ToTable("MediaTable");

            entity.HasIndex(e => e.Id, "IX_MediaTable_Id").IsUnique();

            entity.HasIndex(e => e.Location, "IX_MediaTable_Location").IsUnique();
        });

        modelBuilder.Entity<RatingTable>(entity =>
        {
            entity.ToTable("RatingTable");

            entity.HasIndex(e => e.Id, "IX_RatingTable_Id").IsUnique();

            entity.HasIndex(e => e.ImageId, "IX_RatingTable_ImageId").IsUnique();

            entity.HasOne(d => d.Image).WithOne(p => p.RatingTable).HasForeignKey<RatingTable>(d => d.ImageId);
        });

        modelBuilder.Entity<TagAlias>(entity =>
        {
            entity.ToTable("TagAlias");

            entity.HasIndex(e => e.Alias, "IX_TagAlias_Alias").IsUnique();

            entity.HasIndex(e => e.Id, "IX_TagAlias_Id").IsUnique();

            entity.HasOne(d => d.Tag).WithMany(p => p.TagAliases).HasForeignKey(d => d.TagId);
        });

        modelBuilder.Entity<TagTable>(entity =>
        {
            entity.ToTable("TagTable");

            entity.HasIndex(e => e.Id, "IX_TagTable_Id").IsUnique();

            entity.HasIndex(e => e.Tag, "IX_TagTable_Tag").IsUnique();

            entity.HasOne(d => d.TagType).WithMany(p => p.TagTables).HasForeignKey(d => d.TagTypeId);
        });

        modelBuilder.Entity<TagToImage>(entity =>
        {
            entity.ToTable("TagToImage");

            entity.HasIndex(e => e.Id, "IX_TagToImage_Id").IsUnique();

            entity.HasOne(d => d.Media).WithMany(p => p.TagToImages).HasForeignKey(d => d.MediaId);

            entity.HasOne(d => d.Tag).WithMany(p => p.TagToImages).HasForeignKey(d => d.TagId);
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
