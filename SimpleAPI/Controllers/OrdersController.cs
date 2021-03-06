﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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
        [HttpHead]
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
        [HttpGet("{id}", Name = "GetOrder")]
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

            var client = _clientRepository.GetClient(order.ClientId, false);

            if (client == null)
            {
                return NotFound();
            }

            var finalOrderCreated = _mapper.Map<Entities.Order>(order);
            finalOrderCreated.Client = client;

            _orderRepository.CreateOrder(finalOrderCreated);
            _orderRepository.Save();

            var createdOrderToReturn = _mapper.Map<Models.OrderDto>(finalOrderCreated);

            return CreatedAtRoute(
                "GetOrder",
                new { id = createdOrderToReturn.Id },
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

        // PATCH api/<OrdersController>/5
        /*
            Example of PATCH request:
            [{
                "op": "replace",
                "path": "/code",
                "value": "TTTTTT123"               
            }]
         */
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<OrderForUpdateDto> patchDoc)
        {
            var orderEntity = _orderRepository.GetOrder(id);
            if (orderEntity == null)
            {
                return NotFound();
            }

            var orderToPatch = _mapper.Map<OrderForUpdateDto>(orderEntity);
            orderToPatch.UpdatedAt = DateTime.Now;

            patchDoc.ApplyTo(orderToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!TryValidateModel(orderToPatch))
            {
                return BadRequest();
            }

            _mapper.Map(orderToPatch, orderEntity);
            _orderRepository.Save();

            return NoContent();
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var orderEntityToRemove = _orderRepository.GetOrder(id);
            
            if (orderEntityToRemove == null)
                return NotFound();
            
            _orderRepository.DeleteOrder(orderEntityToRemove);
            _orderRepository.Save();

            return NoContent();
        }
    }
}
