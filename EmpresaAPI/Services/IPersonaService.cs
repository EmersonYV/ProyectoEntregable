using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IPersonaService
    {
        // Métodos para Personas Naturales
        Task<int> CreatePersonaNaturalAsync(PersonaNatural persona);
        Task<int> UpdatePersonaNaturalAsync(int id, PersonaNatural persona);
        Task<int> DeletePersonaNaturalAsync(int id);
        Task<PersonaNatural> GetPersonaNaturalByIdAsync(int id);
        Task<IEnumerable<PersonaNatural>> ListPersonaNaturalesAsync(string filtro);

        // Métodos para Personas Jurídicas
        Task<int> CreatePersonaJuridicaAsync(PersonaJuridica persona);
        Task<int> UpdatePersonaJuridicaAsync(int id, PersonaJuridica persona);
        Task<int> DeletePersonaJuridicaAsync(int id);
        Task<PersonaJuridica> GetPersonaJuridicaByIdAsync(int id);
        Task<IEnumerable<PersonaJuridica>> ListPersonaJuridicasAsync(string filtro);
    }
}