using System.ComponentModel.DataAnnotations;

namespace AuraCommerce.Models.ViewModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "Digite um e-mail válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O assunto é obrigatório")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "A mensagem é obrigatória")]
        [MinLength(10, ErrorMessage = "A mensagem deve ter pelo menos 10 caracteres")]
        public string Message { get; set; }
    }
}