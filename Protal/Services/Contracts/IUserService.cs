using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Entities;
using Portal.DTOs;

namespace Portal.Services.Contracts
{
    public interface IUserService
    {
        Task<Teacher> GetById(Guid userId);
        Task Register(RegistrationDto dto);
        Task<Teacher> Login(string email, string password);
        string GenerateJwt(Teacher user);
    }
}
