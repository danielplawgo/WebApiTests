using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiTests.Api.Mappers;

namespace WebApiTests.Api.App_Start.AutofacModules
{
    public class MappersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(GetType().Assembly)
                .AsClosedTypesOf(typeof(IMapper<,>))
                .AsImplementedInterfaces();
        }
    }
}