using System.Collections.Generic;
using System.Threading.Tasks;
using Univerus.Console.Domain.Entities;

namespace Univerus.Console.Application.Services
{
    public interface IPersonTypeServices
    {
        Task<int> CreatePersonType(PersonType personType);
        Task<List<PersonType>> GetPersonTypes();
    }
}
