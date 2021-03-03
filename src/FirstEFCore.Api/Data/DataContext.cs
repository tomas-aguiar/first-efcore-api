using FirstEFCore.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FirstEFCore.Api.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;
        
        public DbSet<Product> Products { get; set; }

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("inMemoryDb");
        }
    }
}