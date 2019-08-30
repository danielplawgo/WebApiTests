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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(Lazy<DataContext> dataContext)
            : base(dataContext)
        {

        }        
    }
}
