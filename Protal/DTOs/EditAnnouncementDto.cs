using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace Portal.DTOs
{
    public class EditAnnouncementDto
    {
        public Guid AdvertId { get; set; }
        public PostAnnouncementDto NewValues { get; set; }
    }
}
