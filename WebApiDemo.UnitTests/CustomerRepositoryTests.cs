using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApiDemo.Data;
using WebApiDemo.Data.Entities;
using WebApiDemo.Data.Repositories;
using System.Linq;
using FizzWare.NBuilder;

namespace WebApiDemo.UnitTests
{
    [TestClass]
    [TestCategory("repository"), TestCategory("ef")]
    public class CustomerRepositoryTests
    {
        [TestMethod]
        public void GetById_found()
        {
            var options = new DbContextOptionsBuilder<DemoDbContext>()
                .UseInMemoryDatabase(databaseName: "GetById_found")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DemoDbContext(options))
            {
                context.Customers.Add(new Customer { Id = 1, FirstName = "john", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 2, FirstName = "jane", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 3, FirstName = "paul", LastName = "doe" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new DemoDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var result = repository.GetById(2);
                Assert.IsNotNull(result);
                Assert.AreEqual("jane", result.FirstName);
            }
        }

        [TestMethod]
        public void GetByIdAsync_found()
        {
            var options = new DbContextOptionsBuilder<DemoDbContext>()
                .UseInMemoryDatabase(databaseName: "GetByIdAsync_found")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DemoDbContext(options))
            {
                context.Customers.Add(new Customer { Id = 1, FirstName = "john", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 2, FirstName = "jane", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 3, FirstName = "paul", LastName = "doe" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new DemoDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var result = repository.GetByIdAsync(2).Result;
                Assert.IsNotNull(result);
                Assert.AreEqual("jane", result.FirstName);
            }
        }

        [TestMethod]
        public void GetByIdAsync_not_found()
        {
            var options = new DbContextOptionsBuilder<DemoDbContext>()
                .UseInMemoryDatabase(databaseName: "GetByIdAsync_not_found")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DemoDbContext(options))
            {
                context.Customers.Add(new Customer { Id = 1, FirstName = "john", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 2, FirstName = "jane", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 3, FirstName = "paul", LastName = "doe" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new DemoDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var result = repository.GetByIdAsync(5).Result;
                Assert.IsNull(result);
            }
        }

        [TestMethod]
        public void GetAll()
        {
            var options = new DbContextOptionsBuilder<DemoDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAll")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DemoDbContext(options))
            {
                context.Customers.Add(new Customer { Id = 1, FirstName = "john", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 2, FirstName = "jane", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 3, FirstName = "paul", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 4, FirstName = "bob", LastName = "johnson" });
                context.Customers.Add(new Customer { Id = 5, FirstName = "tim", LastName = "johnson" });
                context.Customers.Add(new Customer { Id = 6, FirstName = "peter", LastName = "piper" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new DemoDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var result = repository.GetAll();
                Assert.IsNotNull(result);
                Assert.AreEqual(6, result.Count);
            }
        }

        [TestMethod]
        public void GetAllAsync()
        {
            var options = new DbContextOptionsBuilder<DemoDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllAsync")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DemoDbContext(options))
            {
                context.Customers.Add(new Customer { Id = 1, FirstName = "john", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 2, FirstName = "jane", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 3, FirstName = "paul", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 4, FirstName = "bob", LastName = "johnson" });
                context.Customers.Add(new Customer { Id = 5, FirstName = "tim", LastName = "johnson" });
                context.Customers.Add(new Customer { Id = 6, FirstName = "peter", LastName = "piper" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new DemoDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var result = repository.GetAllAsync().Result;
                Assert.IsNotNull(result);
                Assert.AreEqual(6, result.Count);
            }
        }

        [TestMethod]
        public void GetAllAsync_with_filter()
        {
            var options = new DbContextOptionsBuilder<DemoDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllAsync_with_filter")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DemoDbContext(options))
            {
                context.Customers.Add(new Customer { Id = 1, FirstName = "john", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 2, FirstName = "jane", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 3, FirstName = "paul", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 4, FirstName = "bob", LastName = "johnson" });
                context.Customers.Add(new Customer { Id = 5, FirstName = "tim", LastName = "johnson" });
                context.Customers.Add(new Customer { Id = 6, FirstName = "peter", LastName = "piper" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new DemoDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var result = repository.GetAllAsync(filter: c => c.LastName == "doe" ).Result;
                Assert.IsNotNull(result);
                Assert.AreEqual(3, result.Count);
            }
        }

        [TestMethod]
        public void GetAllAsync_with_filter_and_orderby()
        {
            var options = new DbContextOptionsBuilder<DemoDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllAsync_with_filter_and_orderby")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DemoDbContext(options))
            {
                context.Customers.Add(new Customer { Id = 1, FirstName = "john", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 2, FirstName = "jane", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 3, FirstName = "paul", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 4, FirstName = "bob", LastName = "johnson" });
                context.Customers.Add(new Customer { Id = 5, FirstName = "tim", LastName = "johnson" });
                context.Customers.Add(new Customer { Id = 6, FirstName = "peter", LastName = "piper" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new DemoDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var result = repository.GetAllAsync(filter: c => c.LastName == "doe", orderBy: q => q.OrderBy(c => c.FirstName)).Result;
                Assert.IsNotNull(result);
                Assert.AreEqual(3, result.Count);
                Assert.AreEqual(2, result[0].Id);
            }
        }

        [TestMethod]
        public void GetCount()
        {
            var options = new DbContextOptionsBuilder<DemoDbContext>()
                .UseInMemoryDatabase(databaseName: "GetCount")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DemoDbContext(options))
            {
                context.Customers.Add(new Customer { Id = 1, FirstName = "john", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 2, FirstName = "jane", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 3, FirstName = "paul", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 4, FirstName = "bob", LastName = "johnson" });
                context.Customers.Add(new Customer { Id = 5, FirstName = "tim", LastName = "johnson" });
                context.Customers.Add(new Customer { Id = 6, FirstName = "peter", LastName = "piper" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new DemoDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var result = repository.GetCount();
                Assert.IsNotNull(result);
                Assert.AreEqual(6, result);
            }
        }

        [TestMethod]
        public void GetCountAsync()
        {
            var options = new DbContextOptionsBuilder<DemoDbContext>()
                .UseInMemoryDatabase(databaseName: "GetCountAsync")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DemoDbContext(options))
            {
                context.Customers.Add(new Customer { Id = 1, FirstName = "john", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 2, FirstName = "jane", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 3, FirstName = "paul", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 4, FirstName = "bob", LastName = "johnson" });
                context.Customers.Add(new Customer { Id = 5, FirstName = "tim", LastName = "johnson" });
                context.Customers.Add(new Customer { Id = 6, FirstName = "peter", LastName = "piper" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new DemoDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var result = repository.GetCountAsync().Result;
                Assert.IsNotNull(result);
                Assert.AreEqual(6, result);
            }
        }

        [TestMethod]
        public void GetExists()
        {
            var options = new DbContextOptionsBuilder<DemoDbContext>()
                .UseInMemoryDatabase(databaseName: "GetExists")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DemoDbContext(options))
            {
                context.Customers.Add(new Customer { Id = 1, FirstName = "john", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 2, FirstName = "jane", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 3, FirstName = "paul", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 4, FirstName = "bob", LastName = "johnson" });
                context.Customers.Add(new Customer { Id = 5, FirstName = "tim", LastName = "johnson" });
                context.Customers.Add(new Customer { Id = 6, FirstName = "peter", LastName = "piper" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new DemoDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var result = repository.GetExists();
                Assert.IsNotNull(result);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void GetExistsAsync()
        {
            var options = new DbContextOptionsBuilder<DemoDbContext>()
                .UseInMemoryDatabase(databaseName: "GetExistsAsync")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DemoDbContext(options))
            {
                context.Customers.Add(new Customer { Id = 1, FirstName = "john", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 2, FirstName = "jane", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 3, FirstName = "paul", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 4, FirstName = "bob", LastName = "johnson" });
                context.Customers.Add(new Customer { Id = 5, FirstName = "tim", LastName = "johnson" });
                context.Customers.Add(new Customer { Id = 6, FirstName = "peter", LastName = "piper" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new DemoDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var result = repository.GetExistsAsync().Result;
                Assert.IsNotNull(result);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void GetExistsAsync_found_with_filter()
        {
            var options = new DbContextOptionsBuilder<DemoDbContext>()
                .UseInMemoryDatabase(databaseName: "GetExistsAsync_found_with_filter")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DemoDbContext(options))
            {
                context.Customers.Add(new Customer { Id = 1, FirstName = "john", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 2, FirstName = "jane", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 3, FirstName = "paul", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 4, FirstName = "bob", LastName = "johnson" });
                context.Customers.Add(new Customer { Id = 5, FirstName = "tim", LastName = "johnson" });
                context.Customers.Add(new Customer { Id = 6, FirstName = "peter", LastName = "piper" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new DemoDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var result = repository.GetExistsAsync(c => c.LastName == "doe").Result;
                Assert.IsNotNull(result);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void GetExistsAsync_not_found_with_filter()
        {
            var options = new DbContextOptionsBuilder<DemoDbContext>()
                .UseInMemoryDatabase(databaseName: "GetExistsAsync_not_found_with_filter")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DemoDbContext(options))
            {
                context.Customers.Add(new Customer { Id = 1, FirstName = "john", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 2, FirstName = "jane", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 3, FirstName = "paul", LastName = "doe" });
                context.Customers.Add(new Customer { Id = 4, FirstName = "bob", LastName = "johnson" });
                context.Customers.Add(new Customer { Id = 5, FirstName = "tim", LastName = "johnson" });
                context.Customers.Add(new Customer { Id = 6, FirstName = "peter", LastName = "piper" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new DemoDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var result = repository.GetExistsAsync(c => c.LastName == "smith").Result;
                Assert.IsNotNull(result);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void GetPagedAsync()
        {
            var options = new DbContextOptionsBuilder<DemoDbContext>()
                .UseInMemoryDatabase(databaseName: "GetPagedAsync")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DemoDbContext(options))
            {
                var customers = Builder<Customer>.CreateListOfSize(25)
                    .All()
                        .With(c => c.FirstName = Faker.Name.First())
                        .With(c => c.LastName = Faker.Name.Last())
                    .Build();

                context.Customers.AddRange(customers.ToArray());
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new DemoDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var result = repository.GetPagedAsync(pageIndex:0, pageSize: 10).Result;
                Assert.IsNotNull(result);
                Assert.AreEqual(0, result.PageIndex);
                Assert.AreEqual(10, result.PageSize);
                Assert.AreEqual(25, result.TotalItems);
                Assert.AreEqual(10, result.Items.Count);
                Assert.AreEqual(1, result.Items[0].Id);
            }
        }

        [TestMethod]
        public void GetPagedAsync_second_page()
        {
            var options = new DbContextOptionsBuilder<DemoDbContext>()
                .UseInMemoryDatabase(databaseName: "GetPagedAsync_second_page")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DemoDbContext(options))
            {
                var customers = Builder<Customer>.CreateListOfSize(25)
                    .All()
                        .With(c => c.FirstName = Faker.Name.First())
                        .With(c => c.LastName = Faker.Name.Last())
                    .Build();

                context.Customers.AddRange(customers.ToArray());
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new DemoDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var result = repository.GetPagedAsync(pageIndex: 1, pageSize: 10).Result;
                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.PageIndex);
                Assert.AreEqual(10, result.PageSize);
                Assert.AreEqual(25, result.TotalItems);
                Assert.AreEqual(10, result.Items.Count);
                Assert.AreEqual(11, result.Items[0].Id);
            }
        }

        [TestMethod]
        public void GetPagedAsync_with_filter()
        {
            var options = new DbContextOptionsBuilder<DemoDbContext>()
                .UseInMemoryDatabase(databaseName: "GetPagedAsync_with_filter")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DemoDbContext(options))
            {
                var customers = Builder<Customer>.CreateListOfSize(25)
                    .All()
                        .With(c => c.FirstName = Faker.Name.First())
                        .With(c => c.LastName = Faker.Name.Last())
                    .Random(12)
                        .With(c => c.LastName = "random_last_name")
                    .Build();

                context.Customers.AddRange(customers.ToArray());
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new DemoDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var result = repository.GetPagedAsync(pageIndex: 0, pageSize: 10, filter: c => c.LastName == "random_last_name").Result;
                Assert.IsNotNull(result);
                Assert.AreEqual(0, result.PageIndex);
                Assert.AreEqual(10, result.PageSize);
                Assert.AreEqual(12, result.TotalItems);
                Assert.AreEqual(10, result.Items.Count);
            }
        }

    }
}
