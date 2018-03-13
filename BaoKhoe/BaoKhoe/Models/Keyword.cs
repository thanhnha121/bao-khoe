using System;
using System.ComponentModel.DataAnnotations;

namespace BaoKhoe.Models
{
    public class Keyword
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string FriendlyTitle { get; set; }
        public int VisitCount { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}