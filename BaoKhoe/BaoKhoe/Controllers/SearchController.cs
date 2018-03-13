using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaoKhoe.Models;

namespace BaoKhoe.Controllers
{
    public class SearchController : Controller
    {
        private readonly AppDBContext _appDbContext;

        public SearchController()
        {
            _appDbContext = new AppDBContext();
        }

        // GET: Tag
        [OutputCache(Duration = 1200, VaryByParam = "url")]
        public ActionResult Index(string url)
        {
            string[] input = url.Split('/');
            ViewBag.Categories = _appDbContext.Categories
                .Include(x => x.SubCategories)
                .ToList();

            DateTime date = DateTime.Now;
            string searchInput = input[1].ToLower().Replace("-", " ");
            DateTime checkDateTime = date.AddDays(-14);

            List<Article> hotArticles = _appDbContext.Articles
                .Where(x => x.CreatedAt > checkDateTime)
                .OrderByDescending(x => x.ViewCount)
                .ThenByDescending(x => x.CreatedAt)
                .Include(x => x.Category)
                .Take(10)
                .ToList();
            ViewBag.HotArticles = hotArticles;

            List<Article> articles = _appDbContext.Articles
                .Where(x => x.Title.ToLower().Contains(searchInput.ToLower())
                    || x.Headlines.ToLower().Contains(searchInput.ToLower())
                    || x.Title.ToLower().Contains(searchInput.ToLower())
                    || x.Keywords.ToLower().Contains(searchInput.ToLower())
                    )
                .OrderByDescending(x => x.CreatedAt)
                .Include(x => x.Category)
                .Take(20)
                .ToList();

            ViewBag.SearchInput = searchInput;
            ViewBag.Articles = articles;

            return View();
        }

        // Load More
        public ActionResult LoadMore(string searchInput, string listUrls)
        {
            List<Article> articles = _appDbContext.Articles
                .Where(x => (x.Title.ToLower().Contains(searchInput.ToLower())
                            || x.Headlines.ToLower().Contains(searchInput.ToLower())
                            || x.Title.ToLower().Contains(searchInput.ToLower())
                            || x.Keywords.ToLower().Contains(searchInput.ToLower()))
                        && !listUrls.Contains(x.FriendlyTitle)
                )
                .OrderByDescending(x => x.CreatedAt)
                .Include(x => x.Category)
                .Take(20)
                .ToList();

            ViewBag.Articles = articles;
            ViewBag.SearchInput = searchInput;

            return View();
        }
    }
}