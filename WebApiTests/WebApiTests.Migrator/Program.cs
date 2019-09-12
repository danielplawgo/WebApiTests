using CommandLine;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTests.DataAccess;
using WebApiTests.DataAccess.Migrations;

namespace WebApiTests.Migrator
{
    class Program
    {
        private static DatabaseRestoreService _databaseRestoreService;

        protected static DatabaseRestoreService DatabaseRestoreService
        {
            get
            {
                if(_databaseRestoreService == null)
                {
                    _databaseRestoreService = new DatabaseRestoreService();
                }

                return _databaseRestoreService;
            }
        }

        static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<Options>(args);

            result
                .WithParsed(r => Migrate(r));
        }

        private static void Migrate(Options options)
        {
            if(options.CreateSnapshot)
            {
                DatabaseRestoreService.Restore(options.ConnectionString);
                DatabaseRestoreService.DropSnapshot(options.ConnectionString);
            }

            var configuration = new Configuration();

            configuration.TargetDatabase = new DbConnectionInfo(
                options.ConnectionString,
                "System.Data.SqlClient");

            var migrator = new DbMigrator(configuration);

            MigratorLoggingDecorator logger = new MigratorLoggingDecorator(migrator, new MigrationLogger());
            logger.Update();

            if(options.CreateSnapshot)
            {
                DatabaseRestoreService.CreateSnapshot(options.ConnectionString, options.SnapshotPath);
            }
        }
    }
}
