using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;
using Dapper;

namespace DAL.Repositories
{
    public class VehicleRepository : BaseRepository, IRepository<Vehicle>
    {
        private const string queryCreate = "INSERT INTO Vehicles (" +
            "VehicleTypeId, ModelName, StateNumber, ManufactureYear, Mileage, Weight, EngineType," +
            "EngineCapacity, EngineConsumption, TankCapacity, Color)" +
            "VALUES(" +
            "@VehicleTypeId, @ModelName, @StateNumber, @ManufactureYear, @Mileage, @Weight, @EngineType," +
            "@EngineCapacity, @EngineConsumption, @TankCapacity, @Color";

        private const string queryDelete = "DELETE FROM Vehicles WHERE VehicleId = @Id";

        private const string queryGetAll = "SELECT * FROM Vehicles";

        private const string queryGetById = "SELECT * FROM Vehicles WHERE VehicleId = @Id";

        private const string queryUpdate = "UPDATE Vehicles SET" +
            "VehicleTypeId = @VehicleTypeId," +
            "ModelName = @ModelName," +
            "StateNumber = @StateNumber," +
            "ManufactureYear = @ManufactureYear," +
            "Mileage = @Mileage," +
            "Weight = @Weight," +
            "EngineType = @EngineType," +
            "EngineCapacity = @EngineCapacity," +
            "EngineConsumption = @EngineConsumption," +
            "TankCapacity = @TankCapacity," +
            "Color = @Color" +
            "WHERE VehicleId = @Id";
        public VehicleRepository(string connectionString) : base(connectionString) { }

        public void Create(Vehicle item) => connection.ExecuteAsync(queryCreate, item);

        public void Delete(int id) => connection.ExecuteAsync(queryDelete, id);

        public async Task<IEnumerable<Vehicle>> GetAll() => await connection.QueryAsync<Vehicle>(queryGetAll);

        public async Task<Vehicle> GetById(int id) => await connection.QueryFirstAsync<Vehicle>(queryGetById, id);

        public void Update(Vehicle item) => connection.ExecuteAsync(queryUpdate, item);
    }
}
