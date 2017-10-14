using Angular2Demo.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular2Demo.Data
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
