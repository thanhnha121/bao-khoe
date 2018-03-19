using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BaoKhoe.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50), Column(TypeName = "NCHAR")]
        public string FullName { get; set; }
        [MaxLength(20), Column(TypeName = "CHAR")]
        public string Username { get; set; }
        [MaxLength(128), Column(TypeName = "CHAR")]
        public string Password { get; set; }
        [MaxLength(50), Column(TypeName = "CHAR")]
        public string Email { get; set; }
        /// <summary>
        /// Trạng thái tài khoản
        /// </summary>
        [MaxLength(50), Column(TypeName = "NCHAR")]
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}