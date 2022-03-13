using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Personas.Api .context;
using Personas.Api .model;
using Microsoft.EntityFrameworkCore;

namespace Personas.Api .repository
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly ApplicationContext _context;
        public PersonaRepository(ApplicationContext context)
        {
           _context = context; 
        }

        public async Task<IEnumerable<Persona>> GetAllPersonasAsync() =>
          await _context.Personas.ToListAsync(); 

        public async Task<Persona> GetPersonaByIdAsync(int id) 
        {
            var persona = await _context.Personas.FindAsync(id);
            if(persona == null)return null!;
            return persona;
        }

        public async Task<int> CreatePersonaAsync(Persona persona)
        {
            await _context.Personas.AddAsync(persona);
            if(await _context.SaveChangesAsync() > 0)return 1;
            return 0;
        }

        public async Task<int> UpdatePersonaAsync(Persona persona)
        {
            _context.Personas.Update(persona);
            if(await _context.SaveChangesAsync() > 0) return 1;
            return 0;
        }

        public async Task<int> DeletePersonaAsync(int id)
        {
            var personaToDelete = await _context.Personas.FindAsync(id);
            if(personaToDelete == null) return 0;
            _context.Personas.Remove(personaToDelete);
            if(await _context.SaveChangesAsync() > 0) return 1;
            return 0;
        }
    }
}