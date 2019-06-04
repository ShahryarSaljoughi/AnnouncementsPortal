using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.ApDbContext;
using Models.Entities;
using Portal.DTOs;
using Portal.Helper;
using Portal.Services.Contracts;

namespace Portal.Services.Implementations
{
    public class UserService: IUserService
    {
        private APDbContext Db { get; set; }
        private IConfiguration Configuration { get; set; }
        public UserService(APDbContext db, IConfiguration config)
        {
            Configuration = config;
            Db = db;
        }

        public async Task Register(RegistrationDto dto)
        {
            var email = dto.Email;

            var newTeacher = await Db.Set<Teacher>().FirstOrDefaultAsync(t => t.Email.Contains(email));
            if (newTeacher is null)
                throw new AppException();

            var passwordBytes = Encoding.UTF8.GetBytes(dto.Password);
            byte[] passwordHash, passwordSalt;
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(passwordBytes);
            }
            newTeacher.PasswordHash = passwordHash;
            newTeacher.PasswordSalt = passwordSalt;
            newTeacher.AccountActivated = true;

            await Db.SaveChangesAsync();
        }

        public async Task<Teacher> Login(string email, string password)
        {
            var user = await GetUserByEmail(email);
            if (user is null)
                throw new AppException("user does not exist") { ExceptionReason = ExceptionReason.LoginFailed};
            if (user.AccountActivated == false)
            {
                throw new AppException("account has not been activated"){ExceptionReason = ExceptionReason.LoginFailed};
            }
            var doesPasswordMatch = VerifyPassword(password, user.PasswordHash, user.PasswordSalt);

            if (!doesPasswordMatch)
                throw new AppException("Email or Password is incorrect")
                    {ExceptionReason = ExceptionReason.LoginFailed};

            return user;
        }

        public string GenerateJwt(Teacher user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                Configuration["JwtSecret"]
            ));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.GivenName, user.Firstname +" "+ user.Lastname),
            };
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = "AnnouncementsPortal",
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(1),//AddDays(10),
                SigningCredentials = signingCredentials,
                Audience = "AnnouncementsPortal",
                NotBefore = DateTimeOffset.Now.DateTime
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return jwt;
        }

        private async Task<Teacher> GetUserByEmail(string email)
        {
            var user = await Db.Set<Teacher>().FirstOrDefaultAsync(t => t.Email.Contains(email));
            return user;
        }
        

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedPasswordHash.Length; i++)
                {
                    if (computedPasswordHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public async Task<Teacher> GetById(Guid userId)
        {
            var teacher = await Db.Set<Teacher>().FindAsync(userId);
            return teacher;
        }

        
    }
}
