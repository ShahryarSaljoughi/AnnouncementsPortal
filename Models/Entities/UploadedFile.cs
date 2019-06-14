using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.Entities
{
    [Table(nameof(UploadedFile), Schema = "AP")]
    public class UploadedFile
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string PhysicalPath { get; set; }
        public DateTimeOffset CreationDateTimeOffset { get; set; } 
    }
}
