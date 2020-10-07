using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleAPI.Models;
using SimpleAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimpleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepository orderRepository,
            IClientRepository clientRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository ?? 
                throw new ArgumentNullException(nameof(orderRepository));
            _clientRepository = clientRepository ??
                throw new ArgumentNullException(nameof(clientRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/<OrdersController>
        [HttpGet]
        public IActionResult Get()
        {
            var orderEntities = _orderRepository.GetOrders();
            /* With Automapper */
            return Ok(_mapper.Map<IEnumerable<OrderDto>>(orderEntities));

            /* Without Automapper */
            //var results = new List<OrderDto>();

            //foreach (var order in orderEntities)
            //{
            //    results.Add(new OrderDto()
            //    {
            //        Id = order.Id,
            //        Code = order.Code,
            //        Status = order.Status,
            //        IsPaid = order.IsPaid
            //    });
            //}

            //return Ok(results);
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var order = _orderRepository.GetOrder(id);

            if (order == null)
            {
                return NotFound();
            }
            
            /* With Automapper */
            return Ok(_mapper.Map<OrderDto>(order));
        }

        // POST api/<OrdersController>
        [HttpPost]
        public IActionResult Post([FromBody] OrderForCreationDto order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_clientRepository.ClientExists(order.ClientId))
            {
                return NotFound();
            }

            var finalOrderCreated = _mapper.Map<Entities.Order>(order);

            _clientRepository.AddOrderForClient(order.ClientId, finalOrderCreated);
            _clientRepository.Save();

            var createdOrderToReturn = _mapper.Map<Models.OrderDto>(order);

            return CreatedAtRoute(
                "GetOrder",
                new { order.ClientId, id = createdOrderToReturn.Id },
                createdOrderToReturn);
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] OrderForUpdateDto order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_clientRepository.ClientExists(order.ClientId))
            {
                return NotFound();
            }

            var orderFetched = _orderRepository.GetOrder(id);
            if (orderFetched == null)
            {
                return NotFound();
            }

            _mapper.Map(order, orderFetched);
            _orderRepository.Save();

            return NoContent();
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
