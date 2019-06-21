using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.DTOs
{
    public class TeacherDto
    {
        public Guid? TeacherId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ZnuUrl { get; set; }
        public string College { get; set; }
        public string Department { get; set; }

    }
}
