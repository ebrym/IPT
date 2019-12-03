
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
    public class TaxPayerRepository : ITaxPayerRepository
    {
        private readonly IPTContext _context;

        public TaxPayerRepository(IPTContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateTaxPayerInAsync(TaxPayer taxpayer)
        {
            if (taxpayer != null)
            {

                await _context.AddAsync(taxpayer);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<TaxPayer>> GetTaxPayersAsync()
        {
            return await _context.TaxPayers.ToListAsync();
        }

        public async Task<TaxPayer> GetTaxPayerByIdAsync(int Id)
        {
            return await _context.TaxPayers.FindAsync(Id);
        }

        public async Task<TaxPayer> GetTaxPayerByTaxIdAsync(string taxId)
        {
            var taxpayer = await _context.TaxPayers.Where(t => t.IPTaxId == taxId).FirstOrDefaultAsync();
            return taxpayer;
        }

        public async Task<bool> UpdateTaxPayerInAsync(TaxPayer taxpayer)
        {
            if (taxpayer != null)
            {
                _context.Update(taxpayer);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
        
        public string GenerateTaxpayerIdAsync(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public async Task<TaxPayer> GetTaxPayerByPhoneNumberAsync(string phoneNo)
        {
            var taxpayer = await _context.TaxPayers.Where(t => t.PhoneNo == phoneNo).FirstOrDefaultAsync();
            return taxpayer;
        }

        public BigInteger GuidToBigInteger(Guid guid)
        {
            BigInteger l_retval = 0;
            byte[] ba = guid.ToByteArray();
            int i = ba.Count();
            foreach (byte b in ba)
            {
                l_retval += b * BigInteger.Pow(10, --i);
            }

            return l_retval;
        }

     
    }
}
