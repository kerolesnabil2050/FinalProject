using Autofac;
using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Repositry;
using Ecommerce.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class AutoFacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<Context>().InstancePerLifetimeScope();
        builder.RegisterType<UnitOfWork>().InstancePerLifetimeScope();

        builder.RegisterGeneric(typeof(Reposity<,>)).As(typeof(IgenricRepo<,>)).InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(typeof(MediatorService).Assembly).InstancePerLifetimeScope();

   
    }
}
