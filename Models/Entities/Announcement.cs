using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Models.Entities
{
    [Table(nameof(Announcement), Schema = "AP")]
    public class Announcement
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Text { get; set; }
        public string PhoneNo { get; set; } // comma separated

        public Teacher Owner { get; set; }
        [ForeignKey(nameof(Owner))]
        public Guid? OwnerId { get; set; }
        public DateTimeOffset? CreationDateTimeOffset { get; set; }
    }
}
