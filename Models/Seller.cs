using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AuraCommerce.Models
{
    public class Seller
    {
        public int Id { get; set; }

        // Validação: Obrigatório, Min 3 letras, Max 60 letras
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "O tamanho do {0} deve ser entre {2} e {1}")]
        public string Name { get; set; }

        // Validação: Obrigatório e formato de e-mail
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "Entre com um e-mail válido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        // Validação: Obrigatório
        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        // Validação: Obrigatório e valor entre 100 e 500.000
        [Display(Name = "Salário Base")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(100.0, 500000.0, ErrorMessage = "O {0} deve ser entre {1} e {2}")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double BaseSalary { get; set; }

        // --- Chave Estrangeira e Navegação ---
        [Display(Name = "Departamento")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        // --- Lista de Vendas ---
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        // Versionador BD
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;

            if (department != null)
            {
                DepartmentId = department.Id;
            }
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}