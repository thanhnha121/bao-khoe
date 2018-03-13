using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using BaoKhoe.Models;

namespace BaoKhoe.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDBContext _appDbContext;

        public CategoryController()
        {
            _appDbContext = new AppDBContext();
        }

        // GET: Detail
        [OutputCache(Duration = 1200, VaryByParam = "url")]
        public ActionResult Index(string url)
        {
            DateTime date = DateTime.Now.AddDays(-14);
            ViewBag.Categories = _appDbContext.Categories
                .Include(x => x.SubCategories)
                .ToList();
            DateTime checkDate = date.AddDays(-14);

            string input = url.Replace("/", "");

            Category category =
                _appDbContext.Categories
                    .Include(x => x.SubCategories)
                    .FirstOrDefault(x => x.Url.Equals(input.ToLower().Trim()));

            
            if (category != null)
            {
                string subIds = "";
                if (!category.IsSubCategory)
                {
                    for (int i = 0; i < category.SubCategories.Count; i++)
                    {
                        subIds += "/" + category.SubCategories[i].Url + "/";
                        if (i != category.SubCategories.Count - 1)
                        {
                            subIds += ",";
                        }
                    }
                }
                ViewBag.Category = category;

                // include sub category
                List<Article> articles1 = _appDbContext.Articles
                    .Where(x => (x.Category.Id == category.Id || subIds.Contains("/" + x.Category.Url + "/")) 
                        && x.CreatedAt > checkDate)
                    .OrderByDescending(x => x.ViewCount)
                    .Include(x => x.Category)
                    .Take(3)
                    .ToList();
                ViewBag.HotArticles = articles1;
                List<Article> articles = _appDbContext.Articles
                    .Where(x => x.Category.Id == category.Id || subIds.Contains("/" + x.Category.Url + "/"))
                    .OrderByDescending(x => x.CreatedAt)
                    .Include(x => x.Category)
                    .Take(23)
                    .ToList();
                for (int i = 0; i < articles.Count; i++)
                {
                    foreach (Article article in articles1)
                    {
                        if (articles[i].Id == article.Id)
                        {
                            articles.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                       
                }
                ViewBag.NewestArticles = articles;
                return View();
            }
            else
            {
                return Redirect("/Error404");
            }
        }

        // load more article
        [HttpPost]
        public ActionResult LoadMore(int categoryId, string listUrls)
        {
            Category category =
                _appDbContext.Categories
                    .Include(x => x.SubCategories)
                    .FirstOrDefault(x => x.Id == categoryId);
            string subIds = "";
            if (category != null && !category.IsSubCategory)
            {
                for (int i = 0; i < category.SubCategories.Count; i++)
                {
                    subIds += "/" + category.SubCategories[i].Url + "/";
                    if (i != category.SubCategories.Count - 1)
                    {
                        subIds += ",";
                    }
                }
            }

            List<Article> articles = _appDbContext.Articles
                .Where(x => (x.Category.Id == categoryId || subIds.Contains("/" + x.Category.Url + "/")) 
                    && !listUrls.Contains(x.FriendlyTitle))
                .OrderByDescending(x => x.CreatedAt)
                .Include(x => x.Category)
                .Take(20)
                .ToList();
            ViewBag.Articles = articles;
            return View();
        }
    }
}
