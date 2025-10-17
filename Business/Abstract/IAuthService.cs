using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<AdminUser> AdminUserLogin(AdminUserLoginDto adminUserLoginDto);
        IDataResult<AdminUser> AdminUserRegister(AdminUserRegisterDto adminUserRegisterDto, string password);
        IDataResult<Customer> CustomerLogin(CustomerLoginDto customerLoginDto);
        IDataResult<Customer> CustomerRegister(CustomerRegisterDto customerRegisterDto);
        IResult adminUserExist(string email);
        IResult customerExist(string email);
        IDataResult<AccessToken> CreateAccessTokenForAdminUser(AdminUser adminUser);
        IDataResult<AccessToken> CreateAccessTokenForCustomer(Customer customer);
    }
}
