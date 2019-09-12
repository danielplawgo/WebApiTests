using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTests.Migrator
{
    public class Options
    {
        [Option('c', "connectionString", Required = true, HelpText = "The connection string to database that needs to be updated.")]
        public string ConnectionString { get; set; }

        [Option('s', "createSnapshot", Required = false, HelpText = "Should create database snapshot after running migrations.")]
        public bool CreateSnapshot { get; set; }

        [Option('p', "snapshotPath", Required = false, HelpText = "The path for snapshot database.")]
        public string SnapshotPath { get; set; }
    }
}
