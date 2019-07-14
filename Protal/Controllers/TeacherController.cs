using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.ApDbContext;
using Models.Entities;
using Portal.DTOs;


namespace Portal.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TeacherController : ControllerBase
    {
        private APDbContext Db { get; set; }
        public TeacherController(APDbContext db)
        {
            Db = db;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetTeacherInfo()
        {
            var teacher = await GetCurrentUserAsync();
            var outputDto = new TeacherDto()
            {
                Department = teacher.Department?.PersianName,
                ZnuUrl = teacher.ZnuUrl,
                College = teacher.Department?.College.ToString(),
                Name = teacher.GetFullName(),
                FirstName = teacher.Firstname,
                LastName = teacher.Lastname,
                Phone = teacher.Phone,
                TeacherId = teacher.Id
            };

            return Ok(outputDto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateProfile([FromBody]UpdateProfileDto dto)
        {
            if (dto is null)
            {
                return BadRequest("پارامتر ارسالی خالی است");
            }

            var user = await GetCurrentUserAsync();
            user.Phone = dto.Phone;
            user.ZnuUrl = dto.ZnuUrl;
            user.Firstname = dto.FirstName;
            user.Lastname = dto.LastName;
            await Db.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Guid>> GetMyId()
        {
            var user = await GetCurrentUserAsync();
            return Ok(user.Id);
        }

        private async Task<Teacher> GetCurrentUserAsync()
        {
            var userId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)?.Value);
            var user = await Db.Set<Teacher>().FindAsync(userId);
            return user;
        }
    }
}
