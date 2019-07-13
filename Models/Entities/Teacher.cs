using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    [Table(nameof(Teacher), Schema = "AP")]
    public class Teacher
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ZnuUrl { get; set; }
        public string AcademicRank { get; set; } //مرتبه علمی
        public bool AccountActivated { get; set;}
        public Department Department { get; set; }
        [ForeignKey(nameof(Department))]
        public Guid? DepartmentId { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public string GetFullName()
        {
            return string.Join(' ', Firstname, Lastname);
        }

        // todo: add school and department : گروه آموزشی و دانشکده
    }
}
