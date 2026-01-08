using Microsoft.AspNetCore.Mvc;
using AuraCommerce.Services;
using AuraCommerce.Models;
using AuraCommerce.Models.ViewModels;

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

        // POST 

        [HttpPost]
        [ValidateAntiForgeryToken] // Proteção contra ataques CSRF
        public IActionResult Create(Seller seller)
        {
            // Chama o serviço para gravar no banco
            _sellerService.Insert(seller);

            // Volta para a lista de vendedores
            return RedirectToAction(nameof(Index));
        }
    }
}