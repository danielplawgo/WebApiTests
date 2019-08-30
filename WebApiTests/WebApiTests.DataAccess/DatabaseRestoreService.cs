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
        private Lazy<DataContext> _dataContext;

        protected DataContext DataContext => _dataContext.Value;


        public DatabaseRestoreService(Lazy<DataContext> dataContext)
        {
            _dataContext = dataContext;
        }

        public Result Restore()
        {
            var databaseName = DataContext.Database.Connection.Database;

            SqlConnectionStringBuilder connectionBuilder = new SqlConnectionStringBuilder(DataContext.Database.Connection.ConnectionString)
            { InitialCatalog = "master" };

            using (var conn = new SqlConnection(connectionBuilder.ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"ALTER DATABASE {databaseName} SET SINGLE_USER WITH ROLLBACK IMMEDIATE;  
RESTORE DATABASE {databaseName} FROM DATABASE_SNAPSHOT = '{databaseName}_Snapshot';  
ALTER DATABASE {databaseName} SET MULTI_USER;";
                    cmd.ExecuteNonQuery();
                }
            }

            return Result.Ok();
        }
    }
}
