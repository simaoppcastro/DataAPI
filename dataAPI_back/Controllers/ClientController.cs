using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dataAPI_back.Models;

namespace dataAPI_back.Controllers
{
    [Route("api/[controller]")]
    public class ClientController: Controller
    {
        private readonly ApiContext _context;
        public ClientController(ApiContext context)
        {
            _context = context;
        }

        // GET api/client
        [HttpGet]
        public IActionResult Get()
        {
            var data = _context.Clients.OrderBy(c => c.Id);

            // returning the data
            // http 200 response -> ok response
            return Ok(data);
        }

        // GET api/client/client number id
        [HttpGet("{id}", Name = "GetClient")]
        public IActionResult Get(int id)
        {
            var client = _context.Clients.Find(id);

            // return the client specified
            return Ok(client);   
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Client client)
        {   
            if(client == null)
            {
                return BadRequest();    
            }

            // else add the new client
            _context.Clients.Add(client);
            // and save the changes 
            _context.SaveChanges();

            // http 201 response
            // arguments -> route, object values for the route (is this case the id of the new client), actual object (the new client) 
            return CreatedAtRoute("GetClient", new {id = client.Id}, client);
        }
    }

}