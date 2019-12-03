using IPT.Data.Entity;
using IPT.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPT.Repository.EmailLogRepo
{
   public interface IEmailSentLog : IDependencyRegister
    {
        Task<bool> LogEmailAsync(EmailLog emailLog);
        Task<bool> LogEmailTransactionAsync(EmailLog emailLog);

        Task<bool> UpdateEmailAsync(EmailLog emailLog);

        Task<EmailLog> GetEmailSentByIdAsync(int Id);

        Task<IEnumerable<EmailLog>> GetEmailSentLogAsync();
    }
}
