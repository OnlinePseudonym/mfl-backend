using MFL.Data.Players.Entities;
using MFL.Data.SeedWork;
using MFL.Data.Users.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace MFL.Data
{
    public class MFLContext : DbContext
    {
        public MFLContext(DbContextOptions<MFLContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MFLContext).Assembly);
        }

        public override int SaveChanges()
        {
            var now = DateTime.Now;

            foreach (var entity in ChangeTracker.Entries())
            {
                if (entity.Entity is IUpdatable updatable)
                {
                    if (entity.State == EntityState.Added)
                    {
                        updatable.CreatedDate = now;
                        updatable.UpdatedDate = now;
                    }
                    else if (entity.State == EntityState.Modified)
                    {
                        updatable.UpdatedDate = now;
                    }
                }
            }
            return base.SaveChanges();
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<WaiverClaim> Claims { get; set; }
    }
}
