using ReadLater.Services;
using ReadLater.Services.Models.Bookmarks;
using ReadLater.Services.Models.Exceptions;
using System;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class BookmarksController : Controller
    {
        private readonly IBookmarkService _bookmarkService;
        private readonly ICategoryService _categoryService;

        public BookmarksController(IBookmarkService bookmarkService, ICategoryService categoryService)
        {
            _bookmarkService = bookmarkService;
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            return View(_bookmarkService.GetBookmarks(null, User.Identity.Name));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateBookmark request)
        {
            if(!ModelState.IsValid)
            {
                return View(request);
            }

            try
            {
                var id = _bookmarkService.CreateBookmark(request, User.Identity.Name);
                return RedirectToAction("Index");
                //return RedirectToAction("Details", id);
            }
            catch (BookmarkException ex)
            {
                ViewBag.Error = ex.Message;
                return View(request);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
        }

        public ActionResult Details(int? id)
        {
            try
            {
                return View(_bookmarkService.GetBookmarkById(id, User.Identity.Name));
            }
            catch (BookmarkException ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
        }

        public ActionResult Edit(int? id)
        {
            try
            {
                var bookmark = _bookmarkService.GetBookmarkById(id, User.Identity.Name);
                var categories = _categoryService.GetCategoriesJson();
                return View(new EditBookmark { Bookmark = bookmark, Categories = categories.Categories });
            }
            catch (BookmarkException ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        public ActionResult Edit(CreateBookmark request)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Error = "Not valid input.";
                return RedirectToAction("Edit", request.ID);
            }

            try
            {
                int id = _bookmarkService.EditBookmark(request, User.Identity.Name);
                return RedirectToAction("Details", id);
            }
            catch (BookmarkException ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("Edit", request.ID);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                return View(_bookmarkService.GetBookmarkById(id, User.Identity.Name));
            }
            catch (BookmarkException ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _bookmarkService.DeleteBookmark(id, User.Identity.Name);
                return RedirectToAction("Index");
            }
            catch (BookmarkException ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}