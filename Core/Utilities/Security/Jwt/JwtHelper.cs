using Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;

        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public AccessToken CreateToken<T>(T user, List<string> roles) where T : IEntity
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, roles);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken<T>(TokenOptions tokenOptions, T user,
            SigningCredentials signingCredentials, List<string> roles) where T : IEntity
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, roles),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims<T>(T user, List<string> roles) where T : IEntity
        {
            var claims = new List<Claim>();

            // Get user properties dynamically
            var userType = user.GetType();
            var idProperty = userType.GetProperty("Id");
            var emailProperty = userType.GetProperty("Email");
            var fullNameProperty = userType.GetProperty("FullName");

            if (idProperty != null)
            {
                var idValue = idProperty.GetValue(user);
                claims.Add(new Claim(ClaimTypes.NameIdentifier, idValue?.ToString() ?? ""));
            }

            if (emailProperty != null)
            {
                var emailValue = emailProperty.GetValue(user);
                claims.Add(new Claim(ClaimTypes.Email, emailValue?.ToString() ?? ""));
            }

            if (fullNameProperty != null)
            {
                var nameValue = fullNameProperty.GetValue(user);
                claims.Add(new Claim(ClaimTypes.Name, nameValue?.ToString() ?? ""));
            }

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}
