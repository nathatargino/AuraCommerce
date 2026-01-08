using AuraCommerce.Data;
using AuraCommerce.Models;
using Microsoft.EntityFrameworkCore;
using AuraCommerce.Services.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace AuraCommerce.Services
{
    public class SellerService
    {
        private readonly AuraCommerceContext _context;

        public SellerService(AuraCommerceContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }


        // Busca 1 vendedor pelo ID (e traz o Departamento junto)
        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        // Remove o vendedor pelo ID
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
        public void Update(Seller obj)
        {
            // 1. Verifica se o Vendedor existe no banco antes de tentar editar
            if (!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {

                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}