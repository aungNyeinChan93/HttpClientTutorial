using HttpClient.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClient.Database
{
    public class AppDbContext :DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<Manager> Managers { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manager>()
                .HasOne(x => x.Team)
                .WithOne(t => t.Manager)
                .HasForeignKey<Team>(t => t.ManagerId)
                .IsRequired(false);
        }
    }
}
