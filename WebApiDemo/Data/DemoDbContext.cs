using WebApiDemo.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApiDemo.Data
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        //TODO: set last saved date on save
    }
}
