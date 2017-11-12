using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApiDemo.Data;
using WebApiDemo.Data.Entities;
using WebApiDemo.Data.Repositories;
using System.Linq;

namespace WebApiDemo.UnitTests
{
    [TestClass]
    [TestCategory("repository"), TestCategory("ef")]
    public class CustomerRepositoryTests
    {
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
    }
}
