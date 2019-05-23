using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.ApDbContext;
using Protal.DTOs;
using Models.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Protal.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AnnouncementController : ControllerBase
    {

        public APDbContext Db { get; set; }

        public AnnouncementController(APDbContext db)
        {
            Db = db;
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostNewAnnouncement([FromBody]CreateAnnouncementDto dto)
        {
            var user = await GetCurrentUser();
            if (user is null)
            {
                return BadRequest("there were problems in authenticating the client ");
            }
            var newAnnouncement = new Announcement()
            {
                Text = dto.Text,
                Title = dto.Title,
                OwnerId = user.Id,
                Owner = user,
                PhoneNo = string.Empty
            };
            Db.Set<Announcement>().Add(newAnnouncement);
            await Db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> GetAnnouncements([FromBody]GetAnnouncementsDto dto)
        {
            var ads = await Db.Set<Announcement>().Skip((dto.PageNumber - 1) * dto.PageSize).Take(dto.PageSize).ToListAsync();
            return Ok(ads);
        }
        
        private async Task<Teacher> GetCurrentUser()
        {
            var userId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)?.Value);
            var user = await Db.Set<Teacher>().FindAsync(userId);
            return user;
        }
    }
}
