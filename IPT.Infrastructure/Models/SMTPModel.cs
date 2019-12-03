﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IPT.Infrastructure.Models
{
    public class EmailSettings
    {
        public bool SSl { get; set; } = false;
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string Sender { get; set; }
        public string SenderName { get; set; }
        public string Password { get; set; }
        public string subject { get; set; }
    }

}
