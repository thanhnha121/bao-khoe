using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using BaoKhoe.Models;

namespace BaoKhoe.Controllers
{
    public class DetailController : Controller
    {
        private readonly AppDBContext _appDbContext;

        public DetailController()
        {
            _appDbContext = new AppDBContext();
        }

        // GET: Detail
        [OutputCache(Duration = 86000, VaryByParam = "url")]
        public ActionResult Index(string url)
        {
            string[] input = url.Split('/');
            DateTime date = DateTime.Now;
            ViewBag.Categories = _appDbContext.Categories
                .Include(x => x.SubCategories)
                .ToList();

            string tmp = input[0];
            Category category = _appDbContext.Categories.FirstOrDefault(x => x.Url.Equals(tmp.ToLower().Trim()));
            if (category == null)
            {
                return Redirect("/Error404");
            }

            if (input.Length < 2)
            {
                return Redirect("/Error404");
            }
            else
            {
                string friendlyTitle = input[1];
                Article article =
                    _appDbContext.Articles
                    .Include(x => x.Category)
                    .FirstOrDefault(x => x.FriendlyTitle.Equals(friendlyTitle.ToLower().Trim()));

                if (article != null)
                {
                    article.ListKeywords = _appDbContext.ArticleKeywords
                        .Where(y => y.Article.Id == article.Id)
                        .Select(x => x.Keyword).ToList();
                    article.RelatedArticles = _appDbContext.RelatedArticles
                        .Where(x => x.Origin.Id == article.Id)
                        .OrderByDescending(x => x.Index)
                        .Take(3)
                        .Select(x => x.Related).ToList();

                    article.ViewCount++;
                    ViewBag.Article = article;
                    DateTime checkDate = date.AddDays(-14);

                    List<Article> articles1 = _appDbContext.Articles
                        .Where(x => x.CreatedAt > checkDate && x.Id != article.Id)
                        .OrderByDescending(x => x.ViewCount)
                        .Include(x => x.Category)
                        .Take(6)
                        .ToList();
                    List<Article> articles2 = _appDbContext.Articles
                        .Where(x => x.Category.Id == article.Category.Id && x.Id != article.Id)
                        .OrderByDescending(x => x.CreatedAt)
                        .Include(x => x.Category)
                        .Take(26)
                        .ToList();

                    for (int i = 0; i < articles2.Count; i++)
                    {
                        foreach (Article t in articles1)
                        {
                            if (articles2[i].Id == t.Id)
                            {
                                articles2.RemoveAt(i);
                                i--;
                                break;
                            }
                        }
                    }
                    ViewBag.HotArticles = articles1;
                    ViewBag.SameCatergoryArticles = articles2;
                    _appDbContext.SaveChanges();

                    return View();
                }
                else
                {
                    return Redirect("/Error404");
                }
            }
        }
    }
}