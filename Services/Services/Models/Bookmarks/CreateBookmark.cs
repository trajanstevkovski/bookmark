using System.ComponentModel.DataAnnotations;

namespace ReadLater.Services.Models.Bookmarks
{
    public class CreateBookmark
    {
        public int? ID { get; set; }
        [Required]
        public string URL { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public int? CategoryId { get; set; }
    }
}
