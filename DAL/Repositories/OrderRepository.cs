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
    public class OrderRepository : BaseRepository, IOrderRepositoryExt
    {
        private const string QueryCreate = "INSERT INTO Orders (VehicleId) VALUES(@VehicleId)";
        private const string QueryCreateReturn = "INSERT INTO Orders (VehicleId) " +
            " OUTPUT Inserted.OrderId, Inserted.VehicleId " +
            " VALUES(@VehicleId)";
        private const string QueryDelete = "DELETE FROM Orders WHERE OrderId = @Id";
        private const string QueryGetAll = "SELECT "
                                           + "O.*, V.VehicleId AS VId, V.VehicleTypeId, V.ModelName, V.StateNumber, V.Weight, V.ManufactureYear, "
                                           + "V.Mileage, V.Color, V.EngineType, V.EngineCapacity, V.EngineConsumption, V.TankCapacity "
                                           + "FROM Orders AS O "
                                           + "LEFT JOIN Vehicles AS V ON O.VehicleId = V.VehicleId ";

        private const string QueryGetById = "SELECT "
                                           + "O.*, V.VehicleId AS VId, V.VehicleTypeId, V.ModelName, V.StateNumber, V.Weight, V.ManufactureYear, "
                                           + "V.Mileage, V.Color, V.EngineType, V.EngineCapacity, V.EngineConsumption, V.TankCapacity, "
                                           + "OE.OrderElementId AS OEId, OE.OrderId, OE.DetailId, OE.DetailCount, "
                                           + "D.DetailId AS DId, D.Name FROM Orders AS O "
                                           + "LEFT JOIN OrdersElements  AS OE  ON O.OrderId = OE.OrderId "
                                           + "     JOIN Vehicles       AS V   ON O.VehicleId = V.VehicleId "
                                           + "LEFT JOIN Details         AS D  ON OE.DetailId = D.DetailId "
                                           + "WHERE O.OrderId = @Id";

        private const string QueryUpdate = "UPDATE Orders SET VehicleId = @VehicleId WHERE OrderId = @Id";

        public OrderRepository(string connectionString) : base(connectionString) { }

        public async Task Create(Order item) => await connection.ExecuteAsync(QueryCreate, item);

        public async Task<Order> CreateReturn(Order item) => await connection.QuerySingleAsync<Order>(QueryCreateReturn, item);

        public async Task Delete(int id) => await connection.ExecuteAsync(QueryDelete, id);

        public async Task<IEnumerable<Order>> GetAll()
        {


            return await connection.QueryAsync<Order, Vehicle, Order>
            (QueryGetAll, (order, vehicle) =>
            {
                order.Vehicle = vehicle;
                return order;
            },
            splitOn: "VId"
            );
        }


        public async Task<Order> GetById(int id)
        {
            var orderElements = new List<OrderElement>();

            var collection = await connection.QueryAsync<Order, Vehicle, OrderElement, Detail, Order>
            (QueryGetById, (order, vehicle, orderElement, detail) =>
            {
                order.Vehicle = vehicle;
                orderElement.Part = detail;
                orderElements.Add(orderElement);

                return order;
            },
            splitOn: "VId,OEId,DId",
            param: new { id }
            );

            var order = collection.First();
            order.OrderElements = orderElements;

            return order;
        }

        public async Task Update(Order item) => await connection.ExecuteAsync(QueryUpdate, item);
    }
}
