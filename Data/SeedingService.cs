using AuraCommerce.Models;
using AuraCommerce.Models.Enums;
using System;
using System.Collections.Generic; 
using System.Linq;

namespace AuraCommerce.Data
{
    public class SeedingService
    {
        private readonly AuraCommerceContext _context;

        public SeedingService(AuraCommerceContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            
            var deptComputers = _context.Department.FirstOrDefault(x => x.Name == "Computers");
            if (deptComputers != null) deptComputers.Name = "Computadores";

            var deptElectronics = _context.Department.FirstOrDefault(x => x.Name == "Electronics");
            if (deptElectronics != null) deptElectronics.Name = "Eletrônicos";

            var deptFashion = _context.Department.FirstOrDefault(x => x.Name == "Fashion");
            if (deptFashion != null) deptFashion.Name = "Moda";

            var deptBooks = _context.Department.FirstOrDefault(x => x.Name == "Books");
            if (deptBooks != null) deptBooks.Name = "Livros";

            
            if (deptComputers != null || deptElectronics != null)
            {
                _context.SaveChanges();
            }

            
            bool hasData = _context.SalesRecord.Any();

            List<Seller> sellers = new List<Seller>();

            if (!hasData)
            {
                // 1. Departamentos 
                Department d1 = new Department { Name = "Computadores" };
                Department d2 = new Department { Name = "Eletrônicos" };
                Department d3 = new Department { Name = "Moda" };
                Department d4 = new Department { Name = "Livros" };

                // 2. Vendedores
                Seller s1 = new Seller { Name = "João Ricardo", Email = "joaoricardo@gmail.com", BirthDate = new DateTime(1998, 4, 15), BaseSalary = 1200.0, Department = d1 };
                Seller s2 = new Seller { Name = "Maria Green", Email = "maria@gmail.com", BirthDate = new DateTime(1979, 12, 31), BaseSalary = 3500.0, Department = d2 };
                Seller s3 = new Seller { Name = "Alex Grey", Email = "alex@gmail.com", BirthDate = new DateTime(1988, 1, 15), BaseSalary = 2200.0, Department = d1 };
                Seller s4 = new Seller { Name = "Martha Red", Email = "martha@gmail.com", BirthDate = new DateTime(1993, 11, 30), BaseSalary = 3000.0, Department = d4 };
                Seller s5 = new Seller { Name = "Donald Blue", Email = "donald@gmail.com", BirthDate = new DateTime(2000, 1, 9), BaseSalary = 4000.0, Department = d3 };
                Seller s6 = new Seller { Name = "Alex Pink", Email = "bob@gmail.com", BirthDate = new DateTime(1997, 3, 4), BaseSalary = 3000.0, Department = d2 };

                _context.Department.AddRange(d1, d2, d3, d4);
                _context.Seller.AddRange(s1, s2, s3, s4, s5, s6);

                // Salvamos aqui para garantir que os vendedores tenham IDs para as vendas
                _context.SaveChanges();

                // Atualiza a lista local de vendedores com os que acabaram de ser salvos
                sellers.AddRange(new[] { s1, s2, s3, s4, s5, s6 });

                
                SalesRecord r1 = new SalesRecord { Date = new DateTime(2018, 09, 25), Amount = 11000.0, Status = SaleStatus.Billed, Seller = s1 };
                SalesRecord r2 = new SalesRecord { Date = new DateTime(2018, 09, 4), Amount = 7000.0, Status = SaleStatus.Billed, Seller = s5 };
                SalesRecord r3 = new SalesRecord { Date = new DateTime(2018, 09, 13), Amount = 4000.0, Status = SaleStatus.Canceled, Seller = s4 }; 

                _context.SalesRecord.AddRange(r1, r2, r3);
                _context.SaveChanges();
            }
            else
            {
                
                sellers = _context.Seller.ToList();
            }

            
            if (_context.SalesRecord.Count() < 50)
            {
                var r = new Random();
                var salesToAdd = new List<SalesRecord>();

                for (int i = 0; i < 300; i++)
                {
                    var seller = sellers[r.Next(sellers.Count)];

                    // Data entre 01/01/2025 e Hoje
                    DateTime start = new DateTime(2025, 1, 1);
                    int range = (DateTime.Today - start).Days;
                    DateTime randomDate = start.AddDays(r.Next(range));

                    double amount = 100.0 + (r.NextDouble() * 4900.0);
                    SaleStatus status = (SaleStatus)r.Next(0, 3);

                    SalesRecord sr = new SalesRecord
                    {
                        Date = randomDate,
                        Amount = amount,
                        Status = status,
                        Seller = seller
                    };

                    salesToAdd.Add(sr);
                }

                _context.SalesRecord.AddRange(salesToAdd);
                _context.SaveChanges();
            }
        }
    }
}