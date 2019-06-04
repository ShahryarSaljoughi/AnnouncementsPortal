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
using Portal.Helper;


namespace Portal.Controllers
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
            var phone = string.IsNullOrWhiteSpace(dto.PhoneNo) ? user.Phone : dto.PhoneNo;
            var newAnnouncement = new Announcement()
            {
                Text = dto.Text,
                Title = dto.Title,
                OwnerId = user.Id,
                Owner = user,
                PhoneNo = phone,
                CreationDateTimeOffset = DateTimeOffset.Now
            };
            Db.Set<Announcement>().Add(newAnnouncement);
            await Db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> GetAnnouncements([FromBody]GetAnnouncementsDto dto)
        {
            var ads = await Db.Set<Announcement>().Include(t => t.Owner)
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToListAsync();
            var result = 
                from ad in ads
                select new AnnouncementDto()
                {
                    Author = new TeacherDto()
                    {
                        Name = string.Join(' ', ad.Owner.Firstname, ad.Owner.Lastname),
                        Phone = ad.Owner.Phone,
                        TeacherId = ad.OwnerId,
                        ZnuUrl = ad.Owner.ZnuUrl,
                    },
                    PersianCreationTime = ad.CreationDateTimeOffset?.ToPersianDate(), //todo: convert to persian,
                    Text = ad.Text,
                    Title = ad.Title,
                    PhoneNo = ad.PhoneNo
                };
            return Ok(result);
        }
        
        private async Task<Teacher> GetCurrentUser()
        {
            var userId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)?.Value);
            var user = await Db.Set<Teacher>().FindAsync(userId);
            return user;
        }
    }
}
