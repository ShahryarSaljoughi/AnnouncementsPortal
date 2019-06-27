using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.Entities
{
    [Table(nameof(EmailVerificaionCode), Schema = "AP")]
    public class EmailVerificaionCode
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public Guid UserId { get; set; }
    }
}
