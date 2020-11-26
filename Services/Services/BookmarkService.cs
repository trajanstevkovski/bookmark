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
        private readonly IUserRepository _userRepository;

        public BookmarkService(IUnitOfWork unitOfWork,
            IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public int CreateBookmark(CreateBookmark request, string username)
        {
            CheckIfUserExists(username, out var user);

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
                CreateDate = DateTime.UtcNow,
                UserId = user.Id,
                User = user
            };

            _unitOfWork.Repository<Bookmark>().Insert(bookmark);
            _unitOfWork.Save();

            return bookmark.ID;
        }

        public void DeleteBookmark(int id, string username)
        {
            CheckIfUserExists(username, out var user);

            var repo = _unitOfWork.Repository<Bookmark>();
            var bookmark = repo.Query().Include(x => x.User).Filter(x => x.ID == id && x.User.Email == username).Get().First();

            if(bookmark == null)
            {
                throw new BookmarkException("Bookmark does not exists");
            }

            repo.Delete(bookmark);
            _unitOfWork.Save();
        }

        public int EditBookmark(CreateBookmark request, string username)
        {
            CheckIfUserExists(username, out var user);

            var bookmark = _unitOfWork.Repository<Bookmark>()
                .Query().Include(x => x.User).Filter(x => x.ID == request.ID && x.User.Email == username).Get().First();

            if (bookmark == null)
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

        public BookmarkDto GetBookmarkById(int? id, string username)
        {
            if (!id.HasValue)
            {
                throw new BookmarkException("Id is not valid");
            }

            CheckIfUserExists(username, out var user);

            var bookmark = _unitOfWork.Repository<Bookmark>()
                .Query().Include(x => x.User).Filter(x => x.ID == id && x.User.Email == username).Get().First();

            if (bookmark == null)
            {
                throw new BookmarkException("Bookmark with that id does not exists");
            }

            return BookmarkDto.FromBookmark(bookmark);
        }

        public IEnumerable<BookmarkDto> GetBookmarks(string category, string username)
        {
            CheckIfUserExists(username, out var user);

            var bookmarks = _unitOfWork.Repository<Bookmark>()
                .Query()
                .OrderBy(l => l.OrderByDescending(b => b.CreateDate));

            if (!string.IsNullOrWhiteSpace(category))
            {
                bookmarks.Filter(b => b.Category != null && b.Category.Name == category);
            }

            return bookmarks.Include(x => x.User).Filter(x => x.User.Email == username).Get().ToList().Select(x => BookmarkDto.FromBookmark(x));
        }

        private void CheckIfUserExists(string username, out User user)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new BookmarkException("Not valid username");
            }

            user = _userRepository.GetUser(username);

            if (user == null)
            {
                throw new BookmarkException("Not valid username");
            }
        }
    }
}
