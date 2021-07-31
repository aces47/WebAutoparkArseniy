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
    public class OrderElementRepository : BaseRepository, IRepository<OrderElement>
    {
        private const string QueryCreate = "INSERT INTO OrderElements(OrderId, DetailId, DetailCount) "
                                       + "VALUES (@OrderId, @DetailId, @DetailCount)";

        private const string QueryDelete = "DELETE FROM OrderElements WHERE OrderElementId = @OrderElementId";

        private const string QueryGetAll = "SELECT D.Name, OE.OrderId,OE.DetailCount" +
            "FROM OrderElements AS OE" +
            "JOIN Details AS D ON OE.DetailId=D.DetilId";

        private const string QueryGetById = "SELECT D.Name, OE.OrderId,OE.DetailCount" +
            "FROM OrderElements AS OE" +
            "JOIN Details AS D ON OE.DetailId=D.DetilId" +
            "WHERE OrderElementId = @OrderElementId";

        private const string QueryUpdate = "UPDATE OrderElements "
                                       + "SET OrderId = @OrderId, DetailId = @DetailId, DetailCount = @DetailCount "
                                       + "WHERE OrderElementId = @OrderElementId";

        public OrderElementRepository(string connectionString) : base(connectionString) { }

        public async Task Create(OrderElement item) => await connection.ExecuteAsync(QueryCreate, item);

        public async Task Delete(int id) => await connection.ExecuteAsync(QueryDelete, id);

        public async Task<IEnumerable<OrderElement>> GetAll() =>
            await connection.QueryAsync<OrderElement, Detail, OrderElement>
            (
                QueryGetAll, (orderElement, detail) =>
                 {
                     orderElement.Part = detail;
                     return orderElement;
                 },
                splitOn: "DDetailId"
            );

        public async Task<OrderElement> GetById(int id)
        {
            var collection = await connection.QueryAsync<OrderElement, Detail, OrderElement>
            (
                QueryGetById, (orderElement, detail) =>
                {
                    orderElement.Part = detail;
                    return orderElement;
                },
                splitOn: "DDetailId",
                param: new { id }
            );

            return collection.First();
        }
            

        public async Task Update(OrderElement item) => await connection.ExecuteAsync(QueryUpdate, item);
    }
}
