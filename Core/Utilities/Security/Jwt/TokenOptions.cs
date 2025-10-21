using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Jwt
{
    public class TokenOptions
    {
        public string Audience { get; set; } //tokenin hitap edeceði yer
        public string Issuer { get; set; } //tokenin kim tarafýndan verildiði
        public int AccessTokenExpiration { get; set; } //tokenin ne kadar süre geçerli olacaðý
        public string SecurityKey { get; set; } //tokenin imzalanmasýnda kullanýlacak anahtar
    }
}
