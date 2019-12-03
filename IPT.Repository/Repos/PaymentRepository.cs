
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using IPT.Repository.Interface;
using IPT.Data;
using IPT.Data.Entity;
using System.Numerics;

namespace IPT.Repository.Repos
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IPTContext _context;

        public PaymentRepository(IPTContext context)
        {
            _context = context;
        }

    

 
  

        public async Task<bool> CreatePaymentAsync(PaymentTransaction taxpayer)
        {
            if (taxpayer != null)
            {

                await _context.AddAsync(taxpayer);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<PaymentTransaction>> GetPaymentAsync()
        {
            return await _context.PaymentTransactions.ToListAsync();
        }

        public Task<IEnumerable<PaymentTransaction>> GetPaymentByIPTIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PaymentTransaction> GetPaymentByIPTIdAsync(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
