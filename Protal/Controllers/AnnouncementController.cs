using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.ApDbContext;
using Models.Entities;
using Models.Enums;
using Portal.DTOs;
using Portal.Helper;


namespace Portal.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AnnouncementController : ControllerBase
    {
        public APDbContext Db { get; set; }
        public IConfiguration Configuration { get; }
        public AnnouncementController(APDbContext db, IConfiguration configuration)
        {
            Db = db;
            Configuration = configuration;
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostNewAnnouncement([FromBody] PostAnnouncementDto dto)
        {
            var user = await GetCurrentUserAsync();
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
                CreationDateTimeOffset = DateTimeOffset.Now,
                FileId = dto.ImageFileId
            };
            Db.Set<Announcement>().Add(newAnnouncement);
            await Db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> GetAnnouncements([FromBody]GetAnnouncementsDto dto)
        {
            IQueryable<Announcement> ads = Db.Set<Announcement>().Include(t => t.Owner);
                

            ads = ApplyFilters(ads, dto.Filter);

            var listOfAds = await ads
                .Include(a => a.Owner.Department)
                .Include(t => t.File)
                .OrderBy(i => i.CreationDateTimeOffset)
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToListAsync();

            var result = 
                from ad in listOfAds
                select new AnnouncementDto()
                {
                    Author = new TeacherDto()
                    {
                        Name = string.Join(' ', ad.Owner.Firstname, ad.Owner.Lastname),
                        Phone = ad.Owner?.Phone,
                        TeacherId = ad.OwnerId,
                        ZnuUrl = ad.Owner?.ZnuUrl,
                        Department = ad.Owner?.Department?.PersianName,
                        College = ad.Owner?.Department?.College.GetPersianTranslation()
                    },
                    PersianCreationTime = ad.CreationDateTimeOffset?.ToPersianDate(), //todo: convert to persian,
                    Text = ad.Text,
                    Title = ad.Title,
                    PhoneNo = ad.PhoneNo,
                    ImageUrl = ad.File is null ? null: $"/{ad.File?.FileName}",
                    AnnouncementId = ad.Id
                };
            return Ok(result);
        }

        private IQueryable<Announcement> ApplyFilters(IQueryable<Announcement> ads, AdvertFilterDto dtoFilter)
        {
            if (dtoFilter is null) return ads;
            if (dtoFilter.From.HasValue)
            {
                ads = ads.Where(i =>
                    (i.CreationDateTimeOffset.HasValue && i.CreationDateTimeOffset.Value > dtoFilter.From )
                    || !i.CreationDateTimeOffset.HasValue);
            }
            
            if (dtoFilter.To.HasValue)
            {
                ads = ads.Where(i =>
                    (i.CreationDateTimeOffset.HasValue && i.CreationDateTimeOffset.Value < dtoFilter.To)
                    || !i.CreationDateTimeOffset.HasValue);
            }
            
            if (dtoFilter.OwnerId.HasValue)
            {
                ads = ads.Where(i => i.OwnerId == dtoFilter.OwnerId);
            }
            return ads;
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UploadFile(List<IFormFile> uploadedFile)
        {
            long size = uploadedFile.Sum(f => f.Length);
            
            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in uploadedFile)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            var fileName = GetFileName(uploadedFile.First());
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                fileName
            );
            System.IO.File.Move(filePath, path);

            var newFile = new UploadedFile()
            {
                CreationDateTimeOffset = DateTimeOffset.Now,
                Id = Guid.NewGuid(),
                FileName = fileName,
                PhysicalPath = path
            };  
            Db.Add(newFile);
            await Db.SaveChangesAsync();

            return Ok(new { newFile.Id, newFile.FileName, Url=$"/{newFile.FileName}"});
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditAnnouncement([FromBody]EditAnnouncementDto dto)
        {
            if (dto is null)
                return BadRequest("مقادیر ارسالی معتبر نیست");
            var advert = await Db.Set<Announcement>().FindAsync(dto.AdvertId);
            if (advert is null)
                return BadRequest("آگهی یافت نشد");

            if (!string.IsNullOrWhiteSpace(dto.NewValues.PhoneNo))
            {
                advert.PhoneNo = dto.NewValues.PhoneNo;
            }

            if (!string.IsNullOrWhiteSpace(dto.NewValues.Text))
            {
                advert.Text = dto.NewValues.Text;
            }

            if (!string.IsNullOrWhiteSpace(dto.NewValues.Title))
            {
                advert.Title = dto.NewValues.Title;
            }

            await Db.SaveChangesAsync();
            return Ok(advert);
        }


        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteAnnouncement([FromBody]DeleteAnnouncementDto dto)
        {
            var user = await GetCurrentUserAsync();
            if (dto?.AnnouncementId is null)
                return BadRequest("مقادیر ارسالی معتبر نیست");
            var advert = await Db.Set<Announcement>().FindAsync(dto.AnnouncementId);
            if (advert is null)
                return BadRequest($"آگهی با آیدی {dto.AnnouncementId} یافت نشد!");
            if (advert.OwnerId != user.Id)
                return BadRequest("اجازه پاک کردن این آگهی را ندارید.");
            Db.Remove(advert);
            await Db.SaveChangesAsync();
            return Ok();
        }
        private string GetFileName(IFormFile file)
        {
            var fileName = $"{DateTimeOffset.UtcNow.Ticks}_{file.FileName.Replace(' ', '_').ToLower()}";
            return fileName;
        }
        private async Task<Teacher> GetCurrentUserAsync()
        {
            var userId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)?.Value);
            var user = await Db.Set<Teacher>().FindAsync(userId);
            return user;
        }
    }

    public class CreateAnnouncementDto
    {
        public IFormFile uploadedFile { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string PhoneNo { get; set; }
    }
}
