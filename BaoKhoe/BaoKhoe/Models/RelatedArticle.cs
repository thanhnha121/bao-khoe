using System;
using System.ComponentModel.DataAnnotations;

namespace BaoKhoe.Models
{
    public class RelatedArticle
    {
        [Key]
        public int Id { get; set; }
        public int Index { get; set; }
        public string Type { get; set; }
        public Article Origin { get; set; }
        public Article Related{ get; set; }
        public DateTime CreatedAt { get; set; }
    }
}