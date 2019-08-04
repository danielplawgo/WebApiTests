using FizzWare.NBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTests.Logic.Repositories;
using WebApiTests.Models;

namespace WebApiTests.DataAccess
{
    public class ProductRepository : IProductRepository
    {
        private static IList<Product> _data;

        static ProductRepository()
        {
            ResetTestData();
        }

        public static void ResetTestData()
        {
            _data = Builder<Product>.CreateListOfSize(5)
                .Build();
        }

        public void Add(Product entity)
        {
            entity.Id = _data.Select(p => p.Id).Max() + 1;
            _data.Add(entity);
        }

        public void Delete(Product entity)
        {
            _data.Remove(entity);
        }

        public void Delete(int id)
        {
            var entity = _data.FirstOrDefault(e => e.Id == id);

            if(entity == null)
            {
                return;
            }

            Delete(entity);
        }

        public IEnumerable<Product> GetAll()
        {
            return _data;
        }

        public IEnumerable<Product> GetAllActive()
        {
            return _data.Where(e => e.IsActive);
        }

        public Product GetById(int id)
        {
            return _data.FirstOrDefault(e => e.Id == id);
        }

        public void SaveChanges()
        {            
        }
    }
}
