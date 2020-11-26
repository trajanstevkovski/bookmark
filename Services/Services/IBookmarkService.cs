using System.Collections.Generic;
using ReadLater.Services.Models.Bookmarks;

namespace ReadLater.Services
{
    public interface IBookmarkService
    {
        IEnumerable<BookmarkDto> GetBookmarks(string category, string username);
        int CreateBookmark(CreateBookmark request, string username);
        BookmarkDto GetBookmarkById(int? id, string username);
        int EditBookmark(CreateBookmark request, string username);
        void DeleteBookmark(int id, string username);
    }
}