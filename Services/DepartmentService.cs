using AuraCommerce.Data;
using AuraCommerce.Models;
using System.Collections.Generic;
using System.Linq;

namespace AuraCommerce.Services
{
    public class DepartmentService
    {
        private readonly AuraCommerceContext _context;

        public DepartmentService(AuraCommerceContext context)
        {
            _context = context;
        }

        // Buscar todos os departamentos ordenados por nome
        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }
    }
}