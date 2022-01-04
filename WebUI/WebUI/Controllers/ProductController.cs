using AutoMapper;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Configuration;
using WebUI.Models.Products;
using WebUI.Utilities;

namespace WebUI.Controllers
{
    public class ProductController : MainController
    {
        private readonly IProductService _supplierService;

        public ProductController(IMapper mapper, IProductService supplierService, INotifierService notifier)
                                    : base(mapper, notifier)
        {
            _supplierService = supplierService;
        }

        // GET: ProductController
        public async Task<IActionResult> Index(string sortOrder, Guid? SelectedCategory, string searchString, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["QuantitySortParm"] = sortOrder == "Quantity" ? "quantity_desc" : "Quantity";
            ViewData["PriceSalesSortParm"] = sortOrder == "PriceSales" ? "price_desc" : "PriceSales";
            ViewData["CurrentFilter"] = searchString;
            var categoryViewModel = _mapper.Map<IEnumerable<CategoryViewModel>>(await _supplierService.ListCategories());
            ViewBag.SelectedCategory = new SelectList(categoryViewModel, "Id", "Name", SelectedCategory);

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                var movieSearch = _mapper.Map<IEnumerable<ProductViewModel>>(_supplierService.SearchString(searchString, SelectedCategory));
                return View(movieSearch);
            }

            var sort = await _supplierService.SortFilter(sortOrder);
            var result = _mapper.Map<List<ProductViewModel>>(sort);

            int pageSize = 5;

            return View(await PaginatedList<ProductViewModel>.CreateAsync(result, pageNumber ?? 1, pageSize));
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
    }
}
