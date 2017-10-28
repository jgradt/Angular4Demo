using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Models;
using WebApiDemo.Data;
using AutoMapper;
using WebApiDemo.Infrastructure;
using WebApiDemo.Data.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiDemo.Controllers
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

        [HttpGet]
        public PagedData<CustomerModel> Get(int pageIndex = 0, int pageSize = 10)
        {
            //System.Threading.Thread.Sleep(1500);

            var data = customerRepository.GetPaged(pageIndex, pageSize);
            var mappeData = new PagedData<CustomerModel>()
            {
                PageIndex = data.PageIndex,
                PageSize = data.PageSize,
                TotalPages = data.TotalPages,
                TotalItems = data.TotalItems,
                Data = mapper.Map<List<CustomerModel>>(data.Data)
            };

            return mappeData;
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get(int id)
        {
            var customer = customerRepository.GetById(id);
            if(customer == null)
            {
                return NotFound();
            }

            var mappedData = mapper.Map<CustomerModel>(customer);

            return new ObjectResult(mappedData);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CustomerModel item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            if(!ModelState.IsValid)
            {
                //TODO: what do I return here? 
            }

            var mappedDataIn = mapper.Map<Customer>(item);
            var result = customerRepository.Add(mappedDataIn);
            var mappedDataOut = mapper.Map<CustomerModel>(result);

            return CreatedAtRoute("GetCustomer", new { id = mappedDataOut.Id }, mappedDataOut);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]CustomerModel item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var mappedData = mapper.Map<Customer>(item);

            customerRepository.Update(id, mappedData);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            customerRepository.Delete(id);

            return new NoContentResult();
        }
    }
}
