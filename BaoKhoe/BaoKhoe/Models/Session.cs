using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaoKhoe.Models
{
    public class Session
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Token của Session
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Thời gian tạo Session
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Thời hạn của session
        /// </summary>
        public DateTime ExpiredDate { get; set; }
        /// <summary>
        /// Ip tạo session
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// Chủ thể
        /// </summary>
        public User User { get; set; }
    }
}