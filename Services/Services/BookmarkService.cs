using System;
using System.Collections.Generic;
using System.Linq;
using ReadLater.Entities;
using ReadLater.Repository;
using ReadLater.Services.Models.Bookmarks;
using ReadLater.Services.Models.Exceptions;

namespace ReadLater.Services
{
    public class BookmarkService : IBookmarkService
    {
        protected IUnitOfWork _unitOfWork;

        public BookmarkService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int CreateBookmark(CreateBookmark request)
        {
            var category = _unitOfWork.Repository<Category>()
                .FindById(request.CategoryId);

            if(category == null)
            {
                throw new BookmarkException("Not valid category");
            }

            var bookmark = new Bookmark
            {
                ShortDescription = request.ShortDescription,
                URL = request.URL,
                Category = category,
                CategoryId = category.ID,
                CreateDate = DateTime.UtcNow
            };

            _unitOfWork.Repository<Bookmark>().Insert(bookmark);
            _unitOfWork.Save();

            return bookmark.ID;
        }

        public void DeleteBookmark(int id)
        {
            var repo = _unitOfWork.Repository<Bookmark>();
            var bookmark = repo
                .FindById(id);

            if(bookmark == null)
            {
                throw new BookmarkException("Bookmark does not exists");
            }

            repo.Delete(bookmark);
            _unitOfWork.Save();
        }

        public int EditBookmark(CreateBookmark request)
        {
            var bookmark = _unitOfWork.Repository<Bookmark>()
                .FindById(request.ID);
            
            if(bookmark == null)
            {
                throw new BookmarkException("Bookmark with that id does not exists");
            }

            bookmark.ShortDescription = request.ShortDescription;
            bookmark.URL = request.URL;
            bookmark.CategoryId = request.CategoryId;

            _unitOfWork.Repository<Bookmark>().Update(bookmark);
            _unitOfWork.Save();

            return bookmark.ID;
        }

        public BookmarkDto GetBookmarkById(int? id)
        {
            if (!id.HasValue)
            {
                throw new BookmarkException("Id is not valid");
            }

            var bookmark = _unitOfWork.Repository<Bookmark>()
                .FindById(id);

            if (bookmark == null)
            {
                throw new BookmarkException("Bookmark with that id does not exists");
            }

            return BookmarkDto.FromBookmark(bookmark);
        }

        public IEnumerable<BookmarkDto> GetBookmarks(string category)
        {
            var bookmarks = _unitOfWork.Repository<Bookmark>()
                .Query()
                .OrderBy(l => l.OrderByDescending(b => b.CreateDate));

            if (!string.IsNullOrWhiteSpace(category))
            {
                bookmarks.Filter(b => b.Category != null && b.Category.Name == category);
            }

            return bookmarks.Get().ToList().Select(x => BookmarkDto.FromBookmark(x));
        }
    }
}
