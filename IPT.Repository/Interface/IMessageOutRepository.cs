using IPT.Data.Entity;
using IPT.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPT.Repository.Interface
{
    public interface IMessageOutRepository : IDependencyRegister
    {
        Task<bool> CreateMessageOutAsync(MessageOut sms);
        bool CreateMessageOut(MessageOut category);
        Task<IEnumerable<MessageOut>> GetMessageOutAsync();
        Task<MessageOut> GetMessageOutByIdAsync(int Id);
    }
}
