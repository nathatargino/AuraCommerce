using Microsoft.AspNetCore.Mvc;
using AuraCommerce.Services;
using AuraCommerce.Models;
using AuraCommerce.Models.ViewModels;
using AuraCommerce.Services.Exceptions;
using System.Collections.Generic; // Necessário para usar List<>

namespace AuraCommerce.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        // Injetando os DOIS serviços
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        // GET: Abre a tela de cadastro
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        // POST: Salva o novo vendedor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        // GET: Abre a tela de confirmação de exclusão
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        // POST: Executa a exclusão
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Detalhes do vendedor
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        } 

        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

           
            List<Department> departments = _departmentService.FindAll();

            
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };

            return View(viewModel);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            // Validação de segurança
            if (id != seller.Id)
            {
                // Redireciona para a tela de erro personalizada
                return RedirectToAction("Error", "Home", new { message = "Id mismatch" });
            }

            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                // Redireciona para a tela de erro personalizada
                return RedirectToAction("Error", "Home", new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                // Redireciona para a tela de erro personalizada
                return RedirectToAction("Error", "Home", new { message = e.Message });
            }
        }
    }
}