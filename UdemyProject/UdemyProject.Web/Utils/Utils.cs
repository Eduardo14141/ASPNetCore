using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace UdemyProject.Web.Utils
{
    public class Utils
    {
        public static void EncryptPassword(string password, out byte[] password_hash, out byte[] password_salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                password_hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                password_salt = hmac.Key;
            }
        }
        public static bool VerifyLogin(string password, byte[] password_hash, byte[] password_salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(password_salt))
            {
                var encrypted_password = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return new ReadOnlySpan<byte>(password_hash).SequenceEqual(new ReadOnlySpan<byte>(encrypted_password));
            }
        }

        public static string generateLoginToken(List<Claim> claims, IConfiguration _config)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(180),
                signingCredentials: creds,
                claims: claims
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
