using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaoKhoe.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(256), Required, Column(TypeName = "NVARCHAR")]
        public string Title { get; set; }
        /// <summary>
        /// Short title
        /// </summary>
        public string SubTitle { get; set; }
        /// <summary>
        /// Vắn tắt
        /// </summary>
        public string Headlines { get; set; }
        /// <summary>
        /// Nội dung
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Key word, tags,...
        /// </summary>
        [NotMapped]
        public List<Keyword> ListKeywords { get; set; }
        public string Keywords { get; set; }
        public string FriendlyKeywords { get; set; }
        /// <summary>
        /// Nguồn, clone từ trang nào
        /// </summary>
        public string Source { get; set; }
        public string SourceUrl { get; set; }
        /// <summary>
        /// Title thu dạng không dấu VD: title-dang-khong-dau
        /// </summary>
        public string FriendlyTitle { get; set; }
        /// <summary>
        /// Tên tác giả
        /// </summary>
        public string AuthorAlias { get; set; }
        /// <summary>
        /// Trạng thái bài viết
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Ảnh thu nhỏ
        /// </summary>
        public string Thumbnail { get; set; }
        [NotMapped]
        public List<Article> RelatedArticles { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public User CreatedBy { get; set; }
        /// <summary>
        /// Người chỉnh sửa lần cuối
        /// </summary>
        public User LastModifiedBy { get; set; }
        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Chỉnh sửa lần cuối bởi
        /// </summary>
        public DateTime LastModifiedAt { get; set; }
        /// <summary>
        /// Số lượt xem
        /// </summary>
        public int ViewCount { get; set; }
        public Category Category { get; set; }

        public Article()
        {
             ListKeywords = new List<Keyword>();
            RelatedArticles = new List<Article>();
        }

        public string GetDisplayTime()
        {
            return CreatedAt.ToString("HH:mm | dd/MM/yyyy");
        }
    }
}