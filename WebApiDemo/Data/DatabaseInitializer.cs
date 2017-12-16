using FizzWare.NBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiDemo.Data.Entities;

namespace WebApiDemo.Data
{
    public static class DatabaseInitializer
    {

        public static void AddDatabaseSeedData(DemoDbContext dbContext)
        {
            // see: https://www.jerriepelser.com/blog/creating-test-data-with-nbuilder-and-faker/

            List<Customer> customerList = new List<Customer>();

            if (!dbContext.Customers.Any())
            {
                var customers = Builder<Data.Entities.Customer>.CreateListOfSize(100)
                    .All()
                        .With(c => c.FirstName = Faker.Name.First())
                        .With(c => c.LastName = Faker.Name.Last())
                        .With(c => c.Email = $"{c.FirstName.ToLower()[0]}.{c.LastName.ToLower()}@{Faker.Internet.DomainName()}")
                    .Build();

                customerList = customers.ToList();

                dbContext.Customers.AddRange(customerList);
            }

            if (!dbContext.Orders.Any())
            {
                var rndGenerator = new RandomGenerator();

                var orders = Builder<Data.Entities.Order>.CreateListOfSize(500)
                    .All()
                        .With(o => o.Customer = Pick<Customer>.RandomItemFrom(customerList))
                        .With(o => o.OrderDate = DateTime.Today.AddDays(-rndGenerator.Next(0, 60)))
                        .With(o => o.DeliveredDate = o.OrderDate.AddDays(rndGenerator.Next(1,8)))
                        .With(o => o.TotalDue = rndGenerator.Next(0.01m, 500.00m))
                        .With(o => o.Comment = rndGenerator.Phrase(30))
                    .Build();

                dbContext.Orders.AddRange(orders.ToList());
            }

            dbContext.SaveChanges();
        }
    }
}
