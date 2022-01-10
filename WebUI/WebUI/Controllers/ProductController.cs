using AutoMapper;
using Domain.Interfaces.Services;
using Domain.Models.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProductController(IMapper mapper, IProductService productService, INotifierService notifier, IHostingEnvironment hostingEnvironment)
                                    : base(mapper, notifier)
        {
            _productService = productService;
            _hostingEnvironment = hostingEnvironment;
        }

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
                var productSearch = _mapper.Map<List<ProductViewModel>>(_productService.SearchString(searchString, SelectedCategory));
                int count = 0;

                foreach (var item in productSearch)
                {
                    count++;
                }

                PaginatedList<ProductViewModel> search = new PaginatedList<ProductViewModel>(productSearch, count, pageNumber ?? 1, pageSize);
                return View(search);
            }

            var sort = await _productService.SortFilter(sortOrder);
            var result = _mapper.Map<List<ProductViewModel>>(sort);



            return View(await PaginatedList<ProductViewModel>.CreateAsync(result, pageNumber ?? 1, pageSize));
        }

        [HttpGet]
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
        [AutoValidateAntiforgeryToken]
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
            await MappingListCategories(productViewModel);

            ViewBag.CategoryId = new SelectList(productViewModel.Categories, "Id", "Name", product.CategoryId);
            ViewBag.SupplierId = new SelectList(productViewModel.Suppliers, "Id", "FantasyName", product.SupplierId);
            return View(productViewModel);
        }

        [HttpPost, ActionName("Edit")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> EditConfirmed(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid) return View(await MappingListCategories(productViewModel));

            if (productViewModel.ImagesUpload != null && productViewModel.ImagesUpload.Count > 0 && productViewModel.ImagesUpload.Count <= 5)
            {
                string path = string.Empty;

                foreach (var item in productViewModel.ImagesUpload)
                {
                    var upload = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    path = Guid.NewGuid().ToString() + Path.GetExtension(item?.FileName);
                    var filePath = Path.Combine(upload, path);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                    }

                    productViewModel.Images.Add(new ImageViewModel(path));
                }
            }
            var product = _mapper.Map<Product>(productViewModel);

            await _productService.Update(product);

            if (!ValidOperation())
            {
                return RedirectToAction(nameof(Error));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> RemovePicture(Guid id)
        {
            var result = await _productService.FindImagePathByImageId(id);
            var image = await _productService.FindImageById(id);

            if (result != null)
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", result);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }


            await _productService.RemoveImage(image);


            return RedirectToAction(nameof(Edit), new { Id = image.ProductId });
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

        [HttpPost, ActionName("Delete")]
        [AutoValidateAntiforgeryToken]
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
                        string path = string.Empty;
                        var productString = await _productService.FindImagePathByProductId(viewModel.Id);
                        if (productString != null)
                        {
                            var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", productString);
                            if (System.IO.File.Exists(oldPath))
                            {
                                System.IO.File.Delete(oldPath);
                            }
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
