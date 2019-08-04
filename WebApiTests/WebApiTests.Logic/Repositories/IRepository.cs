using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTests.Models;

namespace WebApiTests.Logic.Repositories
{
    public interface IRepository<T> where T : BaseModel, new()
    {
        void Add(T entity);

        void Delete(T entity);

        void Delete(int id);

        T GetById(int id);

        IEnumerable<T> GetAllActive();

        IEnumerable<T> GetAll();

        void SaveChanges();
    }
}
