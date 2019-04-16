using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    [Table(nameof(Post), Schema="AP")]
    class Post
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTimeOffset CreationDateTime { get; set; }

    }
}
