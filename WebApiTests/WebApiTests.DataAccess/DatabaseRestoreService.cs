using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTests.Logic;
using WebApiTests.Logic.Interfaces;

namespace WebApiTests.DataAccess
{
    public class DatabaseRestoreService : IDatabaseRestoreService
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public Result Restore(string connectionString)
        {
            var connectionBuilder = new SqlConnectionStringBuilder(connectionString);

            var databaseName = connectionBuilder.InitialCatalog;
            _logger.Info($"Restore snapshot for {databaseName} database");

            var connectionToMasterBuilder = new SqlConnectionStringBuilder(connectionString)
            { InitialCatalog = "master" };

            using (var conn = new SqlConnection(connectionToMasterBuilder.ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"IF DB_ID('{databaseName}_Snapshot') IS NOT NULL
BEGIN
    ALTER DATABASE {databaseName} SET SINGLE_USER WITH ROLLBACK IMMEDIATE;  
    RESTORE DATABASE {databaseName} FROM DATABASE_SNAPSHOT = '{databaseName}_Snapshot';  
    ALTER DATABASE {databaseName} SET MULTI_USER;
END";

                    _logger.Trace(cmd.CommandText);
                    cmd.ExecuteNonQuery();
                }
            }

            return Result.Ok();
        }

        public Result CreateSnapshot(string connectionString, string path)
        {
            var connectionBuilder = new SqlConnectionStringBuilder(connectionString);

            var databaseName = connectionBuilder.InitialCatalog;
            _logger.Info($"Create snapshot for {databaseName} database");

            var connectionToMasterBuilder = new SqlConnectionStringBuilder(connectionString)
            { InitialCatalog = "master" };

            using (var conn = new SqlConnection(connectionToMasterBuilder.ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"CREATE DATABASE {databaseName}_Snapshot ON  
( NAME = {databaseName}, FILENAME = '{path}' )  
AS SNAPSHOT OF {databaseName};";
                    _logger.Trace(cmd.CommandText);
                    cmd.ExecuteNonQuery();
                }
            }

            return Result.Ok();
        }

        public Result DropSnapshot(string connectionString)
        {
            var connectionBuilder = new SqlConnectionStringBuilder(connectionString);

            var databaseName = connectionBuilder.InitialCatalog;
            _logger.Info($"Drop snapshot for {databaseName} database");

            var connectionToMasterBuilder = new SqlConnectionStringBuilder(connectionString)
            { InitialCatalog = "master" };

            using (var conn = new SqlConnection(connectionToMasterBuilder.ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"IF DB_ID('{databaseName}_Snapshot') IS NOT NULL
    DROP DATABASE {databaseName}_Snapshot;";
                    _logger.Trace(cmd.CommandText);
                    cmd.ExecuteNonQuery();
                }
            }

            return Result.Ok();
        }
    }
}
