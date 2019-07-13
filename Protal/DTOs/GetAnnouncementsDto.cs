using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.DTOs
{
    public class GetAnnouncementsDto
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public AdvertFilterDto Filter { get; set; }
    }
}
