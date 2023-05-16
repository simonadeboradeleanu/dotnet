
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using dotnet.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using dotnet.Data;

namespace dotnet
{
    public class JwtAuthenticationManager
    {
        //key declaration
        private readonly string key;
        private IDictionary<string, string> users = new Dictionary<string, string>();

        /*private readonly IDictionary<string, string> users = new Dictionary<string, string>()
        { {"test", "password"}, {"test1", "password1"}, {"user", "assword"} };*/
        /*public JwtAuthenticationManager(DataContext context)
        {
            _context = context;
        }*/



        private readonly DataContext _context;


        public JwtAuthenticationManager(string key, DataContext _context)
        {
            this.key = key;
            this.users = _context.Users.ToDictionary(u => u.username, u => u.password);
        }


        public string Authenticate(string username, string password)
        {
            
            if (!users.Any(u => u.Key == username && u.Value == password))
            {
                return null;
            }
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
             
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature) 
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        
    }
}