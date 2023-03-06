using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Univerus.Console.Application.Services;
using Univerus.Console.Domain.DTO;
using Univerus.Console.Domain.Entities;
using Univerus.Console.Shared.DatabaseAccess;

namespace Univerus.Console.Infrastructure.Services
{
    public class PersonServices : IPersonServices
    {
        private readonly IDALBase _dataAccess;

        public PersonServices(IDALBase dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<int> CreatePerson(Person person)
        {
            string query = "INSERT INTO Person (Name, Age, PersonTypeId) " +
                         "VALUES (@Name, @Age, @PersonTypeId)";

            var param = new DynamicParameters();
            param.Add("Name", person.Name);
            param.Add("Age", person.Age);
            param.Add("PersonTypeId", person.PersonTypeId);
            int affectedRecords = await this._dataAccess.ExecuteNonQueryAsync(query, param, System.Data.CommandType.Text);
            return affectedRecords;
        }

        public async Task<int> CreateMultiplePerson(List<Person> persons)
        {
            var affectedRecords = 0;

            foreach (var person in persons)
            {
                string query = "INSERT INTO Person (Name, Age, PersonTypeId) " +
                 "VALUES (@Name, @Age, @PersonTypeId)";

                var param = new DynamicParameters();
                param.Add("Name", person.Name);
                param.Add("Age", person.Age);
                param.Add("PersonTypeId", person.PersonTypeId);
                affectedRecords += await this._dataAccess.ExecuteNonQueryAsync(query, param, System.Data.CommandType.Text);
            }

            return affectedRecords;
        }

        public async Task<bool> CheckIfDuplicate(string name)
        {
            var person = await this.GetPersonByName(name);
            return person == null ? true : false;
        }

        public async Task<int> DeletePerson(int id)
        {
            var person = await this.GetPerson(id);

            if (person != null)
            {
                string query = $"DELETE FROM Person WHERE id = @Id";

                var param = new DynamicParameters();
                param.Add("Id", id);

                int affectedRecords = await this._dataAccess.ExecuteNonQueryAsync(query, param, System.Data.CommandType.Text);
                return affectedRecords;
            }

            return 0;
        }

        public async Task<int> UpdatePerson(Person person)
        {
            var selectedPerson = await this.GetPerson(person.Id);

            if (selectedPerson != null)
            {
                string query = $"UPDATE Person SET Name = @Name, Age = @Age, PersonTypeId = @PersonTypeId WHERE Id = @Id";

                var param = new DynamicParameters();
                param.Add("Id", person.Id);
                param.Add("Name", person.Name);
                param.Add("Age", person.Age);
                param.Add("PersonTypeId", person.PersonTypeId);

                int affectedRecords = await this._dataAccess.ExecuteNonQueryAsync(query, param, System.Data.CommandType.Text);
                return affectedRecords;
            }

            return 0;

        }

        public async Task<List<PersonDetailsDTO>> GetPersons()
        {
            string query = $"SELECT {_dataAccess.GenerateColumnsSelectQuery(new Person(), "p")} , pt.[Description] PersonType " +
                $"FROM Person p " +
                $"INNER JOIN PersonType pt ON p.PersonTypeId = pt.PersonTypeId ORDER BY Name;";
            return await _dataAccess.QueryAsListAsync<PersonDetailsDTO>(query, null, System.Data.CommandType.Text);
        }

        public async Task<Person> GetPerson(int id)
        {
            string query = $"SELECT {_dataAccess.GenerateColumnsSelectQuery(new Person())} FROM Person WITH (NOLOCK) WHERE Id = {id};";
            return await _dataAccess.QueryFirstOrDefaultAsync<Person>(query, null, System.Data.CommandType.Text);
        }

        public async Task<Person> GetPersonByName(string name)
        {
            string query = $"SELECT {_dataAccess.GenerateColumnsSelectQuery(new Person())} FROM Person WITH (NOLOCK) WHERE Name = {name};";
            return await _dataAccess.QueryFirstOrDefaultAsync<Person>(query, null, System.Data.CommandType.Text);
        }
    }
}
