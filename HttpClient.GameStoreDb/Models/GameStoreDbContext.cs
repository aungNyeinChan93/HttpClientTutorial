using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HttpClient.GameStoreDb.Models;

public partial class GameStoreDbContext : DbContext
{
    public GameStoreDbContext()
    {
    }

    public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Game> Games { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-8CJL57G\\SQLEXPRESS;Initial Catalog=GameStore;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Games__3214EC07B200E142");

            entity.Property(e => e.AgeRating).HasMaxLength(50);
            entity.Property(e => e.Developer).HasMaxLength(200);
            entity.Property(e => e.Genre).HasMaxLength(100);
            entity.Property(e => e.Platform).HasMaxLength(100);
            entity.Property(e => e.Publisher).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
