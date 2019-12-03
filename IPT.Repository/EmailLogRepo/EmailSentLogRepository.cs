using IPT.Data;
using IPT.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPT.Repository.EmailLogRepo
{
   public class EmailSentLogRepository : IEmailSentLog
    {
        private readonly IPTContext _context;

        public EmailSentLogRepository(IPTContext context)
        {
            _context = context;
        }

        public async Task<bool> LogEmailAsync(EmailLog emailLog)
        {
            if (emailLog != null)
            {

                await _context.AddAsync(emailLog);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> LogEmailTransactionAsync(EmailLog emailLog)
        {
            if (emailLog != null)
            {

                await _context.AddAsync(emailLog);
                //await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async  Task<bool> UpdateEmailAsync(EmailLog emailLog)
        {
            if (emailLog != null)
            {
                _context.Update(emailLog);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<EmailLog> GetEmailSentByIdAsync(int Id)
        {
            return await _context.EmailLogs.FindAsync(Id);
        }
        public async Task<IEnumerable<EmailLog>> GetEmailSentLogAsync()
        {
            return await _context.EmailLogs.ToListAsync();
        }

    }
}
