using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Models;
using WebApiDemo.Data;
using AutoMapper;
using WebApiDemo.Infrastructure;
using WebApiDemo.Data.Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WebApiDemo.Data.Dto;
using WebApiDemo.Data.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiDemo.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/orders")]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public OrdersController(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<PagedData<OrderDto>> Get(int pageIndex = 0, int pageSize = 10)
        {
            //System.Threading.Thread.Sleep(1500);

            var data = await orderRepository.GetPagedAsync(pageIndex, pageSize);
            var mappeData = new PagedData<OrderDto>()
            {
                PageIndex = data.PageIndex,
                PageSize = data.PageSize,
                TotalItems = data.TotalItems,
                Items = mapper.Map<List<OrderDto>>(data.Items)
            };

            return mappeData;
        }

        [HttpGet("{id}", Name = "GetOrder")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await orderRepository.GetByIdAsync(id);
            if(order == null)
            {
                return NotFound();
            }

            var mappedData = mapper.Map<OrderDto>(order);

            return new ObjectResult(mappedData);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderDto item)
        {
            var mappedDataIn = mapper.Map<Order>(item);
            var result =  await orderRepository.AddAsync(mappedDataIn);
            await orderRepository.SaveAsync();
            var mappedDataOut = mapper.Map<OrderDto>(result);

            return CreatedAtRoute("GetOrder", new { id = mappedDataOut.Id }, mappedDataOut);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]OrderDto item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var mappedData = mapper.Map<Order>(item);

            await orderRepository.UpdateAsync(id, mappedData);
            await orderRepository.SaveAsync();

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await orderRepository.DeleteAsync(id);
            await orderRepository.SaveAsync();

            return new NoContentResult();
        }
    }
}
