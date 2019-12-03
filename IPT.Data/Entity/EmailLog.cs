using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IPT.Data.Entity
{
    public class EmailLog : BaseEntity.Entity
    {
        public string RecipientEmail { get; set; }

        [DataType(DataType.Text)]
        public string EmailContent { get; set; }
        public string Status { get; set; }
    }
}
