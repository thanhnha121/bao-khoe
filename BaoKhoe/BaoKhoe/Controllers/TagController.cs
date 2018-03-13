using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaoKhoe.Models;

namespace BaoKhoe.Controllers
{
    public class TagController : Controller
    {
        private readonly AppDBContext _appDbContext;

        public TagController()
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
            string tag = input[1].ToLower();
            DateTime checkDateTime = date.AddDays(-14);

            List<Article> hotArticles = _appDbContext.Articles
                .Where(x => x.CreatedAt > checkDateTime)
                .OrderByDescending(x => x.ViewCount)
                .ThenByDescending(x => x.CreatedAt)
                .Include(x => x.Category)
                .Take(10)
                .ToList();
            ViewBag.HotArticles = hotArticles;

            List<Article> articles =
                _appDbContext.ArticleKeywords.Where(x => x.Keyword.FriendlyTitle.ToLower().Equals(tag.ToLower()))
                    .Select(y => y.Article)
                    .OrderByDescending(y => y.CreatedAt)
                    .Include(y => y.Category)
                    .Take(20)
                    .ToList();

            //List<Article> articles = _appDbContext.Articles
            //    .Where(x => x.ArticleKeywords.Select(z => z.Keyword.FriendlyTitle).ToList().Any(y => y.ToLower().Equals(tag.ToLower())))
            //    .OrderByDescending(x => x.CreatedAt)
            //    .Include(x => x.Category)
            //    .Take(20)
            //    .ToList();

            ViewBag.Tag = tag;
            ViewBag.Articles = articles;

            Keyword keyword =
                _appDbContext.Keywords.FirstOrDefault(x => x.FriendlyTitle.ToLower().Equals(tag.ToLower()));
            if (keyword != null) keyword.VisitCount++;
            _appDbContext.SaveChanges();

            return View();
        }
        
        // Load More
        public ActionResult LoadMore(string tag, string listUrls)
        {
            //List<Article> articles = _appDbContext.Articles
            //    .Where(x => !listUrls.Contains(x.FriendlyTitle) 
            //        && x.ArticleKeywords.Select(z => z.Keyword.FriendlyTitle).ToList().Any(y => y.ToLower().Equals(tag.ToLower())))
            //    .OrderByDescending(x => x.CreatedAt)
            //    .Include(x => x.Category)
            //    .Take(20)
            //    .ToList();

            List<Article> articles =
                _appDbContext.ArticleKeywords.Where(x => x.Keyword.FriendlyTitle.Equals(tag.ToLower()) && !listUrls.Contains(x.Article.FriendlyTitle))
                    .Select(y => y.Article)
                    .OrderByDescending(y => y.CreatedAt)
                    .Include(x => x.Category)
                    .Take(20)
                    .ToList();
            ViewBag.Articles = articles;

            return View();
        }
    }
}