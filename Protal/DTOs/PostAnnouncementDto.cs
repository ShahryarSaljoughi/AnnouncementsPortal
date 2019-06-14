using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.DTOs
{
    public class PostAnnouncementDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string PhoneNo { get; set; }
        public Guid? ImageFileId { get; set; }
    }
}
