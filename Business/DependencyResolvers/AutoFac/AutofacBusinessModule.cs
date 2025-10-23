using Autofac;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Dtos;
using Entities.MappingProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.AutoFac
{
    public class AutofacBusinessModule:Module
    { 
        override protected void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TicketSystemContext>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<TicketManager>().As<ITicketService>();
            builder.RegisterType<EfTicketDal>().As<ITicketDal>();

            builder.RegisterType<EfCustomerDal>().As<ICustomerDal>();
            builder.RegisterType<CustomerManager>().As<ICustomerService>();

            builder.RegisterType<AdminUserManager>().As<IAdminUserService>();
            builder.RegisterType<EfAdminUserDal>().As<IAdminUserDal>();

            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
            builder.RegisterType<AuthManager>().As<IAuthService>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

                builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

            // AutoMapper configuration
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TicketProfile());
                cfg.AddProfile(new AdminUserProfile());
                cfg.AddProfile(new CustomerProfile());
            })).AsSelf().SingleInstance();

            builder.Register(context =>
            {
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper();
            }).As<IMapper>().InstancePerLifetimeScope();
        }
    }
}
