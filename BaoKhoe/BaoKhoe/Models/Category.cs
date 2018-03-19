using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaoKhoe.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Tên của Cate
        /// </summary>
        public string Name { get; set; }
        [MaxLength(50), Column(TypeName = "CHAR")]
        public string FriendlyName { get; set; }
        [MaxLength(50), Column(TypeName = "CHAR")]
        public string Url { get; set; }
        public string Descriptions { get; set; }
        [MaxLength(20), Column(TypeName = "NCHAR")]
        public string Status { get; set; }
        public List<Category> SubCategories { get; set; }
        public bool IsSubCategory { get; set; }
        [NotMapped]
        public List<Article> Articles { get; set; }
        [NotMapped]
        public List<Article> TopHotArticles { get; set; }

        public Category()
        {
            SubCategories = new List<Category>();
            Articles = new List<Article>();
            TopHotArticles = new List<Article>();
        }
    }
}