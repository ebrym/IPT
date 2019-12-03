using System;
using System.Collections.Generic;
using System.Text;

namespace IPT.Data.Entity
{
   public class MessageIn
    {
        
        public int Id { get; set; }
        public string PhoneNo { get; set; }
        public string msg { get; set; }
        public string receiver { get; set; }
        public string sender { get; set; }
        public string msgtype { get; set; }
        public string Operator { get; set; }
        public string ServiceType { get; set; }
        public DateTime? SentTime { get; set; }
        public DateTime? ReceivedTime { get; set; }
        public DateTime? DateReceived { get; set; }
        public bool IsProcessed { get; set; }
        public DateTime? DateProcessed { get; set; }
    }
}
