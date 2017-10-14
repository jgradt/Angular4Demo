using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Angular2Demo.Models;
using Angular2Demo.Data;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Angular2Demo.Controllers
{
    [Route("api/customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;

        public CustomersController(ICustomerRepository customerRepository, IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<CustomerModel> Get()
        {
            //System.Threading.Thread.Sleep(1500);

            var customers = customerRepository.GetAll();
            var mappedData = mapper.Map<List<CustomerModel>>(customers);

            return mappedData;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public CustomerModel Get(int id)
        {
            var customer = customerRepository.GetById(id);
            if(customer != null)
            {
                var mappedData = mapper.Map<CustomerModel>(customer);
                return mappedData;
            }

            //TODO: should we return something else here?
            return null;
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
