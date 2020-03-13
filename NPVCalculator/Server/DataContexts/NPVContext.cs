using Microsoft.EntityFrameworkCore;
using NPVCalculator.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPVCalculator.Server.DataContexts
{
    public class NPVContext : DbContext
    {
        public NPVContext() { }

        public NPVContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NPVCriteria>()
                .Property(x => x.DateCreated)
                .HasDefaultValueSql("getdate()");
        }

        public DbSet<NPVCriteria> Criterias { get; set; }
        public DbSet<NPVResult> Results { get; set; }

    }
}
