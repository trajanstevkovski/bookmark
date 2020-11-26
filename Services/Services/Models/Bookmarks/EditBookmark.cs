using ReadLater.Services.Models.Categories;
using System.Collections.Generic;

namespace ReadLater.Services.Models.Bookmarks
{
    public class EditBookmark
    {
        public BookmarkDto Bookmark { get; set; }
        public IEnumerable<CategoryDto> Categories { get; set; }
    }
}
