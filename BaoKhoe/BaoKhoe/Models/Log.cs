using System;
using System.ComponentModel.DataAnnotations;

namespace BaoKhoe.Models
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }
    }
}