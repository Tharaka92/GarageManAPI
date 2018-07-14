extern alias MySqlConnectorAlias;

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Breakdown.EndSystems.MySql
{
    public static class DbConnectionFactory
    {
        public static DbConnection GetConnection(string connectionString)
        {
            MySqlConnectorAlias::MySql.Data.MySqlClient.MySqlConnection dbConnection = new MySqlConnectorAlias::MySql.Data.MySqlClient.MySqlConnection
            {
                ConnectionString = connectionString
            };
            return dbConnection;
        }
    }
}
