using MFL.Data.Models;
using MFL.Data.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFL.Data
{
    public class MFLContext : DbContext
    {
        public MFLContext(DbContextOptions<MFLContext> options) : base(options)
        {
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
    }
}
