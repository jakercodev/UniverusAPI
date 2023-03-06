
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Univerus.Console.Application.Services;
using Univerus.Console.Domain.Entities;
using Univerus.Console.Shared.DatabaseAccess;

namespace Univerus.Console.Infrastructure.Services
{
    public class PersonTypeServices: IPersonTypeServices
    {
        private readonly IDALBase _dataAccess;

        public PersonTypeServices(IDALBase dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<int> CreatePersonType(PersonType personType)
        {
            string query = "INSERT INTO PersonType (Description) " +
                         "VALUES (@Description)";

            var param = new DynamicParameters();
            param.Add("Description", personType.Description);

            int affectedRecords = await this._dataAccess.ExecuteNonQueryAsync(query, param, System.Data.CommandType.Text);
            return affectedRecords;
        }

        public async Task<List<PersonType>> GetPersonTypes()
        {
            string query = $"SELECT {_dataAccess.GenerateColumnsSelectQuery(new PersonType())} FROM PersonType ORDER BY PersonTypeId;";
            return await _dataAccess.QueryAsListAsync<PersonType>(query, null, System.Data.CommandType.Text);
        }
    }
}
