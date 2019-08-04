using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTests.Models;

namespace WebApiTests.Logic.Interfaces
{
    public interface ILogic<T> where T : BaseModel
    {
        Result<IEnumerable<T>> GetAllActive();

        Result<T> GetById(int id);

        Result<T> Add(T model);

        Result<T> Update(T model);

        Result Delete(T model);
    }
}
