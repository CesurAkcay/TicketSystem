using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AdminUserManager : IAdminUserService
    {
        IAdminUserDal _adminUserDal;
        public AdminUserManager(IAdminUserDal adminUserDal)
        {
            _adminUserDal = adminUserDal;
        }
        public void AdminUserAdd(AdminUser adminUser)
        {
            _adminUserDal.Add(adminUser);
        }

        public AdminUser GetByMail(string email)
        {
            return _adminUserDal.Get(u => u.Email == email);
        }
    }
}
