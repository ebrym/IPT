using System;
using System.Collections.Generic;
using System.Text;

namespace IPT.Data.Entity
{
   public class PaymentTransaction : BaseEntity.Entity
    {
       
        public TaxPayer TaxPayer { get; set; }
        public string IPTaxId { get; set; }
        public string PaymentType { get; set; }
        public double AmountPaid { get; set; }
        public string PaymentServiceProvider { get; set; }
        public string PaymentReferenceNo { get; set; }
        public string TransactionReferenceNo { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseDetails { get; set; }
        public string Status { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
