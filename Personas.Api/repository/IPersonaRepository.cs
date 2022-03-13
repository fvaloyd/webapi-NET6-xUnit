using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Personas.Api .model;

namespace Personas.Api .repository
{
    public interface IPersonaRepository
    {
        Task<IEnumerable<Persona>> GetAllPersonasAsync();
        Task<Persona> GetPersonaByIdAsync(int id);
        Task<int> CreatePersonaAsync(Persona persona); 
        Task<int> UpdatePersonaAsync(Persona persona);
        Task<int> DeletePersonaAsync(int id);
    }
}