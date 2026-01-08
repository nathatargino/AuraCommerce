using Microsoft.AspNetCore.Mvc;
using AuraCommerce.Services;
using AuraCommerce.Models;
using AuraCommerce.Models.ViewModels;
using AuraCommerce.Services.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuraCommerce.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        
        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            ModelState.Remove("RowVersion");
            ModelState.Remove("Seller.RowVersion");
            ModelState.Remove("Department");
            ModelState.Remove("Seller.Department");
            ModelState.Remove("Sales");
            ModelState.Remove("Seller.Sales");

            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null) return NotFound();

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction("Error", "Home", new { message = e.Message });
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null) return NotFound();

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null) return NotFound();

            
            List<Department> departments = await _departmentService.FindAllAsync();

            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return RedirectToAction("Error", "Home", new { message = "Id mismatch" });
            }

            ModelState.Remove("Department");
            ModelState.Remove("Sales");

            
            if (!ModelState.IsValid)
            {
                
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction("Error", "Home", new { message = e.Message });
            }
        }
    }
}