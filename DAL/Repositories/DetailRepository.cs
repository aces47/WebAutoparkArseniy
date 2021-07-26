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
        private const string queryCreate = "INSERT INTO Details (Name) VALUES(@Name)";

        private const string queryDelete = "DELETE FROM Details WHERE DetailId = @Id";

        private const string queryGetAll = "SELECT * FROM Details";

        private const string queryGetById = "SELECT * FROM Details WHERE DetailId = @Id";

        private const string queryUpdate = "UPDATE Details SET Name = @Name WHERE DetailId = @Id";

        public DetailRepository(string connectionString) : base(connectionString) { }

        public void Create(Detail item) => connection.ExecuteAsync(queryCreate, item);

        public void Delete(int id) => connection.ExecuteAsync(queryDelete, id);

        public async Task<IEnumerable<Detail>> GetAll() => await connection.QueryAsync<Detail>(queryGetAll);

        public async Task<Detail> GetById(int id) => await connection.QueryFirstAsync<Detail>(queryGetById, id);
        public void Update(Detail item) => connection.ExecuteAsync(queryUpdate, item);
    }
}
