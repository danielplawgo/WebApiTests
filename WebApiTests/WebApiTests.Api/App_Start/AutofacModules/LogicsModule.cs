using Autofac;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiTests.Logic.Interfaces;

namespace WebApiTests.Api.App_Start.AutofacModules
{
    public class LogicsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ILogic<>).Assembly)
                .AsClosedTypesOf(typeof(ILogic<>))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(ILogic<>).Assembly)
                .AsClosedTypesOf(typeof(IValidator<>))
                .AsImplementedInterfaces();
        }
    }
}