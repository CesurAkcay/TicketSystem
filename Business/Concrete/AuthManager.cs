using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IAdminUserService _adminUserService;
        private ICustomerService _customerService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IAdminUserService adminUserService, ICustomerService customerService, ITokenHelper tokenHelper)
        {
            _adminUserService = adminUserService;
            _customerService = customerService;
            _tokenHelper = tokenHelper;
        }
        public IDataResult<AdminUser> AdminUserLogin(AdminUserLoginDto adminUserLoginDto)
        {
            var userToCheck = _adminUserService.GetByMail(adminUserLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<AdminUser>(Messages.UserNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(adminUserLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<AdminUser>(Messages.PasswordError);
            }

            return new SuccessDataResult<AdminUser>(userToCheck, Messages.SuccesfullAdminLogin);
        }
        public IDataResult<Customer> CustomerLogin(CustomerLoginDto customerLoginDto)
        {
            var customerToCheck = _customerService.GetByMail(customerLoginDto.Email);
            if (customerToCheck == null)
            {
                return new ErrorDataResult<Customer>(Messages.CustomerNotFound);
            }
            return new SuccessDataResult<Customer>(customerToCheck, Messages.SuccesfullCustomerLogin);
        }

        public IDataResult<AdminUser> AdminUserRegister(AdminUserRegisterDto adminUserRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var adminUser = new AdminUser
            {
                Email = adminUserRegisterDto.Email,
                FullName = adminUserRegisterDto.FullName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                IsActive = true //örnek önce onaylanıp sonra aktif olacaksa false olmalı
            };
            _adminUserService.AdminUserAdd(adminUser);
            return new SuccessDataResult<AdminUser>(adminUser, Messages.UserRegistered);
        }
        public IDataResult<Customer> CustomerRegister(CustomerRegisterDto customerRegisterDto)
        {
            var customer = new Customer
            {
                Email = customerRegisterDto.Email,
                FullName = customerRegisterDto.FullName,
            };
            _customerService.Add(customer);
            return new SuccessDataResult<Customer>(customer, Messages.CustomerRegistered);
        }
        public IResult customerExist(string email)
        {
            if (_customerService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExist);
            }
            return new SuccessResult();
        }

        public IResult adminUserExists(string email)
        {
            if (_adminUserService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExist);
            }
            return new SuccessResult();
        }

        public IResult adminUserExist(string email)
        {
            if (_adminUserService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExist);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessTokenForAdminUser(AdminUser adminUser)
        {
            var roles = new List<string> { "Admin" };
            var accessToken = _tokenHelper.CreateToken(adminUser, roles);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public IDataResult<AccessToken> CreateAccessTokenForCustomer(Customer customer)
        {
            var roles = new List<string> { "Customer" };
            var accessToken = _tokenHelper.CreateToken(customer, roles);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
    }
}
