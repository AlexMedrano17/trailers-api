using System;
using System.Collections.Generic;

#nullable disable

namespace trailers_api.Models
{
    public partial class Trailer
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long Genre { get; set; }
        public string Year { get; set; }
        public string Url { get; set; }
        public string ImgUrl { get; set; }
        public byte[] SheduleDate { get; set; }

        public virtual Genre GenreNavigation { get; set; }
    }
}
