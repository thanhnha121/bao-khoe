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
        public string FriendlyName { get; set; }
        public string Url { get; set; }
        public string Descriptions { get; set; }
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