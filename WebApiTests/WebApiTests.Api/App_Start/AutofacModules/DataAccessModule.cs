using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiTests.DataAccess;
using WebApiTests.Logic.Repositories;

namespace WebApiTests.Api.App_Start.AutofacModules
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataContext>()
                .InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(DataContext).Assembly)
                .AsClosedTypesOf(typeof(IRepository<>))
                .AsImplementedInterfaces();

            builder.RegisterType<DatabaseRestoreService>()
                .AsImplementedInterfaces();
        }
    }
}