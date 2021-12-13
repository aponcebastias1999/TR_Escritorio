using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Dapper;
using Oracle.ManagedDataAccess.Client;

namespace Aplicacion.Database
{

    public class OracleProvider
    {
        const string connectionString = @"DATA SOURCE=localhost/ORCL;PASSWORD=0;PERSIST SECURITY INFO=True;USER ID=TRDBA";

        public static IDbConnection GetConnection()
        {
            var connection = new OracleConnection(connectionString);
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();

            }
            return connection;
        }
        public static void CloseConnection(IDbConnection connection)
        {
            if (connection.State == ConnectionState.Open || connection.State == ConnectionState.Broken)
            {
                connection.Close();
            }
        }
    }
}
