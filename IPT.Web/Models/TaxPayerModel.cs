using System;
using System.Collections.Generic;
using System.Text;

namespace IPT.Web.Models
{
  public  class TaxPayerModel 
    {
        public string Id { get; set; }
        public string IPTaxId { get; set; }
        public string LIRSId  { get; set; }
        public string FisrtName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string LGA { get; set; }
        public string StateOfResident  { get; set; }
        public string SubscriptionType { get; set; }
        public double AmountToDeduct { get; set; }
        public double Balance { get; set; }
        public string MSIDN { get; set; }
        public string MSP { get; set; }
        public DateTime? LastPaymentDate { get; set; }

    }
}
