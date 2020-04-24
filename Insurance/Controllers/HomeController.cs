using System.Globalization;
using System.Linq;
using Insurance.Data.Models;
using Insurance.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Insurance.Web.Models;

namespace Insurance.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContentsRepo _contentsRepo;

        public HomeController(IContentsRepo contentsRepo)
        {
            _contentsRepo = contentsRepo;
        }

        public IActionResult Index()
        {
            var model = GetContentsViewModel();

            return View(model);
        }

        public JsonResult AddItem(string name, decimal price, int categoryId)
        {
            _contentsRepo.AddItem(new Item
            {
                Name = name,
                Price = price,
                CategoryId = categoryId
            });

            var model = GetContentsViewModel();
            return new JsonResult(new { model });
        }

        public JsonResult DeleteItem(long id)
        {
            _contentsRepo.DeleteItem(id);
            var model = GetContentsViewModel();
            return new JsonResult(new { model });
        }

        private ContentsViewModel GetContentsViewModel()
        {
            var items = _contentsRepo.GetAllItems().ToList();
            var usedCategories = items.GroupBy(i => i.Category).Select(grouped => new UsedCategoryViewModel
            {
                CategoryId = grouped.Key.Id,
                CategoryName = grouped.Key.Name,
                CategoryTotal = grouped.Sum(i => i.Price).ToString("C2", CultureInfo.CurrentCulture),
                Items = grouped.Select(i => new ItemViewModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    Price = i.Price.ToString("C2", CultureInfo.CurrentCulture)
                })
            });
            var model = new ContentsViewModel
            {
                Categories = _contentsRepo.GetAllCategories()
                    .Select(c => new CategoryViewModel { Id = c.Id, Name = c.Name }),
                UsedCategories = usedCategories.OrderBy(c => c.CategoryId),
                Total = items.Sum(i => i.Price).ToString("C2", CultureInfo.CurrentCulture)
            };
            return model;
        }
    }
}
