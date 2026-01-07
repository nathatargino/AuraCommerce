using Microsoft.AspNetCore.Mvc;
using AuraCommerce.Services;
using AuraCommerce.Models;

namespace AuraCommerce.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;

        
        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            // Pede ao serviço a lista de vendedores
            var list = _sellerService.FindAll();
            return View(list);
        }
    }
}