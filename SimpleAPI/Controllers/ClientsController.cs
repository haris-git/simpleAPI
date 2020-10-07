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
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientsController(IClientRepository clientRepository,
            IMapper mapper)
        {
            _clientRepository = clientRepository ?? 
                throw new ArgumentNullException(nameof(clientRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/<ClientsController>
        [HttpGet]
        public IActionResult Get()
        {
            var clientEntities = _clientRepository.GetClients();

            /* With Automapper */
            return Ok(_mapper.Map<IEnumerable<ClientDto>>(clientEntities));

            /* Without Automapper */
            //var results = new List<ClientDto>();

            //foreach (var clientEntity in clientEntities)
            //{
            //    results.Add(new ClientDto
            //    {
            //        Id = clientEntity.Id,
            //        FirstName = clientEntity.FirstName,
            //        LastName = clientEntity.LastName,
            //        Company = clientEntity.Company
            //    });
            //}

            //return Ok(results);
        }

        // GET api/<ClientsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, bool includeOrders = false)
        {
            var client = _clientRepository.GetClient(id, includeOrders);

            if (client == null)
            {
                return NotFound();
            }

            if (includeOrders)
            {
                var clientResult = _mapper.Map<ClientDto>(client); //TODO Change the dto
                return Ok(clientResult);
            }

            return Ok(_mapper.Map<ClientDto>(client));            
        }

        // POST api/<ClientsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ClientsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClientsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
