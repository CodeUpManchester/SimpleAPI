using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SimpleAPI
{
    public class DataLayer
    {
        private readonly ConnectionFactory _connectionFactory;

        public DataLayer()
        {
            _connectionFactory = new ConnectionFactory();
        }

        public SqlDataReader RunStoredProcedure(string storedProcedureName, IList<SqlParameter> parameters)
        {
            var connection = (SqlConnection)_connectionFactory.Create();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = storedProcedureName;
            command.CommandType = CommandType.StoredProcedure;

            foreach (var parameter in parameters)
                command.Parameters.Add(parameter);

            return command.ExecuteReader();
        }
    }
}