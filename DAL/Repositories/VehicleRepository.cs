using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO;
using DAL.Entities;
using DAL.Interfaces;
using Dapper;

namespace DAL.Repositories
{
    public class VehicleRepository : BaseRepository, IVehicleRepository
    {
        private const string QueryCreate = "INSERT INTO Vehicles (" +
            "VehicleTypeId, ModelName, StateNumber, ManufactureYear, Mileage, Weight, EngineType," +
            "EngineCapacity, EngineConsumption, TankCapacity, Color)" +
            "VALUES(" +
            "@VehicleTypeId, @ModelName, @StateNumber, @ManufactureYear, @Mileage, @Weight, @EngineType," +
            "@EngineCapacity, @EngineConsumption, @TankCapacity, @Color)";

        private const string QueryDelete = "DELETE FROM Vehicles WHERE VehicleId = @id";

        private const string QueryGetAll = "SELECT V.*,VT.VehicleTypeId AS VTId,VT.Name,VT.TaxCoefficient " +
            "FROM Vehicles AS V INNER JOIN VehicleTypes AS VT ON V.VehicleTypeId=VT.VehicleTypeId";

        private const string QueryGetById = "SELECT V.*,VT.VehicleTypeId AS VTId,VT.Name,VT.TaxCoefficient " +
            "FROM Vehicles AS V INNER JOIN VehicleTypes AS VT ON V.VehicleTypeId=VT.VehicleTypeId" +
            " WHERE VehicleId = @id";

        private const string QueryUpdate = "UPDATE Vehicles SET " +
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
            "Color = @Color " +
            "WHERE VehicleId = @VehicleId";
        public VehicleRepository(string connectionString) : base(connectionString) { }

        public async Task Create(Vehicle item) => await connection.ExecuteAsync(QueryCreate, item);

        public async Task Delete(int id) => await connection.ExecuteAsync(QueryDelete, new { id });

        public async Task<IEnumerable<Vehicle>> GetAll()
        {
            return await connection.QueryAsync<Vehicle,VehicleType,Vehicle>(
                QueryGetAll,(vehicle,vehicleType)=>
                {
                    vehicle.VehicleType = vehicleType;
                    return vehicle;
                },
                splitOn:"VTId"
                );
        }

        public async Task<IEnumerable<Vehicle>> GetAll(SortOrder sortOrder)
        {
            var ordering = GetOrdering(sortOrder);

            return await connection.QueryAsync<Vehicle, VehicleType, Vehicle>(
                QueryGetAll + $" ORDER BY {ordering.Item1} {ordering.Item2}",
                (vehicle, vehicleType) =>
                {
                    vehicle.VehicleType = vehicleType;
                    return vehicle;
                },
                splitOn: "VTId"
                );
        }

        private (string,string) GetOrdering(SortOrder sortOrder)
        {
            var result = sortOrder switch
            {
                SortOrder.NameAsc => ("V.ModelName", "ASC"),
                SortOrder.NameDesc => ("V.ModelName", "DESC"),
                SortOrder.TypeAsc => ("VT.Name", "ASC"),
                SortOrder.TypeDesc => ("VT.Name", "DESC"),
                SortOrder.MileageAsc => ("V.Mileage", "ASC"),
                SortOrder.MileageDesc => ("V.Mileage", "DESC"),
                SortOrder.IdAsc => ("V.VehicleId", "ASC"),
                _ => ("V.VehicleId","DESC")
            };

            return result;
        }

        public async Task<Vehicle> GetById(int id)
        {
            var collection = await connection.QueryAsync<Vehicle, VehicleType, Vehicle>(
                QueryGetById, (vehicle, vehicleType) =>
                {
                    vehicle.VehicleType = vehicleType;
                    return vehicle;
                },
                splitOn: "VTId",
                param: new { id }
                );

            return collection.First();
        }

        public async Task Update(Vehicle item) => await connection.ExecuteAsync(QueryUpdate, item);
    }
}
