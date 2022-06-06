using Microsoft.EntityFrameworkCore;
using Zadanie1.Models;

namespace Zadanie1.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<CustomerModel> Customers { get; set; }
    }
}