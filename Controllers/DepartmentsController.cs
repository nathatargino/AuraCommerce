using Microsoft.AspNetCore.Mvc;
using AuraCommerce.Services;
using AuraCommerce.Models;
using System.Threading.Tasks;

namespace AuraCommerce.Controllers
{
    public class DepartmentsController : Controller
    {
        
        private readonly DepartmentService _departmentService;

        
        public DepartmentsController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }


        public async Task<IActionResult> Index()
        {
            var list = await _departmentService.FindAllAsync(); 
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
        public async Task<IActionResult> Create(Department department)
        {
            await _departmentService.InsertAsync(department); 
            return RedirectToAction(nameof(Index));
        }
    }
}