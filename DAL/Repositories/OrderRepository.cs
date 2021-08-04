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
    public class OrderRepository : BaseRepository, IRepository<Order>
    {
        private const string QueryCreate = "INSERT INTO Orders (VehicleId) VALUES(@VehicleId)";
        private const string QueryDelete = "DELETE FROM Orders WHERE OrderId = @Id";
        private const string QueryGetAll = "SELECT O.*,V.ModelName,V.StateNumber,OE.DetailCount,D.Name" +
            "FROM Orders AS O" +
            "LEFT JOIN OrderElements AS OE ON O.OrderId = OE.OrderId" +
            "JOIN Vehicles AS V ON O.VehicleId = V.VehicleId" +
            "LEFT JOIN Details AS D ON OE.DetailId = D.DetailId";
        private const string QueryGetById = "SELECT O.*,V.ModelName,V.StateNumber,OE.DetailCount,D.Name" +
            "FROM Orders AS O" +
            "LEFT JOIN OrderElements AS OE ON O.OrderId = OE.OrderId" +
            "JOIN Vehicles AS V ON O.VehicleId = V.VehicleId" +
            "LEFT JOIN Details AS D ON OE.DetailId = D.DetailId" +
            "WHERE OrderId = @Id";
        private const string QueryUpdate = "UPDATE Orders SET VehicleId = @VehicleId WHERE OrderId = @Id";

        public OrderRepository(string connectionString) : base(connectionString) { }

        public async Task Create(Order item) => await connection.ExecuteAsync(QueryCreate, item);

        public async Task Delete(int id) => await connection.ExecuteAsync(QueryDelete, id);

        public async Task<IEnumerable<Order>> GetAll()
        {
            var orderElements = new List<OrderElement>();

            return await connection.QueryAsync<Order, Vehicle, OrderElement, Detail, Order>
            (QueryGetAll, (order, vehicle, orderElement, detail) =>
            {
                order.Vehicle = vehicle;
                orderElement.Part = detail;
                orderElements.Add(orderElement);

                return order;
            },
            splitOn: "VId,OEId,SPId"
            );
        }


        public async Task<Order> GetById(int id)
        {
            var orderElements = new List<OrderElement>();

            var collection = await connection.QueryAsync<Order, Vehicle, OrderElement, Detail, Order>
            (QueryGetAll, (order, vehicle, orderElement, detail) =>
            {
                order.Vehicle = vehicle;
                orderElement.Part = detail;
                orderElements.Add(orderElement);

                return order;
            },
            splitOn: "VId,OEId,SPId",
            param: new { id }
            );

            return collection.First();
        }

        public async Task Update(Order item) => await connection.ExecuteAsync(QueryUpdate, item);
    }
}
