using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.ApDbContext;
using Models.Entities;
using Portal.DTOs;
using Portal.Services.Contracts;
using Portal.Services.Implementations;

namespace Portal.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private APDbContext Db { get; set; }
        private IUserService UserService { get; set; }
        private IEmailSenderService EmailSenderService { get; set; }
        public AuthenticationController(APDbContext db, IUserService userService, IEmailSenderService emailService)
        {
            Db = db;
            UserService = userService;
            EmailSenderService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegistrationDto dto)
        {
            //todo: input validation logic
            var user = await Db.Set<Teacher>().FirstOrDefaultAsync(i => i.Email.Contains(dto.Email));
            if (user is null)
                return BadRequest();
            try
            {
                await UserService.Register(dto);
                await EmailSenderService.SendEmailVerificationLink(user.Id);
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
                return BadRequest(e);
            }

            var jwt = UserService.GenerateJwt(user);
            return Ok(new
            {
                jwt = jwt
            });
        }

        [HttpGet]
        public async Task<IActionResult> VerifyEmail(Guid id, string code)
        {
            var codeRow = await Db.Set<EmailVerificaionCode>().FirstOrDefaultAsync(i => i.UserId == id);
            var user = await Db.Set<Teacher>().FindAsync(id);
            if (user is null)
                return BadRequest();

            if (codeRow?.Code == code)
                user.AccountActivated = true;
            await Db.SaveChangesAsync();
            return Ok();

        }

    }
}