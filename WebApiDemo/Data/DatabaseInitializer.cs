using FizzWare.NBuilder;
using System.Linq;

namespace WebApiDemo.Data
{
    public static class DatabaseInitializer
    {

        public static void AddDatabaseSeedData(DemoDbContext dbContext)
        {
            if (!dbContext.Customers.Any())
            {
                var customers = Builder<Data.Entities.Customer>.CreateListOfSize(100)
                    .All()
                        .With(c => c.FirstName = Faker.Name.First())
                        .With(c => c.LastName = Faker.Name.Last())
                    .Build();

                dbContext.Customers.AddRange(customers.ToArray());
            }

            dbContext.SaveChanges();
        }
    }
}
