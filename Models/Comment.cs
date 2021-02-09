using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace trailers_api.Models
{
    public partial class Comment
    {
        public Comment()
        {
            InverseCommentNavigation = new HashSet<Comment>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [Column("comment", TypeName = "text")]
        public string Comment1 { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("trailer_id")]
        public int TrailerId { get; set; }
        [Column("comment_id")]
        public int? CommentId { get; set; }
        [Column("created_at", TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(CommentId))]
        [InverseProperty(nameof(Comment.InverseCommentNavigation))]
        public virtual Comment CommentNavigation { get; set; }
        [ForeignKey(nameof(TrailerId))]
        [InverseProperty("Comments")]
        public virtual Trailer Trailer { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Comments")]
        public virtual User User { get; set; }
        [InverseProperty(nameof(Comment.CommentNavigation))]
        public virtual ICollection<Comment> InverseCommentNavigation { get; set; }
    }
}
