using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DAL.Repositories
{
    public class VehicleTypeRepository : BaseRepository, IRepository<VehicleType>
    {
        private const string QueryCreate = "INSERT INTO VehicleTypes (Name, TaxCoefficient)" +
            " VALUES(@Name, @TaxCoefficient)";
        private const string QueryDelete = "DELETE FROM VehicleTypes WHERE VehicleTypeId = @id";
        private const string QueryGetAll = "SELECT * FROM VehicleTypes";
        private const string QueryGetById = "SELECT * FROM VehicleTypes WHERE VehicleTypeId = @id";
        private const string QueryUpdate = "UPDATE VehicleTypes SET Name = @Name, TaxCoefficient = @TaxCoefficient "
                                       + "WHERE VehicleTypeId = @VehicleTypeId";

        public VehicleTypeRepository(string connectionString):base(connectionString) { }

        public async Task Create(VehicleType item) => await connection.ExecuteAsync(QueryCreate, item);

        public async Task Delete(int id) => await connection.ExecuteAsync(QueryDelete, new { id });

        public async Task<IEnumerable<VehicleType>> GetAll() =>
            await connection.QueryAsync<VehicleType>(QueryGetAll);

        public async Task<VehicleType> GetById(int id) =>
            await connection.QueryFirstAsync<VehicleType>(QueryGetById, new { id });

        public async Task Update(VehicleType item) => await connection.ExecuteAsync(QueryUpdate, item);
    }
}
