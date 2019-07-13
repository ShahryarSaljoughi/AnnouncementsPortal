using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Entities;

namespace Portal.DTOs
{
    public class AnnouncementDto
    {
        public string Text { get; set; }
        public string Title { get; set; }
        public string PersianCreationTime { get; set; }
        public TeacherDto Author { get; set; }
        public string PhoneNo { get; set; }
        public string ImageUrl { get; set; }
        public Guid AnnouncementId { get; set; }
    }
}
