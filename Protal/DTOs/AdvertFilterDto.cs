using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.DTOs
{
    public class AdvertFilterDto
    {
        public DateTimeOffset? From { get; set; }
        public DateTimeOffset? To { get; set; }
        public Guid? OwnerId { get; set; }
    }
}
