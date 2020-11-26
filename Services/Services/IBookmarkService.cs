using System.Collections.Generic;
using ReadLater.Services.Models.Bookmarks;

namespace ReadLater.Services
{
    public interface IBookmarkService
    {
        IEnumerable<BookmarkDto> GetBookmarks(string category);
        int CreateBookmark(CreateBookmark request);
        BookmarkDto GetBookmarkById(int? id);
        int EditBookmark(CreateBookmark request);
        void DeleteBookmark(int id);
    }
}