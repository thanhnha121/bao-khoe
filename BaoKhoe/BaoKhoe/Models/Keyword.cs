using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaoKhoe.Models
{
    public class Keyword
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(128), Column(TypeName = "NCHAR")]
        public string Title { get; set; }
        [MaxLength(128), Column(TypeName = "CHAR")]
        public string FriendlyTitle { get; set; }
        public int VisitCount { get; set; }
        [MaxLength(50), Column(TypeName = "NCHAR")]
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}