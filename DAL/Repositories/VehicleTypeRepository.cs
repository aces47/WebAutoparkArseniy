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
        private const string queryCreate = "INSERT INTO VehicleTypes (Name, TaxCoefficient)" +
            " VALUES(@Name, @TaxCoefficient)";
        private const string queryDelete = "DELETE FROM VehicleTypes WHERE VehicleTypeId = @Id";
        private const string queryGetAll = "SELECT * FROM VehicleTypes";
        private const string queryGetById = "SELECT * FROM VehicleTypes WHERE VehicleTypeId = @Id";
        private const string queryUpdate = "UPDATE VehicleTypes SET Name = @Name, TaxCoefficient = @TaxCoefficient "
                                       + "WHERE VehicleTypeId = @Id";

        public VehicleTypeRepository(string connectionString):base(connectionString) { }

        public void Create(VehicleType item) => connection.ExecuteAsync(queryCreate, item);

        public void Delete(int id) => connection.ExecuteAsync(queryDelete, id);

        public async Task<IEnumerable<VehicleType>> GetAll() =>
            await connection.QueryAsync<VehicleType>(queryGetAll);

        public async Task<VehicleType> GetById(int id) =>
            await connection.QueryFirstAsync<VehicleType>(queryGetById, id);

        public void Update(VehicleType item) => connection.ExecuteAsync(queryUpdate, item);
    }
}
