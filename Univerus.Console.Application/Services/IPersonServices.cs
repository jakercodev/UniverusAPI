using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univerus.Console.Domain.DTO;
using Univerus.Console.Domain.Entities;

namespace Univerus.Console.Application.Services
{
    public interface IPersonServices
    {
        Task<int> CreatePerson(Person entity);
        Task<int> CreateMultiplePerson(List<Person> persons);
        Task<bool> CheckIfDuplicate(string name);
        Task<int> DeletePerson(int id);
        Task<int> UpdatePerson(Person person);
        Task<List<PersonDetailsDTO>> GetPersons();
        Task<Person> GetPerson(int id);
        Task<Person> GetPersonByName(string name);

    }
}
