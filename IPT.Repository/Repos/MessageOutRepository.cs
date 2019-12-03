
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using IPT.Repository.Interface;
using IPT.Data;
using IPT.Data.Entity;

namespace IPT.Repository.Repos
{
    public class MessageOutRepository : IMessageOutRepository
    {
        private readonly IPTContext _context;

        public MessageOutRepository(IPTContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateMessageOutAsync(MessageOut sms)
        {
            if (sms != null)
            {

                await _context.AddAsync(sms);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }



        public async Task<IEnumerable<MessageOut>> GetMessageOutAsync()
        {
            return await _context.MessageOuts.ToListAsync();
        }

        public async Task<MessageOut> GetMessageOutByIdAsync(int Id)
        {
            return await _context.MessageOuts.FindAsync(Id);
        }

        public async Task<bool> UpdateMessageOutAsync(MessageOut sms)
        {
            if (sms != null)
            {
                _context.Update(sms);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteMessageOutAsync(int Id)
        {
            var sms = await _context.MessageOuts.Where(x => x.Id == Id).FirstAsync();
            if (sms != null)
            {
                _context.Remove(sms);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }




        public async Task<IEnumerable<MessageOut>> GetMessageOutByPhoneNumberAsync(string phoneNo)
        {

            var msgDetails = await _context.MessageOuts.Where(x => x.PhoneNo == phoneNo)
                                                .OrderByDescending(x => x.DateReceived)
                                                .ToListAsync();

            return msgDetails;
        }

        public bool CreateMessageOut(MessageOut sms)
        {
            if (sms != null)
            {

                _context.Add(sms);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

      
    }
}
