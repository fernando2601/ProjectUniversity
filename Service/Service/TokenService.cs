using Domain;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.Service
{
    public static class TokenService
    {
        public static string GenerateToken(Usuario usuario, JwtSettings jwtSettings)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            var tzBrasilia = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            // Obter a data e hora atual no fuso horário de São Paulo
            var now = TimeZoneInfo.ConvertTime(DateTime.Now, tzBrasilia);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Username.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Role.ToString()),
                }),
                // Configurar a expiração usando a data e hora no fuso horário de São Paulo
                Expires = now.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
