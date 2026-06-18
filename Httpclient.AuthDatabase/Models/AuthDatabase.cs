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

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Role1> Roles1 { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-8CJL57G\\SQLEXPRESS;Initial Catalog=Authentication;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__role__3214EC07C7CB60C3");

            entity.ToTable("role");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Role1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC076E4C42CC");

            entity.ToTable("Roles");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07978BC9A8");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053430D4BD2C").IsUnique();

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);

            entity.HasOne(d => d.Role).WithMany(p => p.Users).HasForeignKey(d => d.RoleId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
