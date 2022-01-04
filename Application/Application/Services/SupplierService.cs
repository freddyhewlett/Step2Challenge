using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Models.Enum;
using Domain.Models.Products;
using Domain.Models.Suppliers;
using Domain.Models.Validation;
using Domain.Tools;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly INotifierService _notifierService;

        public SupplierService(ISupplierRepository supplierRepository, INotifierService notifierService)
        {
            _supplierRepository = supplierRepository;
            _notifierService = notifierService;
        }

        public async Task<SupplierPhysical> FindPhysicalById(Guid id)
        {
            var result = await _supplierRepository.FindPhysicalById(id);

            if (result == null)
            {
                _notifierService.AddError("Fornecedor não encontrado.");
            }

            return result;
        }

        public async Task<SupplierJuridical> FindJuridicalById(Guid id)
        {
            var result = await _supplierRepository.FindJuridicalById(id);

            if (result == null)
            {
                _notifierService.AddError("Fornecedor não existe no cadastro.");
            }

            return result;
        }

        public async Task<IEnumerable<Product>> ProductListByPhysical(Guid id)
        {
            var result = await _supplierRepository.ProductListByPhysical(id);
            if (result == null)
            {
                _notifierService.AddError("Não existem produtos cadastrados para este fornecedor.");
            }
            return result;
        }

        public async Task<IEnumerable<Product>> ProductListByJuridical(Guid id)
        {
            var result = await _supplierRepository.ProductListByJuridical(id);
            if (result == null)
            {
                _notifierService.AddError("Não existem produtos cadastrados para este fornecedor.");
            }
            return result;
        }

        public async Task<IEnumerable<SupplierPhysical>> PhysicalAll()
        {
            return await _supplierRepository.ToListPhysical();
        }

        public async Task<IEnumerable<SupplierJuridical>> JuridicalAll()
        {
            return await _supplierRepository.ToListJuridical();
        }

        public async Task InsertPhysical(SupplierPhysical supplier)
        {
            if (_supplierRepository.FindPhysical(x => x.Cpf == supplier.Cpf).Result != null)
            {
                _notifierService.AddError("Já existe fornecedor cadastrado com este CPF.");
                return;
            }
            if (_supplierRepository.FindPhysical(x => x.FantasyName == supplier.FantasyName).Result != null)
            {
                _notifierService.AddError("Já existe fornecedor cadastrado com este nome fantasia.");
                return;
            }
            if (supplier.BirthDate.Date >= DateTime.Now.AddYears(-18).Date)
            {
                _notifierService.AddError("Cadastro permitido apenas para maiores de 18 anos.");
                return;
            }
            if (!supplier.Cpf.IsCpf())
            {
                _notifierService.AddError("CPF INVALIDO");
                return;
            }

            await _supplierRepository.InsertPhysical(supplier);
            await _supplierRepository.SaveChanges();
        }

        public async Task InsertJuridical(SupplierJuridical supplier)
        {
            if (_supplierRepository.FindJuridical(x => x.Cnpj == supplier.Cnpj).Result != null)
            {
                _notifierService.AddError("Já existe fornecedor cadastrado com este CNPJ.");
                return;
            }
            if (_supplierRepository.FindJuridical(x => x.FantasyName == supplier.FantasyName).Result != null)
            {
                _notifierService.AddError("Já existe fornecedor cadastrado com este nome fantasia.");
                return;
            }
            if (!supplier.Cnpj.IsCnpj())
            {
                _notifierService.AddError("CNPJ INVALIDO");
                return;
            }

            await _supplierRepository.InsertJuridical(supplier);
            await _supplierRepository.SaveChanges();
        }

        public async Task RemovePhysical(Guid id)
        {
            var remove = await FindPhysicalById(id);
            if (_notifierService.HasError()) return;

            await _supplierRepository.RemovePhysical(remove);
            await _supplierRepository.SaveChanges();
        }

        public async Task RemoveJuridical(Guid id)
        {
            var remove = await FindJuridicalById(id);
            if (_notifierService.HasError()) return;

            await _supplierRepository.RemoveJuridical(remove);
            await _supplierRepository.SaveChanges();
        }

        public async Task UpdatePhysical(SupplierPhysical supplier)
        {
            RunValidation(new PhysicalValidation(), supplier);

            foreach (var item in supplier.Phones)
            {
                RunValidation(new PhoneValidation(), item);
            }

            //TODO Fluent Validation (classes) para Address e Email

            if (_notifierService.HasError()) return;
            var result = await _supplierRepository.FindPhysicalById(supplier.Id);

            if (result == null)
            {
                _notifierService.AddError("Fornecedor não encontrado");
                return;
            }

            result.SetFantasyName(supplier.FantasyName);
            result.SetAddress(supplier.Address);
            result.SetEmail(supplier.Email.EmailAddress);
            result.SetBirthDate(supplier.BirthDate);
            result.SetCpf(supplier.Cpf);
            result.SetFullName(supplier.FullName);
            
            if (supplier.Phones.Where(x => x.PhoneType == PhoneType.Mobile).FirstOrDefault() != null)
            {
                result.SetUpdatePhone(supplier.Phones.Where(x => x.PhoneType == PhoneType.Mobile).FirstOrDefault());
                await _supplierRepository.UpdatePhone(result.Phones.Where(x => x.PhoneType == PhoneType.Mobile).FirstOrDefault());
            }

            if (supplier.Phones.Where(x => x.PhoneType == PhoneType.Home).FirstOrDefault() != null)
            {
                if (result.PhoneExists(PhoneType.Home))
                {
                    result.SetUpdatePhone(supplier.Phones.Where(x => x.PhoneType == PhoneType.Home).FirstOrDefault());
                    await _supplierRepository.UpdatePhone(result.Phones.Where(x => x.PhoneType == PhoneType.Home).First());
                }
                else
                {
                    result.SetUpdatePhone(supplier.Phones.Where(x => x.PhoneType == PhoneType.Home).FirstOrDefault());
                    await _supplierRepository.InsertPhone(result.Phones.Where(x => x.PhoneType == PhoneType.Home).First());
                }
                
            }
            else
            {
                var phoneExist = result.Phones.Where(x => x.PhoneType == PhoneType.Home).FirstOrDefault();
                if (phoneExist != null)
                {
                    result.SetRemovePhone(phoneExist);
                    await _supplierRepository.RemovePhone(phoneExist);
                }                
            }

            if (supplier.Phones.Where(x => x.PhoneType == PhoneType.Office).FirstOrDefault() != null)
            {
                if (result.PhoneExists(PhoneType.Office))
                {
                    result.SetUpdatePhone(supplier.Phones.Where(x => x.PhoneType == PhoneType.Office).FirstOrDefault());
                    await _supplierRepository.UpdatePhone(result.Phones.Where(x => x.PhoneType == PhoneType.Office).First());
                }
                else
                {
                    result.SetUpdatePhone(supplier.Phones.Where(x => x.PhoneType == PhoneType.Office).FirstOrDefault());
                    await _supplierRepository.InsertPhone(result.Phones.Where(x => x.PhoneType == PhoneType.Office).First());
                }
            }
            else
            {
                var phoneExist = result.Phones.Where(x => x.PhoneType == PhoneType.Office).FirstOrDefault();
                if (phoneExist != null)
                {
                    result.SetRemovePhone(phoneExist);
                    await _supplierRepository.RemovePhone(phoneExist);
                }
            }

            await _supplierRepository.UpdatePhysical(result);
            await _supplierRepository.SaveChanges();
        }

        public async Task UpdateJuridical(SupplierJuridical supplier)
        {

            RunValidation(new JuridicalValidation(), supplier);

            foreach (var item in supplier.Phones)
            {
                RunValidation(new PhoneValidation(), item);
            }

            //TODO Fluent Validation (classes) para Address e Email

            if (_notifierService.HasError()) return;
            var result = await _supplierRepository.FindJuridicalById(supplier.Id);

            if (result == null)
            {
                _notifierService.AddError("Fornecedor não encontrado");
                return;
            }

            result.SetFantasyName(supplier.FantasyName);
            result.SetAddress(supplier.Address);
            result.SetEmail(supplier.Email.EmailAddress);
            result.SetOpenDate(supplier.OpenDate);
            result.SetCnpj(supplier.Cnpj);
            result.SetCompanyName(supplier.CompanyName);

            if (supplier.Phones.Where(x => x.PhoneType == PhoneType.Mobile).FirstOrDefault() != null)
            {
                result.SetUpdatePhone(supplier.Phones.Where(x => x.PhoneType == PhoneType.Mobile).FirstOrDefault());
                await _supplierRepository.UpdatePhone(result.Phones.Where(x => x.PhoneType == PhoneType.Mobile).FirstOrDefault());
            }

            if (supplier.Phones.Where(x => x.PhoneType == PhoneType.Home).FirstOrDefault() != null)
            {
                if (result.PhoneExists(PhoneType.Home))
                {
                    result.SetUpdatePhone(supplier.Phones.Where(x => x.PhoneType == PhoneType.Home).FirstOrDefault());
                    await _supplierRepository.UpdatePhone(result.Phones.Where(x => x.PhoneType == PhoneType.Home).First());
                }
                else
                {
                    result.SetUpdatePhone(supplier.Phones.Where(x => x.PhoneType == PhoneType.Home).FirstOrDefault());
                    await _supplierRepository.InsertPhone(result.Phones.Where(x => x.PhoneType == PhoneType.Home).First());
                }

            }
            else
            {
                var phoneExist = result.Phones.Where(x => x.PhoneType == PhoneType.Home).FirstOrDefault();
                if (phoneExist != null)
                {
                    result.SetRemovePhone(phoneExist);
                    await _supplierRepository.RemovePhone(phoneExist);
                }
            }

            if (supplier.Phones.Where(x => x.PhoneType == PhoneType.Office).FirstOrDefault() != null)
            {
                if (result.PhoneExists(PhoneType.Office))
                {
                    result.SetUpdatePhone(supplier.Phones.Where(x => x.PhoneType == PhoneType.Office).FirstOrDefault());
                    await _supplierRepository.UpdatePhone(result.Phones.Where(x => x.PhoneType == PhoneType.Office).First());
                }
                else
                {
                    result.SetUpdatePhone(supplier.Phones.Where(x => x.PhoneType == PhoneType.Office).FirstOrDefault());
                    await _supplierRepository.InsertPhone(result.Phones.Where(x => x.PhoneType == PhoneType.Office).First());
                }
            }
            else
            {
                var phoneExist = result.Phones.Where(x => x.PhoneType == PhoneType.Office).FirstOrDefault();
                if (phoneExist != null)
                {
                    result.SetRemovePhone(phoneExist);
                    await _supplierRepository.RemovePhone(phoneExist);
                }
            }

            await _supplierRepository.UpdateJuridical(result);
            await _supplierRepository.SaveChanges();
        }

        public IQueryable<SupplierPhysical> SearchPhysical(string search)
        {
            var supplier = _supplierRepository.SearchPhysicalString(search);

            return supplier;
        }

        public IQueryable<SupplierJuridical> SearchJuridical(string search)
        {
            var supplier = _supplierRepository.SearchJuridicalString(search);

            return supplier;
        }

        public async Task InsertPhone(Phone phone)
        {
            await _supplierRepository.InsertPhone(phone);
            await _supplierRepository.SaveChanges();
        }

        public async Task RemovePhone(Phone phone)
        {
            await _supplierRepository.RemovePhone(phone);
            await _supplierRepository.SaveChanges();
        }

        public async Task UpdatePhone(Phone phone)
        {
            await _supplierRepository.UpdatePhone(phone);
            await _supplierRepository.SaveChanges();
        }

        public async Task InsertAddress(Address address)
        {
            await _supplierRepository.InsertAddress(address);
            await _supplierRepository.SaveChanges();
        }

        public async Task UpdateAddress(Address address)
        {
            await _supplierRepository.UpdateAddress(address);
            await _supplierRepository.SaveChanges();
        }

        public async Task InsertEmail(Email email)
        {
            await _supplierRepository.InsertEmail(email);
            await _supplierRepository.SaveChanges();
        }

        public async Task UpdateEmail(Email email)
        {
            await _supplierRepository.UpdateEmail(email);
            await _supplierRepository.SaveChanges();
        }

        private bool RunValidation<Tv, Te>(Tv validation, Te entity) where Tv : AbstractValidator<Te> where Te : Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            foreach (var item in validator.Errors)
            {
                _notifierService.AddError(item.ErrorMessage);
            }

            return false;
        }
    }
}
