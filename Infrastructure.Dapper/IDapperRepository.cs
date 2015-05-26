using System;
using System.Collections.Generic;

namespace Infrastructure.Dapper
{
    public interface IDapperRepository
    {
        IEnumerable<T> Query<T>(String sql);

        IEnumerable<T> Query<T>(String sql, dynamic param);
    }
}
