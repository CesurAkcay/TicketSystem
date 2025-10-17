using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("loginAdminUser")]
        public ActionResult AdminUserLogin(AdminUserLoginDto adminUserLoginDto)
        {
            var userToLogin = _authService.AdminUserLogin(adminUserLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessTokenForAdminUser(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("loginCustomer")]
        public ActionResult CustomerLogin(CustomerLoginDto customerLoginDto)
        {
            var customerToLogin = _authService.CustomerLogin(customerLoginDto);
            if (!customerToLogin.Success)
            {
                return BadRequest(customerToLogin.Message);
            }

            var result = _authService.CreateAccessTokenForCustomer(customerToLogin.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("registerAdminUser")]
        public ActionResult AdminUserRegister(AdminUserRegisterDto adminUserRegisterDto)
        {
            var userExists = _authService.adminUserExist(adminUserRegisterDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }
            var registerResult = _authService.AdminUserRegister(adminUserRegisterDto, adminUserRegisterDto.Password);
            if (registerResult.Success)
            {
                return Ok(registerResult.Data);
            }
            return BadRequest(registerResult.Message);
        }

        [HttpPost("registerCustomer")]
        public ActionResult CustomerRegister(CustomerRegisterDto customerRegisterDto)
        {
            var customerExists = _authService.customerExist(customerRegisterDto.Email);
            if (!customerExists.Success)
            {
                return BadRequest(customerExists.Message);
            }
            var customerRegisterResult = _authService.CustomerRegister(customerRegisterDto);
            if (customerRegisterResult.Success)
            {
                return Ok(customerRegisterResult.Data);
            }
            return BadRequest(customerRegisterResult.Message);
        }
    }
}
