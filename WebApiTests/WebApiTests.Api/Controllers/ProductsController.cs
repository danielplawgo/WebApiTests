using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiTests.Api.Mappers;
using WebApiTests.Api.ViewModels;
using WebApiTests.Logic;
using WebApiTests.Logic.Interfaces;
using WebApiTests.Models;

namespace WebApiTests.Api.Controllers
{
    public class ProductsController : BaseApiController
    {
        private Lazy<IProductLogic> _logic;

        protected IProductLogic Logic
        {
            get { return _logic.Value; }
        }

        private Lazy<IMapper<Product, ProductViewModel>> _mapper;

        protected IMapper<Product, ProductViewModel> Mapper
        {
            get { return _mapper.Value; }
        }

        public ProductsController(Lazy<IProductLogic> logic,
            Lazy<IMapper<Product, ProductViewModel>> mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }

        public IHttpActionResult Get()
        {
            var result = Logic.GetAllActive();

            return MapList(result, Mapper);
        }

        public IHttpActionResult Get(int id)
        {
            var result = Logic.GetById(id);

            return Map(result, Mapper);
        }

        public IHttpActionResult Post([FromBody]ProductViewModel viewModel)
        {
            var product = Mapper.Map(viewModel);

            var result = Logic.Add(product);

            return Map(result, Mapper);
        }

        public IHttpActionResult Put(int id, [FromBody]ProductViewModel viewModel)
        {
            var result = Logic.GetById(id);

            if(result.Success == false)
            {
                return Map(result, Mapper);
            }

            viewModel.Id = id;
            var product = Mapper.Map(viewModel, result.Value);

            result = Logic.Update(product);

            return Map(result, Mapper);
        }

        public IHttpActionResult Delete(int id)
        {
            var result = Logic.GetById(id);

            if (result.Success == false)
            {
                return Map(result, Mapper);
            }

            var deleteResult = Logic.Delete(result.Value);

            return Map(deleteResult);
        }
    }
}
