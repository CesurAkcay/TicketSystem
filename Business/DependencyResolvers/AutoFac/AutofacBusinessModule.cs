using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
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
            builder.RegisterType<TicketManager>().As<ITicketService>();
            builder.RegisterType<EfTicketDal>().As<ITicketDal>();     
            
            builder.RegisterType<EfCustomerDal>().As<ICustomerDal>();
            builder.RegisterType<CustomerManager>().As<ICustomerService>();

            builder.RegisterType<AdminUserManager>().As<IAdminUserService>();
            builder.RegisterType<EfAdminUserDal>().As<IAdminUserDal>();
        }
    }
}
