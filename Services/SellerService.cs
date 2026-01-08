using AuraCommerce.Data;
using AuraCommerce.Models;
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
            // O serviço é quem põe a mão na massa no banco
            return _context.Seller.ToList();
        }
        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}