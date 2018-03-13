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
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        /// <summary>
        /// Trạng thái tài khoản
        /// </summary>
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}