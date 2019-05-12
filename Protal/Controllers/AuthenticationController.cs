using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.ApDbContext;
using Models.Entities;
using Protal.DTOs;
using Protal.Services.Contracts;
using Protal.Services.Implementations;

namespace Protal.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private APDbContext Db { get; set; }
        private IUserService UserService { get; set; }
        public AuthenticationController(APDbContext db, IUserService userService)
        {
            Db = db;
            UserService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationDto dto)
        {
            //todo: input validation logic

            try
            {
                await UserService.Register(dto);
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]RegistrationDto dto)
        {
            Teacher user;
            try
            {
                user = await UserService.Login(dto.Email, dto.Password);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            var jwt = UserService.GenerateJwt(user);
            return Ok(jwt);
        }

    }
}