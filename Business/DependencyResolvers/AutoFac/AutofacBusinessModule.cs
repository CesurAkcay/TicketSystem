using Autofac;
using AutoMapper;
using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Dtos;
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

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TicketProfile>();
                cfg.AddProfile<AdminUserProfile>();
                cfg.AddProfile<CustomerProfile>();

            })).AsSelf().SingleInstance();

            builder.Register(context => context.Resolve<MapperConfiguration>().CreateMapper())
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}
