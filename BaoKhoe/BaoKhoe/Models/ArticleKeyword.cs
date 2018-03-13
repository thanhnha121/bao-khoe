using System.ComponentModel.DataAnnotations;

namespace BaoKhoe.Models
{
    public class ArticleKeyword
    {
        [Key]
        public int Id { get; set; }
        public Keyword Keyword { get; set; }
        public Article Article { get; set; }
    }
}