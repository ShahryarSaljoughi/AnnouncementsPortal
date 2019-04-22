using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    [Table(nameof(TeacherInfo), Schema = "AP")]
    public class TeacherInfo
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ZnuUrl { get; set; }

        public string AcademicRank { get; set; } //مرتبه علمی

        // todo: add school and department : گروه آموزشی و دانشکده
    }
}
