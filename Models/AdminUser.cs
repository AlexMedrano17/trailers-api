using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace trailers_api.Models
{
    [Table("Admin_user")]
    [Index(nameof(Username), IsUnique = true)]
    public partial class AdminUser
    {
        [Key]
        [Column("ID", TypeName = "INT")]
        public long Id { get; set; }
        [Required]
        [Column("username", TypeName = "VARCHAR (50)")]
        public string Username { get; set; }
        [Required]
        [Column("password", TypeName = "VARCHAR")]
        public string Password { get; set; }
        [Required]
        [Column("shedule_date", TypeName = "DATETIME")]
        public byte[] SheduleDate { get; set; }
    }
}
