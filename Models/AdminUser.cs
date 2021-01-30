using System;
using System.Collections.Generic;

#nullable disable

namespace trailers_api.Models
{
    public partial class AdminUser
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] SheduleDate { get; set; }
    }
}
