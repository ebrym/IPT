using System;
using System.Collections.Generic;
using System.Text;

namespace IPT.Data.Entity
{
    public class MessageOut
    {
        public int Id { get; set; }
        public string PhoneNo { get; set; }
        public string Sender { get; set; }
        public string msg { get; set; }
        public string MSGType { get; set; }
        public string Status { get; set; }
        public string ServiceType { get; set; }
        public bool IsProcessed { get; set; }
        public string Receiver { get; set; }
        public string Operator { get; set;}
        public DateTime? DateSent { get; set; }
        public DateTime? DateProcessed { get; set; }
        public DateTime? DateReceived { get; set; }


    }
}
