using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTests.Logic.Interfaces;
using WebApiTests.Logic.Repositories;
using WebApiTests.Models;

namespace WebApiTests.Logic.Products
{
    public class ProductLogic : IProductLogic
    {
        private Lazy<IProductRepository> _repository;

        protected IProductRepository Repository
        {
            get { return _repository.Value; }
        }

        private Lazy<IValidator<Product>> _validator;

        protected IValidator<Product> Validator
        {
            get { return _validator.Value; }
        }

        public ProductLogic(Lazy<IProductRepository> repository,
            Lazy<IValidator<Product>> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public Result<IEnumerable<Product>> GetAllActive()
        {
            return Result.Ok(Repository.GetAllActive());
        }

        public Result<Product> GetById(int id)
        {
            var product = Repository.GetById(id);

            if(product == null)
            {
                return Result.Error<Product>($"Product with id {id} doesn't exist.");
            }

            return Result.Ok(product);
        }

        public Result<Product> Add(Product product)
        {
            if(product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var validationResult = Validator.Validate(product);

            if(validationResult.IsValid == false)
            {
                return Result.Error<Product>(validationResult.Errors);
            }

            Repository.Add(product);

            Repository.SaveChanges();

            return Result.Ok(product);
        }

        public Result<Product> Update(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if(product.Id == 3)
            {
                throw new Exception("special product");
            }

            var validationResult = Validator.Validate(product);

            if (validationResult.IsValid == false)
            {
                return Result.Error<Product>(validationResult.Errors);
            }

            Repository.SaveChanges();

            return Result.Ok(product);
        }

        public Result Delete(Product product)
        {
            Repository.Delete(product);

            Repository.SaveChanges();

            return Result.Ok();
        }
    }
}
