using AutoMapper;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Models.Suppliers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Configuration;
using WebUI.Models;
using WebUI.Models.Enum;
using WebUI.Models.Suppliers;
using WebUI.Utilities;

namespace WebUI.Controllers
{
    public class SupplierController : MainController
    {
        private readonly ISupplierService _supplierService;
        private readonly IProductService _productService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public SupplierController(IHostingEnvironment hostingEnvironment, IMapper mapper, ISupplierService supplierService, INotifierService notifier, IProductService productService)
                                    : base(mapper, notifier)
        {
            _supplierService = supplierService;
            _productService = productService;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> IndexPhysical(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
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
                var productSearch = _mapper.Map<List<SupplierPhysicalViewModel>>(_supplierService.SearchPhysical(searchString));
                int count = 0;

                foreach (var item in productSearch)
                {
                    count++;
                }

                PaginatedList<SupplierPhysicalViewModel> search = new PaginatedList<SupplierPhysicalViewModel>(productSearch, count, pageNumber ?? 1, pageSize);
                return View(search);
            }

            var sort = await _supplierService.SortPhysicalFilter(sortOrder);
            var result = _mapper.Map<List<SupplierPhysicalViewModel>>(sort);

            return View(await PaginatedList<SupplierPhysicalViewModel>.CreateAsync(result, pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> IndexJuridical(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
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
                var productSearch = _mapper.Map<List<SupplierJuridicalViewModel>>(_supplierService.SearchJuridical(searchString));
                int count = 0;

                foreach (var item in productSearch)
                {
                    count++;
                }

                PaginatedList<SupplierJuridicalViewModel> search = new PaginatedList<SupplierJuridicalViewModel>(productSearch, count, pageNumber ?? 1, pageSize);
                return View(search);
            }

            var sort = await _supplierService.SortJuridicalFilter(sortOrder);
            var result = _mapper.Map<List<SupplierJuridicalViewModel>>(sort);

            return View(await PaginatedList<SupplierJuridicalViewModel>.CreateAsync(result, pageNumber ?? 1, pageSize));
        }

        [HttpGet]
        public async Task<IActionResult> DetailsPhysical(Guid id)
        {
            var supplier = await _supplierService.FindPhysicalById(id);
            if (supplier == null)
            {
                _notifier.AddError("Fornecedor não encontrado.");
            }
            return View(_mapper.Map<SupplierPhysicalViewModel>(supplier));
        }

        public async Task<IActionResult> DetailsJuridical(Guid id)
        {
            var supplier = await _supplierService.FindJuridicalById(id);
            if (supplier == null)
            {
                _notifier.AddError("Fornecedor não encontrado.");
            }
            return View(_mapper.Map<SupplierJuridicalViewModel>(supplier));
        }

        [HttpGet]
        public IActionResult CreatePhysical()
        {
            var newSupplier = new SupplierPhysicalViewModel();
            return View(newSupplier);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreatePhysical(SupplierPhysicalViewModel supplier)
        {
            if (!ModelState.IsValid) return View(supplier);

            if (supplier.MobilePhone != null)
            {
                supplier.Phones.Add(new PhoneViewModel() { Ddd = supplier.MobilePhone[..2], Number = supplier.MobilePhone[2..], PhoneType = PhoneType.Mobile });
            }
            if (supplier.HomePhone != null)
            {
                supplier.Phones.Add(new PhoneViewModel() { Ddd = supplier.HomePhone[..2], Number = supplier.HomePhone[2..], PhoneType = PhoneType.Home });
            }
            if (supplier.OfficePhone != null)
            {
                supplier.Phones.Add(new PhoneViewModel() { Ddd = supplier.OfficePhone[..2], Number = supplier.OfficePhone[2..], PhoneType = PhoneType.Office });
            }
            await _supplierService.InsertPhysical(_mapper.Map<SupplierPhysical>(supplier));


            if (!ValidOperation())
            {
                return RedirectToAction(nameof(Error));
            }
            return RedirectToAction(nameof(IndexPhysical));
        }

        [HttpGet]
        public IActionResult CreateJuridical()
        {
            var newSupplier = new SupplierJuridicalViewModel();
            return View(newSupplier);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateJuridical(SupplierJuridicalViewModel supplier)
        {
            if (!ModelState.IsValid) return View(supplier);

            if (supplier.MobilePhone != null)
            {
                supplier.Phones.Add(new PhoneViewModel() { Ddd = supplier.MobilePhone[..2], Number = supplier.MobilePhone[2..], PhoneType = PhoneType.Mobile });
            }
            if (supplier.HomePhone != null)
            {
                supplier.Phones.Add(new PhoneViewModel() { Ddd = supplier.HomePhone[..2], Number = supplier.HomePhone[2..], PhoneType = PhoneType.Home });
            }
            if (supplier.OfficePhone != null)
            {
                supplier.Phones.Add(new PhoneViewModel() { Ddd = supplier.OfficePhone[..2], Number = supplier.OfficePhone[2..], PhoneType = PhoneType.Office });
            }
            await _supplierService.InsertJuridical(_mapper.Map<SupplierJuridical>(supplier));


            if (!ValidOperation())
            {
                return RedirectToAction(nameof(Error));
            }
            return RedirectToAction(nameof(IndexJuridical));
        }

        [HttpGet]
        public async Task<IActionResult> EditPhysical(Guid id)
        {
            var supplier = await _supplierService.FindPhysicalById(id);
            if (supplier == null)
            {
                _notifier.AddError("Fornecedor não encontrado.");
            }
            var viewModel = _mapper.Map<SupplierPhysicalViewModel>(supplier);
            var homePhone = supplier.Phones.Where(x => x.PhoneType == Domain.Models.Enum.PhoneType.Home).FirstOrDefault();
            var mobilePhone = supplier.Phones.Where(x => x.PhoneType == Domain.Models.Enum.PhoneType.Mobile).FirstOrDefault();
            var officePhone = supplier.Phones.Where(x => x.PhoneType == Domain.Models.Enum.PhoneType.Office).FirstOrDefault();
            if (homePhone != null) viewModel.HomePhone = homePhone.Ddd + homePhone.Number;
            if (mobilePhone != null) viewModel.MobilePhone = mobilePhone.Ddd + mobilePhone.Number;
            if (officePhone != null) viewModel.OfficePhone = officePhone.Ddd + officePhone.Number;
            return View(viewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> EditPhysical(SupplierPhysicalViewModel supplier)
        {
            if (!ModelState.IsValid) return View(supplier);

            supplier.Phones.Add(new PhoneViewModel() { Ddd = supplier.MobilePhone[..2], Number = supplier.MobilePhone[2..], PhoneType = PhoneType.Mobile });

            if (supplier.HomePhone != null)
            {
                supplier.Phones.Add(new PhoneViewModel() { Ddd = supplier.HomePhone[..2], Number = supplier.HomePhone[2..], PhoneType = PhoneType.Home });
            }
            if (supplier.OfficePhone != null)
            {
                supplier.Phones.Add(new PhoneViewModel() { Ddd = supplier.OfficePhone[..2], Number = supplier.OfficePhone[2..], PhoneType = PhoneType.Office });
            }
            await _supplierService.UpdatePhysical(_mapper.Map<SupplierPhysical>(supplier));

            if (!ValidOperation())
            {
                return RedirectToAction(nameof(Error));
            }

            return RedirectToAction(nameof(IndexPhysical));
        }

        [HttpGet]
        public async Task<IActionResult> EditJuridical(Guid id)
        {
            var supplier = await _supplierService.FindJuridicalById(id);
            if (supplier == null)
            {
                _notifier.AddError("Fornecedor não encontrado.");
            }
            var viewModel = _mapper.Map<SupplierJuridicalViewModel>(supplier);
            var homePhone = supplier.Phones.Where(x => x.PhoneType == Domain.Models.Enum.PhoneType.Home).FirstOrDefault();
            var mobilePhone = supplier.Phones.Where(x => x.PhoneType == Domain.Models.Enum.PhoneType.Mobile).FirstOrDefault();
            var officePhone = supplier.Phones.Where(x => x.PhoneType == Domain.Models.Enum.PhoneType.Office).FirstOrDefault();
            if (homePhone != null) viewModel.HomePhone = homePhone.Ddd + homePhone.Number;
            if (mobilePhone != null) viewModel.MobilePhone = mobilePhone.Ddd + mobilePhone.Number;
            if (officePhone != null) viewModel.OfficePhone = officePhone.Ddd + officePhone.Number;
            return View(viewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> EditJuridical(SupplierJuridicalViewModel supplier)
        {
            if (!ModelState.IsValid) return View(supplier);

            supplier.Phones.Add(new PhoneViewModel() { Ddd = supplier.MobilePhone[..2], Number = supplier.MobilePhone[2..], PhoneType = PhoneType.Mobile });

            if (supplier.HomePhone != null)
            {
                supplier.Phones.Add(new PhoneViewModel() { Ddd = supplier.HomePhone[..2], Number = supplier.HomePhone[2..], PhoneType = PhoneType.Home });
            }
            if (supplier.OfficePhone != null)
            {
                supplier.Phones.Add(new PhoneViewModel() { Ddd = supplier.OfficePhone[..2], Number = supplier.OfficePhone[2..], PhoneType = PhoneType.Office });
            }
            await _supplierService.UpdateJuridical(_mapper.Map<SupplierJuridical>(supplier));

            if (!ValidOperation())
            {
                return RedirectToAction(nameof(Error));
            }

            return RedirectToAction(nameof(IndexJuridical));
        }

        [HttpGet]
        public async Task<IActionResult> DeletePhysical(Guid id)
        {
            var supplier = await _supplierService.FindPhysicalById(id);
            if (supplier == null)
            {
                _notifier.AddError("Fornecedor não encontrado.");
            }
            return View(_mapper.Map<SupplierPhysicalViewModel>(supplier));
        }

        // POST: SupplierController/Delete/5
        [HttpPost, ActionName("DeletePhysical")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeletePhysicalConfirmed(Guid id)
        {
            await _supplierService.RemovePhysical(id);
            if (!ValidOperation())
            {
                return RedirectToAction(nameof(Error));
            }
            return RedirectToAction(nameof(IndexPhysical));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteJuridical(Guid id)
        {
            var supplier = await _supplierService.FindJuridicalById(id);
            if (supplier == null)
            {
                _notifier.AddError("Fornecedor não encontrado.");
            }
            return View(_mapper.Map<SupplierJuridicalViewModel>(supplier));
        }

        // POST: SupplierController/Delete/5
        [HttpPost, ActionName("DeleteJuridical")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteJuridicalConfirmed(Guid id)
        {
            await _supplierService.RemoveJuridical(id);
            if (!ValidOperation())
            {
                return RedirectToAction(nameof(Error));
            }
            return RedirectToAction(nameof(IndexJuridical));
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<FileContentResult> ExportPhysicalSupplierXlsx()
        {
            var physicalsuppliersList = await _supplierService.PhysicalAll();
            var productsList = await _productService.ProductsAll();

            foreach (var supplier in physicalsuppliersList)
            {
                foreach (var product in productsList)
                {
                    if (supplier.Id == product.SupplierId)
                    {
                        supplier.Products.Add(product);
                    }
                }
            }


            var listPhysicalModel = _mapper.Map<IEnumerable<SupplierViewModel>>(physicalsuppliersList);

            var uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "excel");
            var fileName = Guid.NewGuid().ToString() + "_RelatorioPessoaFisica.xlsx";
            var filePath = Path.Combine(uploadFolder, fileName);

            ExcelFile.Create(filePath, listPhysicalModel);

            byte[] excel = System.IO.File.ReadAllBytes(filePath);

            return new FileContentResult(excel, $"{filePath}");
        }

        [HttpGet]
        public async Task<IActionResult> ExportJuridicalSupplierXlsx()
        {
            var juridicalsuppliersList = await _supplierService.JuridicalAll();
            var productsList = await _productService.ProductsAll();

            foreach (var supplier in juridicalsuppliersList)
            {
                foreach (var product in productsList)
                {
                    if (supplier.Id == product.SupplierId)
                    {
                        supplier.Products.Add(product);
                    }
                }
            }


            var listPhysicalModel = _mapper.Map<IEnumerable<SupplierViewModel>>(juridicalsuppliersList);

            var uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "excel");
            var fileName = Guid.NewGuid().ToString() + "_RelatorioPessoaJuridica.xlsx";
            var filePath = Path.Combine(uploadFolder, fileName);

            ExcelFile.Create(filePath, listPhysicalModel);

            return RedirectToAction(nameof(IndexPhysical));
        }
    }
}
