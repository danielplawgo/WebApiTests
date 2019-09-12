using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTests.Migrator
{
    public class MigrationLogger : System.Data.Entity.Migrations.Infrastructure.MigrationsLogger
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public override void Info(string message)
        {
            _logger.Info(message);
        }

        public override void Warning(string message)
        {
            _logger.Warn(message);
        }

        public override void Verbose(string message)
        {
            _logger.Trace(message);
        }
    }
}
