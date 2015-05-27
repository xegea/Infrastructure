using System;
using System.Collections.Generic;
using System.Data;

namespace Infrastructure.Dapper
{
    public interface IDapperRepository
    {
        IEnumerable<T> Query<T>(String sql);

        IEnumerable<T> Query<T>(String sql, dynamic param);

        IEnumerable<T> Query<T>(string storedName, dynamic param, CommandType commandType);
    }
}
