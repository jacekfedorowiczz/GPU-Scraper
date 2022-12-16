﻿using Microsoft.EntityFrameworkCore;

namespace GPU_Scraper.Entities
{
    public class GPUScraperDbContext : DbContext
    {
        public DbSet<GPU> GPUs { get; set; }
        public DbSet<Shop> Shops { get; set; }

        public GPUScraperDbContext(DbContextOptions<GPUScraperDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
