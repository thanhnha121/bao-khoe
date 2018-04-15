using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml.Linq;
using BaoKhoe.Models;

namespace BaoKhoe.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDBContext _appDbContext;

        public HomeController()
        {
            _appDbContext = new AppDBContext();
        }

        // [OutputCache(Duration = 1200, VaryByParam = "none")]
        public ActionResult Index()
        {
            DateTime date = DateTime.Now.AddDays(-14);
            ViewBag.Categories = _appDbContext.Categories
                .Include(x => x.SubCategories)
                .ToList();

            List<AssignedArticle> assignedArticlesSpecial = _appDbContext.AssignedArticles
                .Include(x => x.Article)
                .Include(x => x.Article.Category)
                .Where(x => x.Type.Equals("special") && x.ToDate > DateTime.Now)
                .Take(10)
                .ToList();

            List<AssignedArticle> assignedArticlesHot = _appDbContext.AssignedArticles
                .Include(x => x.Article)
                .Include(x => x.Article.Category)
                .Where(x => x.Type.Equals("hot") && x.ToDate > DateTime.Now)
                .Take(12)
                .ToList();

            List<Article> articlesTopView = _appDbContext.Articles
                .Where(y => y.CreatedAt > date)
                .OrderByDescending(x => x.ViewCount)
                .ThenByDescending(x => x.CreatedAt)
                .Include(x => x.Category)
                .Take(10)
                .ToList();

            List<Article> hotArticles = _appDbContext.Articles
                .Where(x => x.CreatedAt > date)
                .OrderByDescending(x => x.ViewCount)
                .ThenByDescending(x => x.CreatedAt)
                .Include(x => x.Category)
                .Take(10)
                .ToList();
            ViewBag.HotArticles = hotArticles;

            foreach (Category category in ViewBag.Categories)
            {
                if (category.IsSubCategory)
                {
                    continue;
                }
                category.Articles = _appDbContext.Articles
                    .Where(x => x.Category.Id == category.Id)
                    .OrderByDescending(x => x.CreatedAt)
                    .Include(x => x.Category)
                    .Take(5)
                    .ToList();
                ViewBag.ArticlesTopView = articlesTopView;
                ViewBag.ArticlesHot = assignedArticlesHot;
                ViewBag.ArticlesSpecial = assignedArticlesSpecial;
            }

            return View();
        }


        // [OutputCache(Duration = 1200, VaryByParam = "none")]
        [HttpPost]
        public ActionResult IndexPartial(int[] listIds)
        {
            List<Category> model = _appDbContext.Categories
                .Include(x => x.SubCategories)
                .ToList();

            foreach (Category category in model)
            {
                if (category.IsSubCategory)
                {
                    continue;
                }
                category.Articles = _appDbContext.Articles
                    .Where(x => x.Category.Id == category.Id)
                    .OrderByDescending(x => x.CreatedAt)
                    .Include(x => x.Category)
                    .Where(x => !listIds.Contains(x.Id))
                    .Take(5)
                    .ToList();
            }
            return PartialView("~/Views/Home/_IndexPartial.cshtml", model);
        }



        public string GetSitemapDocument(IEnumerable<SitemapNode> sitemapNodes)
        {
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement root = new XElement(xmlns + "urlset");
            foreach (SitemapNode sitemapNode in sitemapNodes)
            {
                XElement urlElement = new XElement(
                    xmlns + "url",
                    new XElement(xmlns + "loc", Uri.EscapeUriString(sitemapNode.Url)),
                    sitemapNode.LastModified == null ? null : new XElement(
                        xmlns + "lastmod",
                        sitemapNode.LastModified.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),
                    sitemapNode.Frequency == null ? null : new XElement(
                        xmlns + "changefreq",
                        sitemapNode.Frequency.Value.ToString().ToLowerInvariant()),
                    sitemapNode.Priority == null ? null : new XElement(
                        xmlns + "priority",
                        sitemapNode.Priority.Value.ToString("F1", CultureInfo.InvariantCulture)));
                root.Add(urlElement);
            }
            XDocument document = new XDocument(root);
            return document.ToString();
        }

        public IReadOnlyCollection<SitemapNode> GetSitemap()
        {
            List<SitemapNode> nodes = new List<SitemapNode>();
            var origin = "http://suckhoe24gio.vn";
            if (Request.Url != null)
                nodes.Add(new SitemapNode()
                {
                    Url = origin + "/sitemap-index.xml",
                    Priority = 1.0
                });
            nodes.Add(new SitemapNode()
            {
                Url = origin + "/sitemap-category.xml",
                Priority = 1.0
            });
            return nodes;
        }
        public IReadOnlyCollection<SitemapNode> GetSitemapByMonth()
        {
            List<SitemapNode> nodes = new List<SitemapNode>();
            DateTime date = new DateTime(2018, 1, 1);

            var origin = "http://suckhoe24gio.vn";

            while (date.Month <= DateTime.Now.Month)
            {
                string month = date.Month < 10 ? "0" + date.Month : date.Month + "";
                nodes.Add(new SitemapNode()
                {
                    Url = origin + "/sitemaps-" + date.Year + "-" + month + ".xml",
                    Priority = 0.60
                });
                date = date.AddMonths(1);
            }
            
            return nodes;
        }
        public IReadOnlyCollection<SitemapNode> GetSitemapByCategory()
        {
            List<SitemapNode> nodes = new List<SitemapNode>();
            var origin = "http://suckhoe24gio.vn";

            List<Category> categories = _appDbContext.Categories.ToList();

            foreach (Category category in categories)
            {
                nodes.Add(new SitemapNode()
                {
                    Url = origin + "/" + category.Url,
                    Priority = 0.80
                });
            }
            
            return nodes;
        }
        public IReadOnlyCollection<SitemapNode> GetSitemapByMonthDetails(string url)
        {
            List<SitemapNode> nodes = new List<SitemapNode>();
            var origin = "http://suckhoe24gio.vn";

            string input = url.Split('.')[0];
            DateTime date = new DateTime(Convert.ToInt32(input.Split('-')[1]),
                Convert.ToInt32(input.Split('-')[2]), 
                1);

            List<Article> articles = _appDbContext.Articles
                .Where(x => x.CreatedAt >= date)
                .Include(x => x.Category)
                .OrderByDescending(x => x.ViewCount)
                .ThenByDescending(x => x.CreatedAt)
                .ToList();
            foreach (Article article in articles)
            {
                nodes.Add(new SitemapNode()
                {
                    Url = origin + "/" + article.Category.Url + "/" + article.FriendlyTitle,
                    Priority = 0.40
                });
            }
            
            return nodes;
        }

        // [OutputCache(Duration = 86000, VaryByParam = "none")]
        public ActionResult SiteMap()
        {
            var sitemapNodes = GetSitemap();
            string xml = GetSitemapDocument(sitemapNodes);
            return Content(xml, "text/xml", Encoding.UTF8);
        }

        // [OutputCache(Duration = 86000, VaryByParam = "none")]
        public ActionResult SiteMapByMonth()
        {
            var sitemapNodes = GetSitemapByMonth();
            string xml = GetSitemapDocument(sitemapNodes);
            return Content(xml, "text/xml", Encoding.UTF8);
        }

        // [OutputCache(Duration = 86000, VaryByParam = "none")]
        public ActionResult SiteMapByCategory()
        {
            var sitemapNodes = GetSitemapByCategory();
            string xml = GetSitemapDocument(sitemapNodes);
            return Content(xml, "text/xml", Encoding.UTF8);
        }

        // [OutputCache(Duration = 86000, VaryByParam = "url")]
        public ActionResult SiteMapByMonthDetails(string url)
        {
            var sitemapNodes = GetSitemapByMonthDetails(url);
            string xml = GetSitemapDocument(sitemapNodes);
            return Content(xml, "text/xml", Encoding.UTF8);
        }


    }
}