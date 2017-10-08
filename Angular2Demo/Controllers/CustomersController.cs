using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Angular2Demo.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Angular2Demo.Controllers
{
    [Route("api/customers")]
    public class CustomersController : Controller
    {
        List<CustomerModel> _customers;

        public CustomersController()
        {
            _customers = new List<CustomerModel>()
            {
                new CustomerModel(){ Id = 1, FirstName = "John", LastName = "Doe" },
                new CustomerModel(){ Id = 2, FirstName = "Homer", LastName = "Simpson" },
                new CustomerModel(){ Id = 3, FirstName = "Marge", LastName = "Simpson" },
                new CustomerModel(){ Id = 4, FirstName = "Betty", LastName = "White" },
                new CustomerModel(){ Id = 5, FirstName = "Jerry", LastName = "Seinfeld" }
            };
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<CustomerModel> Get()
        {
            //System.Threading.Thread.Sleep(1500);

            // TODO: retrieve data using repository pattern and DI
            return _customers;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public CustomerModel Get(int id)
        {
            return _customers.FirstOrDefault(c => c.Id == id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
