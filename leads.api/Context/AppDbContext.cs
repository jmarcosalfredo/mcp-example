using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using leads.api.Models;
using Microsoft.EntityFrameworkCore;

namespace leads.api.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Lead> Leads { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
