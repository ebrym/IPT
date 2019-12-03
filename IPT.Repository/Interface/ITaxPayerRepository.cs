using IPT.Data.Entity;
using IPT.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace IPT.Repository.Interface
{
   public  interface ITaxPayerRepository : IDependencyRegister
    {
        Task<bool> CreateTaxPayerInAsync(TaxPayer taxpayer);
        Task<bool> UpdateTaxPayerInAsync(TaxPayer taxpayer);
        Task<IEnumerable<TaxPayer>> GetTaxPayersAsync();
        Task<TaxPayer> GetTaxPayerByIdAsync(int Id);
        Task<TaxPayer> GetTaxPayerByTaxIdAsync(string taxId);
        string GenerateTaxpayerIdAsync(int size, bool lowerCase);
        Task<TaxPayer> GetTaxPayerByPhoneNumberAsync(string phoneNo);
        BigInteger GuidToBigInteger(Guid guid);
    }
}
