using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace DAL.Repositories
{
    public abstract class BaseRepository: IAsyncDisposable
    {
        protected readonly DbConnection connection;

        protected BaseRepository(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }

        public ValueTask DisposeAsync() => (ValueTask)connection?.DisposeAsync();
    }
}
