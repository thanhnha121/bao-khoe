using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaoKhoe.Models
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50), Column(TypeName = "CHAR")]
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
        [MaxLength(512), Column(TypeName = "NVARCHAR")]
        public string Content { get; set; }
    }
}
