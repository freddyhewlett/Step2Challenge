using AutoMapper;
using Domain.Interfaces.Services;
using Domain.Models.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Configuration;
using WebUI.Models;
using WebUI.Models.Products;
using WebUI.Models.Suppliers;
using WebUI.Utilities;

namespace WebUI.Controllers
{
    public class ProductController : MainController
    {
        private readonly IProductService _productService;

        public ProductController(IMapper mapper, IProductService productService, INotifierService notifier)
                                    : base(mapper, notifier)
        {
            _productService = productService;
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
            var categoryViewModel = _mapper.Map<IEnumerable<CategoryViewModel>>(await _productService.ListCategories());
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
                var movieSearch = _mapper.Map<IEnumerable<ProductViewModel>>(_productService.SearchString(searchString, SelectedCategory));
                return View(movieSearch);
            }

            var sort = await _productService.SortFilter(sortOrder);
            var result = _mapper.Map<List<ProductViewModel>>(sort);

            int pageSize = 5;

            return View(await PaginatedList<ProductViewModel>.CreateAsync(result, pageNumber ?? 1, pageSize));
        }

        // GET: ProductController/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _productService.FindById(id);
            if (product == null)
            {
                _notifier.AddError("Produto não encontrado");
            }
            return View(_mapper.Map<ProductViewModel>(product));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var result = new ProductViewModel();

            return View(await MappingListCategories(result));
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel newProduct)
        {
            if (!ModelState.IsValid) return View(await MappingListCategories(newProduct));

            string path = string.Empty;

            if (newProduct.ImagesUpload.Count > 0)
            {
                foreach (var item in newProduct.ImagesUpload)
                {
                    path = Guid.NewGuid().ToString() + Path.GetExtension(item.FileName);

                    if (UploadFile(item, path).Result)
                    {
                        newProduct.Images.Add(SetNewImagePath(path));
                    }
                }                
            }
                     
            await _productService.Insert(_mapper.Map<Product>(newProduct));

            if (!ValidOperation())
            {
                return View(await MappingListCategories(newProduct));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _productService.FindById(id);
            if (product == null)
            {
                _notifier.AddError("Produto não encontrado.");
            }

            var productViewModel = _mapper.Map<ProductViewModel>(product);
            return View(productViewModel);
        }

        // POST: ProductController/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirmed(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid) return View(productViewModel);

            if (productViewModel.ImagesUpload.Count > 1)
            {
                foreach (var item in productViewModel.ImagesUpload)
                {
                    string path = string.Empty;
                    var productString = await _productService.FindImagePath(productViewModel.Id);
                    if (productString != null)
                    {
                        var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", productString);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }
                    path = Guid.NewGuid().ToString() + Path.GetExtension(item.FileName);

                    if (UpdateFile(item, path).Result)
                    {
                        var addImage = productViewModel.Images.Where(x => x.ImagePath == null).FirstOrDefault();
                        addImage.ImagePath = path;
                        productViewModel.Images.Add(addImage);
                    }
                }
                
            }

            await _productService.Update(_mapper.Map<Product>(productViewModel));

            if (!ValidOperation())
            {
                return RedirectToAction(nameof(Error));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productService.FindById(id);
            if (product == null)
            {
                _notifier.AddError("Produto não encontrado");
            }
            return View(_mapper.Map<ProductViewModel>(product));
        }

        // POST: ProductController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _productService.FindById(id);

            if (product != null)
            {
                var viewModel = _mapper.Map<ProductViewModel>(product);
                if (viewModel.Images.Count > 0)
                {
                    foreach (var item in viewModel.Images)
                    {
                        if (System.IO.File.Exists(item.ImagePath))
                        {
                            System.IO.File.Delete(item.ImagePath);
                        }
                    }                    
                }
            }
            await _productService.Remove(id);

            if (_notifier.HasError()) return RedirectToAction(nameof(Error));

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<bool> UploadFile(IFormFile imageUpload, string imgPath)
        {
            if (imageUpload == null || imageUpload?.Length == 0)
            {
                return false;
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgPath);

            if (System.IO.File.Exists(path))
            {
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageUpload.CopyToAsync(stream);
            }
            return true;
        }

        private async Task<ProductViewModel> MappingListCategories(ProductViewModel viewModel)
        {
            await MappingListSuppliers(viewModel);
            viewModel.Categories = _mapper.Map<IEnumerable<CategoryViewModel>>(await _productService.ListCategories());
            return viewModel;
        }

        private async Task<ProductViewModel> MappingListSuppliers(ProductViewModel viewModel)
        {
            //var physical = _mapper.Map<IEnumerable<SupplierViewModel>>(await _productService.ListPhysicalSuppliers());
            //viewModel.Suppliers = _mapper.Map<IEnumerable<SupplierViewModel>>(await _productService.ListJuridicalSuppliers());
            //foreach (var item in physical)
            //{
            //    viewModel.Suppliers.Append(item);
            //}
            viewModel.Suppliers = _mapper.Map<IEnumerable<SupplierViewModel>>(await _productService.ListAllSuppliersIdFantasy());
            return viewModel;
        }

        private static ImageViewModel SetNewImagePath(string path)
        {
            ImageViewModel imageViewModel = new ImageViewModel(); 
            imageViewModel.ImagePath = path;
            return imageViewModel;
        }

        private async Task<bool> UpdateFile(IFormFile imageUpload, string imgPath)
        {
            if (imageUpload == null || imageUpload?.Length == 0)
            {
                return false;
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgPath);

            if (System.IO.File.Exists(path))
            {
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageUpload.CopyToAsync(stream);
            }
            return true;
        }
    }
}
