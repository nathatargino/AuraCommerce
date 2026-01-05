using Microsoft.AspNetCore.Mvc;
using AuraCommerce.Data;
using AuraCommerce.Models;
using System.Linq;

namespace AuraCommerce.Controllers
{
    public class SellersController : Controller
    {
        private readonly AuraCommerceContext _context;

        public SellersController(AuraCommerceContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Busca a lista de vendedores no banco de dados
            var list = _context.Seller.ToList();
            return View(list);
        }
    }
}