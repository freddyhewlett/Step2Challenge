using AutoMapper;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Models.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Configuration;
using WebUI.Models;
using WebUI.Models.Products;
using WebUI.Utilities;

namespace WebUI.Controllers
{
    public class CategoryController : MainController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(IMapper mapper, ICategoryService categoryService, INotifierService notifier)
                                    : base(mapper, notifier)
        {
            _categoryService = categoryService;
        }
        // GET: CategoryController
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = searchString;

            int pageSize = 5;

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
                var productSearch = _mapper.Map<List<CategoryViewModel>>(_categoryService.SearchString(searchString));
                int count = 0;

                foreach (var item in productSearch)
                {
                    count++;
                }

                PaginatedList<CategoryViewModel> search = new PaginatedList<CategoryViewModel>(productSearch, count, pageNumber ?? 1, pageSize);
                return View(search);
            }

            var sort = await _categoryService.SortFilter(sortOrder);
            var result = _mapper.Map<List<CategoryViewModel>>(sort);



            return View(await PaginatedList<CategoryViewModel>.CreateAsync(result, pageNumber ?? 1, pageSize));
        }

        // GET: CategoryController/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var category = await _categoryService.FindById(id);
            if (category == null)
            {
                _notifier.AddError("Produto não encontrado");
            }
            return View(_mapper.Map<CategoryViewModel>(category));
        }

        [HttpGet]
        public IActionResult Create()
        {
            var result = new CategoryViewModel();

            return View(result);
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel newCategory)
        {
            if (!ModelState.IsValid) return View(newCategory);

            await _categoryService.Insert(_mapper.Map<Category>(newCategory));

            if (!ValidOperation())
            {
                return View(newCategory);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: CategoryController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var category = await _categoryService.FindById(id);
            if (category == null)
            {
                _notifier.AddError("Produto não encontrado.");
            }

            var categoryViewModel = _mapper.Map<CategoryViewModel>(category);
            return View(categoryViewModel);
        }

        // POST: CategoryController/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirmed(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid) return View(categoryViewModel);

            var category = _mapper.Map<Category>(categoryViewModel);

            await _categoryService.Update(category);

            if (!ValidOperation())
            {
                return RedirectToAction(nameof(Error));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var category = await _categoryService.FindById(id);
            if (category == null)
            {
                _notifier.AddError("Produto não encontrado");
            }
            return View(_mapper.Map<CategoryViewModel>(category));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _categoryService.Remove(id);
            if (!ValidOperation())
            {
                return RedirectToAction(nameof(Error));
            }

            if (_notifier.HasError()) return RedirectToAction(nameof(Error));

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
