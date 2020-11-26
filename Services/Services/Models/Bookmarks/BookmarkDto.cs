using ReadLater.Entities;

namespace ReadLater.Services.Models.Bookmarks
{
    public class BookmarkDto
    {
        public int ID { get; set; }
        public string URL { get; set; }
        public string ShortDescription { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }

        public static BookmarkDto FromBookmark(Bookmark bookmark)
        {
            return new BookmarkDto
            {
                Category = bookmark.Category.Name,
                CategoryId = bookmark.CategoryId.Value,
                ID = bookmark.ID,
                ShortDescription = bookmark.ShortDescription,
                URL = bookmark.URL
            };
        }
    }
}
