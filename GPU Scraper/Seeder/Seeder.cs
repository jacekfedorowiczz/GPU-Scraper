using GPU_Scraper.Entities;
using GPUScraper.Entities;
using Microsoft.EntityFrameworkCore;

namespace GPUScraper.Seeder
{
    public class Seeder
    {
        private readonly GPUScraperDbContext _dbContext;

        public Seeder(GPUScraperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SeedDatabase()
        {
            var pendingMigrations = _dbContext.Database.GetPendingMigrations();

            if (pendingMigrations != null && pendingMigrations.Any())
            {
                _dbContext.Database.Migrate();
            }
            if (!_dbContext.Roles.Any())
            {
                var roles = GetRoles();
                _dbContext.Roles.AddRange(roles);
                _dbContext.SaveChanges();
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "Admin"
                },
                new Role()
                {
                    Name = "User"
                },
            };
            return roles;
        }
    }
}
