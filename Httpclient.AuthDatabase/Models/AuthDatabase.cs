using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Httpclient.AuthDatabase.Models;

public partial class AuthDatabase : DbContext
{
    public AuthDatabase()
    {
    }

    public AuthDatabase(DbContextOptions<AuthDatabase> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC078D95B75F");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534F875D644").IsUnique();

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
