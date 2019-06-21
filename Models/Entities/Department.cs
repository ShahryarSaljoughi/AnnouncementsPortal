using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Models.Enums;

namespace Models.Entities
{
    [Table(nameof(Department), Schema = "AP")]
    public class Department // this is a look-up table
    {
        public Guid Id { get; set; }
        public string PersianName { get; set; }
        public College College { get; set; }

    }
}
