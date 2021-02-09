using System;
using System.Collections.Generic;

namespace trailers_api.Models.DTO
{
    public class TrailerDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Url { get; set; }
        public string ImgUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}