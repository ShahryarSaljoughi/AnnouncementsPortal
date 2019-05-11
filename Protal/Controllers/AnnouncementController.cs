using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.ApDbContext;
using Protal.DTOs;
using Models.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Protal.Controllers
{
    [Route("api/[controller]")]
    public class AnnouncementController : ControllerBase
    {

        public APDbContext Db { get; set; }

        public AnnouncementController(APDbContext db)
        {
            Db = db;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post(AnnouncementDto dto)
        {    
            var newAnnouncement = new Announcement()
            {
                Text = dto.Text,
                Title = dto.Title,
                OwnerId = Db.Set<Teacher>().First().Id,
                Owner = Db.Set<Teacher>().First(),
                PhoneNo = string.Empty
            };
            Db.Set<Announcement>().Add(newAnnouncement);
            Db.SaveChangesAsync();   
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
