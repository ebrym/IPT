
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
    public class MessageInRepository : IMessageInRepository
    {
        private readonly IPTContext _context;

        public MessageInRepository(IPTContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateMessageInAsync(MessageIn sms)
        {
            if (sms != null)
            {

                await _context.AddAsync(sms);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }



        public async Task<IEnumerable<MessageIn>> GetMessageInAsync()
        {
            return await _context.MessageIns.ToListAsync();
        }

        public async Task<MessageIn> GetMessageInByIdAsync(int Id)
        {
            return await _context.MessageIns.FindAsync(Id);
        }

        public async Task<bool> UpdateMessageInAsync(MessageIn sms)
        {
            if (sms != null)
            {
                _context.Update(sms);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteMessageInAsync(int Id)
        {
            var sms = await _context.MessageIns.Where(x => x.Id == Id).FirstAsync();
            if (sms != null)
            {
                _context.Remove(sms);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }




        public async Task<IEnumerable<MessageIn>> GetMessageInByPhoneNumberAsync(string phoneNo)
        {

            var msgDetails = await _context.MessageIns.Where(x => x.PhoneNo == phoneNo)
                                                .OrderByDescending(x => x.DateReceived)
                                                .ToListAsync();

            return msgDetails;
        }

        public bool CreateMessageIn(MessageIn sms)
        {
            if (sms != null)
            {

                _context.Add(sms);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<MessageIn>> GetOnBaordingMassagesAsync()
        {

            var newOnbaording = await _context.MessageIns.Where(x => x.msg == "IPT" && x.IsProcessed == false).ToListAsync();
            return newOnbaording;
            
        }
    }
}
