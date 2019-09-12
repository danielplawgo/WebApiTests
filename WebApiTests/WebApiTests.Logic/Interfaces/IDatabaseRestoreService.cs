using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTests.Logic.Interfaces
{
    public interface IDatabaseRestoreService
    {
        Result Restore(string connectionString);

        Result CreateSnapshot(string connectionString, string path);

        Result DropSnapshot(string connectionString);
    }
}
