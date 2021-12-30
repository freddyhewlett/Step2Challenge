using AutoMapper;
using Domain.Models.Suppliers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly Domain.Interfaces.Services.ISupplierService _supplierService;

        public SupplierController(IMapper mapper, Domain.Interfaces.Services.ISupplierService supplierService, Domain.Interfaces.Services.INotifierService notifier)
                                    : base(mapper, notifier)
        {
            _supplierService = supplierService;
        }

        public IActionResult Index(int index)
        {
            if (index == 1) return RedirectToAction(nameof(IndexPhysical));

            return RedirectToAction(nameof(IndexJuridical));
        }

        // GET: SupplierController
        public async Task<IActionResult> IndexPhysical()
        {
            var supplier = _mapper.Map<IEnumerable<SupplierPhysicalViewModel>>(await _supplierService.PhysicalAll());

            return View(supplier);
        }

        public async Task<IActionResult> IndexJuridical()
        {
            var supplier = _mapper.Map<IEnumerable<SupplierJuridicalViewModel>>(await _supplierService.JuridicalAll());

            return View(supplier);
        }

        // GET: SupplierController/Details/5
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
            newSupplier.CreatePhoneNumbers(1);
            return View(newSupplier);
        }

        // POST: SupplierController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // POST: SupplierController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateJuridical(SupplierJuridicalViewModel supplier)
        {
            if (!ModelState.IsValid) return View(supplier);

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
            var viewModel = new SupplierPhysicalViewModel { };
            return View(_mapper.Map<SupplierPhysicalViewModel>(supplier));
        }

        // POST: SupplierController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPhysical(SupplierPhysicalViewModel supplier)
        {
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
            var viewModel = new SupplierJuridicalViewModel { };
            return View(_mapper.Map<SupplierJuridicalViewModel>(supplier));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditJuridical(SupplierJuridicalViewModel supplier)
        {
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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteJuridicalConfirmed(Guid id)
        {
            await _supplierService.RemoveJuridical(id);
            if (!ValidOperation())
            {
                return RedirectToAction(nameof(Error));
            }
            return RedirectToAction(nameof(IndexJuridical));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //public void CreatePhoneNumbers(SupplierViewModel supplier, int count = 1)
        //{
        //    if (supplier.Phones.Count >= 3)
        //    {
        //        _notifier.AddError("Quantidade limite de numeros telefonicos atingido.");
        //    }

        //    for (int i = 0; i < count; i++)
        //    {
        //        supplier.Phones.Add(new PhoneViewModel());
        //    }
        //}
    }
}
