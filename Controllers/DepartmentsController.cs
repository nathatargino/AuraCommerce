using Microsoft.AspNetCore.Mvc;
using AuraCommerce.Services;
using AuraCommerce.Models;

namespace AuraCommerce.Controllers
{
    public class DepartmentsController : Controller
    {
        
        private readonly DepartmentService _departmentService;

        
        public DepartmentsController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        
        public IActionResult Index()
        {
            // Busca a lista ordenada 
            var list = _departmentService.FindAll();
            return View(list);
        }

        // Abrir formulário (GET)
        public IActionResult Create()
        {
            return View();
        }

        // Salva formulário (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
           
            _departmentService.Insert(department);
            return RedirectToAction(nameof(Index));
        }
    }
}