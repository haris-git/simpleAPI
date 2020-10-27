using System;
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
        [HttpHead]
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
        [HttpGet("{id}", Name = "GetClient")]
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
        public IActionResult Post([FromBody] ClientForCreationDto client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var finalClientCreated = _mapper.Map<Entities.Client>(client);
            _clientRepository.CreateClient(finalClientCreated);
            _clientRepository.Save();

            var createdClientToReturn = _mapper.Map<Models.ClientDto>(finalClientCreated);

            return CreatedAtRoute(
                "GetClient",
                new { id = createdClientToReturn.Id },
                createdClientToReturn);
        }

        // PUT api/<ClientsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ClientForUpdateDto client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clientFetched = _clientRepository.GetClient(id, false);
            if (clientFetched == null)
                return NotFound();

            _mapper.Map(client, clientFetched);
            _clientRepository.Save();

            return NoContent();
        }

        // PATCH api/<ClientsController>/5
        /*
            Example of PATCH request:
            [{
                "op": "replace",
                "path": "/code",
                "value": "TTTTTT123"               
            }]
         */
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<ClientForUpdateDto> patchDoc)
        {
            var clientFetched = _clientRepository.GetClient(id, false);
            if (clientFetched == null)
            {
                return NotFound();
            }

            var clientToPatch = _mapper.Map<ClientForUpdateDto>(clientFetched);
            clientToPatch.UpdatedAt = DateTime.Now;

            patchDoc.ApplyTo(clientToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!TryValidateModel(clientToPatch))
            {
                return BadRequest();
            }

            _mapper.Map(clientToPatch, clientFetched);
            _clientRepository.Save();

            return NoContent();
        }

        // DELETE api/<ClientsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var clientEntityToRemove = _clientRepository.GetClient(id, false);

            if (clientEntityToRemove == null)
                return NotFound();

            _clientRepository.DeleteClient(clientEntityToRemove);
            _clientRepository.Save();

            return NoContent();
        }
    }
}
