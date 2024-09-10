using ECommerce.Business.Abstract;
using ECommerce.Entities.Models;
using ECommerce.WebUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: ProductController
        public async Task<ActionResult> Index(int page = 1, int category = 0,int sort = 0)
        {
            var items = await _productService.GetAllByCategoryAsync(category);
            int pageSize = 10;

            var model = new ProductListViewModel
            {
                Products = new List<Product>(),
                PageSize = pageSize,
                CurrentCategory = category,
                CurrentPage = page,
                PageCount = (int)Math.Ceiling(items.Count / (double)pageSize),
                Sort = sort

            };

            if(sort == 2)
            {
                //Edit For UnitPrice
                model.Products = items.Skip((page - 1) * pageSize).Take(pageSize).OrderBy(pr => pr.UnitPrice).ToList();
            }
            else if (sort == 1)
            {
                //Edit For Product Name
                model.Products = items.Skip((page - 1) * pageSize).Take(pageSize).OrderBy(pr => pr.ProductName).ToList();
            }
            else
            {
                model.Products = items.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            }
            return View("Index", model);
        }


    

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> SearchProduct(string seachinput,int page =1,int category = 0)
        {
            var list = await _productService.GetAllByCategoryAsync(category);

            if(!string.IsNullOrEmpty(seachinput))
            {
                list = list.Where(p => p.ProductName.StartsWith(seachinput, StringComparison.OrdinalIgnoreCase))
                    .ToList();;;

            }

            const int pagesize = 10;

            var paginatedProducts = list
        .Skip((page - 1) * pagesize)  
        .Take(pagesize) 
        .ToList();

          
            var model = new ProductListViewModel
            {
                Products = paginatedProducts,
                PageSize = pagesize,
                CurrentCategory = category,
                CurrentPage = page,
                PageCount = (int)Math.Ceiling(list.Count / (double)pagesize)  
            };

            return PartialView("_ProductListPartial", model);

        }
    }
}
