using Dapper;
using System.Collections.Generic;
using System.Data;

namespace Infrastructure.Dapper
{
    public class DapperRepository : IDapperRepository
    {
        public IEnumerable<T> Query<T>(string sql)
        {
            using (IDbConnection connection = SqlConnectionHelper.OpenConnection())
            {
                return SqlMapper.Query<T>(connection, sql);
            }
        }

        public IEnumerable<T> Query<T>(string sql, dynamic param)
        {
            using (IDbConnection connection = SqlConnectionHelper.OpenConnection())
            {
                return SqlMapper.Query<T>(connection, sql, param);
            }
        }
    }
}
