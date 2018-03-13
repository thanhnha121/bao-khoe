using System;
using System.ComponentModel.DataAnnotations;

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
        public string Type { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}