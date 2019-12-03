using IPT.Data.Entity;
using IPT.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPT.Repository.Interface
{
   public  interface IMessageInRepository : IDependencyRegister
    {
        Task<bool> CreateMessageInAsync(MessageIn sms);
       bool CreateMessageIn(MessageIn category);
        Task<IEnumerable<MessageIn>> GetMessageInAsync();
        Task<MessageIn> GetMessageInByIdAsync(int Id);
        Task<IEnumerable<MessageIn>> GetOnBaordingMassagesAsync();
        Task<IEnumerable<MessageIn>> GetMessageInByPhoneNumberAsync(string phoneNo);
    }
}
