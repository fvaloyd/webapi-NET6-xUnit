using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Personas.Api .model;
using Personas.Api .repository;
using Microsoft.AspNetCore.Mvc;

namespace Personas.Api .Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaRepository _repo;
        public PersonaController(IPersonaRepository repo)
        {
           _repo = repo; 
        }

        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await _repo.GetAllPersonasAsync());


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if(id <= 0)return BadRequest("Invalid id");
            var persona = await _repo.GetPersonaByIdAsync(id);
            if(persona == null)return NotFound("Persona is not found");
            return Ok(persona);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Persona persona)
        {
            if(persona == null)return BadRequest("Invalid persona");
            if(await _repo.CreatePersonaAsync(persona) > 0)return CreatedAtAction("Get", new {Id = persona.Id}, persona);
            return BadRequest("Could not create the persona");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Persona persona)
        {
            if(id != persona.Id)return BadRequest("Ids are not the same");
            if(await _repo.UpdatePersonaAsync(persona) > 0)return NoContent();
            return BadRequest("Could not edit the persona");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(id <= 0)return BadRequest("Id not valid");
            if(await _repo.DeletePersonaAsync(id) > 0)return NoContent();
            return BadRequest("Could not delete the persona");
        }
    }
}