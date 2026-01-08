using AuraCommerce.Models;
using AuraCommerce.Models.ViewModels;
using AuraCommerce.Services; 
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks; 

namespace AuraCommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EmailService _emailService; 

        // Construtor atualizado recebendo o Logger e o EmailService
        public HomeController(ILogger<HomeController> logger, EmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Gestão inteligente de vendas e departamentos.";
            ViewData["Desenvolvedor"] = "Nathã Targino";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }

        // GET: Abre o formulário
        public IActionResult Contact()
        {
            return View();
        }

        // POST: Envia o e-mail
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Chama o serviço para enviar o e-mail de verdade
                await _emailService.SendEmailAsync(model.Name, model.Email, model.Subject, model.Message);

                ViewData["SuccessMessage"] = "Sua mensagem foi enviada com sucesso! Entraremos em contato em breve.";

                // Limpa o formulário visualmente
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                // Se der erro (ex: senha errada no appsettings), mostra o erro na tela
                ModelState.AddModelError("", $"Erro ao enviar e-mail: {ex.Message}");
                // Retorna o model para o usuário não perder o que digitou
                return View(model);
            }

            return View();
        }
    }
}