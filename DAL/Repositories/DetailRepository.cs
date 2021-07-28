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
    public class DetailRepository : BaseRepository, IRepository<Detail>
    {
        private const string QueryCreate = "INSERT INTO Details (Name) VALUES(@Name)";

        private const string QueryDelete = "DELETE FROM Details WHERE DetailId = @DetailId";

        private const string QueryGetAll = "SELECT * FROM Details";

        private const string QueryGetById = "SELECT * FROM Details WHERE DetailId = @DetailId";

        private const string QueryUpdate = "UPDATE Details SET Name = @Name WHERE DetailId = @DetailId";

        public DetailRepository(string connectionString) : base(connectionString) { }

        public async Task Create(Detail item) => await connection.ExecuteAsync(QueryCreate, item);

        public async Task Delete(int id) => await connection.ExecuteAsync(QueryDelete, id);

        public async Task<IEnumerable<Detail>> GetAll() => await connection.QueryAsync<Detail>(QueryGetAll);

        public async Task<Detail> GetById(int id) => await connection.QueryFirstAsync<Detail>(QueryGetById, id);
        public async Task Update(Detail item) => await connection.ExecuteAsync(QueryUpdate, item);
    }
}
