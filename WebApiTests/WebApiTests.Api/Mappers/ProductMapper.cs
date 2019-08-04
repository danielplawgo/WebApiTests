using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiTests.Api.ViewModels;
using WebApiTests.Models;

namespace WebApiTests.Api.Mappers
{
    public class ProductMapper : IMapper<Product, ProductViewModel>
    {
        public ProductViewModel Map(Product value)
        {
            return new ProductViewModel()
            {
                Id = value.Id,
                Name = value.Name
            };
        }

        public Product Map(ProductViewModel value, Product source = null)
        {
            if(source == null)
            {
                source = new Product();
            }

            source.Id = value.Id;
            source.Name = value.Name;

            return source;
        }
    }
}