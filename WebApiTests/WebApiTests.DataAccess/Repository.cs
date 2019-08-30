using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTests.Logic.Repositories;
using WebApiTests.Models;

namespace WebApiTests.DataAccess
{
    public abstract class Repository<T> : IRepository<T> where T : BaseModel, new()
    {
        public Repository(Lazy<DataContext> dataContext)
        {
            if (dataContext == null)
            {
                throw new ArgumentNullException("dataContext");
            }
            _dataContext = dataContext;
        }

        private Lazy<DataContext> _dataContext;

        protected DataContext DataContext
        {
            get
            {
                return _dataContext.Value;
            }
        }

        public void Add(T entity)
        {
            DataContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            DataContext.Set<T>().Remove(entity);
        }

        public void Delete(int id)
        {
            T entity = new T()
            {
                Id = id
            };

            DataContext.Entry(entity).State = EntityState.Deleted;
        }

        public T GetById(int id)
        {
            return DataContext.Set<T>().FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<T> GetAllActive()
        {
            return DataContext.Set<T>().Where(e => e.IsActive);
        }

        public IEnumerable<T> GetAll()
        {
            return DataContext.Set<T>();
        }

        public void SaveChanges()
        {
            DataContext.SaveChanges();
        }
    }
}
