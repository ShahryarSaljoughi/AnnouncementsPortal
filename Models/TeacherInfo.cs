using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table(nameof(TeacherInfo), Schema = "AP")]
    public class TeacherInfo
    {
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ZnuUrl { get; set; }
        // todo: add school and department : گروه آموزشی و دانشکده
    }
}
