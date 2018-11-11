using System;
using System.Configuration;
using System.Data.Common;

namespace SimpleAPI
{
    public class ConnectionFactory
    {
        private readonly DbProviderFactory _dbProviderFactory;

        public ConnectionFactory()
        {
            _dbProviderFactory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        }

        public DbConnection Create()
        {
            var connection = _dbProviderFactory.CreateConnection();

            if (connection == null)
                throw new Exception("Could not create database connection");

            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;
            return connection;
        }
    }
}