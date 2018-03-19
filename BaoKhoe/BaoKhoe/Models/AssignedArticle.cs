using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaoKhoe.Models
{
    public class AssignedArticle
    {
        [Key]
        public int Id { get; set; }
        public Article Article { get; set; }
        public int Index { get; set; }
        /// <summary>
        /// Loại bài đăng: hot || trend
        /// </summary>
        [MaxLength(20), Column(TypeName = "NCHAR")]
        public string Type { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}