using AuraCommerce.Data;
using AuraCommerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }

        // Salva no banco
        public async Task Insert(Department obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        internal async Task InsertAsync(Department department)
        {
            throw new NotImplementedException();
        }
    }
}