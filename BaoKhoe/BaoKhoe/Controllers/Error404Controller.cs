using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace BaoKhoe.Controllers
{
    public class Error404Controller : Controller
    {
        private readonly AppDBContext _appDbContext;

        public Error404Controller()
        {
            _appDbContext = new AppDBContext();
        }

        // GET: Error404
        public ActionResult Index()
        {
            ViewBag.Categories = _appDbContext.Categories
                .Include(x => x.SubCategories)
                .ToList();

            return View();
        }
    }
}