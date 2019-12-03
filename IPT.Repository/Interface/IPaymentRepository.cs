using IPT.Data.Entity;
using IPT.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace IPT.Repository.Interface
{
   public  interface IPaymentRepository : IDependencyRegister
    {
        Task<bool> CreatePaymentAsync(PaymentTransaction taxpayer);
        
        Task<IEnumerable<PaymentTransaction>> GetPaymentAsync();
        Task<IEnumerable<PaymentTransaction>> GetPaymentByIPTIdAsync();
        Task<PaymentTransaction> GetPaymentByIPTIdAsync(int Id);
    }
}
