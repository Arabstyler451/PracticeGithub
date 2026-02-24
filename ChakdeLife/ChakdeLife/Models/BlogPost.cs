using System.ComponentModel.DataAnnotations;

namespace ChakdeLife.Models
{
    public class BlogPost
    {
        public int BlogPostId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public DateTime PublishedDate { get; set; }

        public bool IsPublished { get; set; }
    }
}