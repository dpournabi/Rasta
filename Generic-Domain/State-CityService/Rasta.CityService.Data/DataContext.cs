using Microsoft.EntityFrameworkCore;
using Rasta.CityService.Domain.Entities;

namespace Rasta.CityService.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}
