
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using IPT.Infrastructure.Interface;
using IPT.Data;
using IPT.Data.Entity;
using IPT.Repository.Interface;
using System.Numerics;

namespace IPT.Infrastructure.Services
{
    public class BackgroundService : IBackgroundService
    {
        private readonly IPTContext _context;
        private  IMessageInRepository _messageIn;
        private ITaxPayerRepository _taxPayerRepository;

        public BackgroundService(IPTContext context,
             ITaxPayerRepository taxPayerRepository,
            IMessageInRepository messageIn)
        {
            _context = context;
            _messageIn = messageIn;
            _taxPayerRepository = taxPayerRepository;
        }

        public async Task ProcessNewOnBaording()
        {
            try
            {
                // get all new onbaording 
                var newOnboard = await _context.MessageIns.Where(x => x.IsProcessed == false && x.msg.ToUpper() == "IPT").ToListAsync();
                for (int i = 0; i < newOnboard.Count(); i++)
                {
                    using (var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.ReadUncommitted))
                    {
                        var retSms = "";
                        var iptID = "";
                      
                        var existingTaxpayer = await _context.TaxPayers.Where(x => x.PhoneNo == newOnboard[i].PhoneNo).ToListAsync();
                        if (existingTaxpayer.Count() == 0)
                        {
                            TaxPayer taxpayer = new TaxPayer
                            {
                                PhoneNo = newOnboard[i].PhoneNo,
                                IPTaxId = _taxPayerRepository.GuidToBigInteger(Guid.NewGuid()).ToString().Substring(0, 10),
                                SubscriptionType = "DAILY",
                                AmountToDeduct = 6,
                                Balance = 3500,
                                CreatedBy = "SMS",
                                IsActive = true,
                                DateCreated = DateTime.Now,
                                LastPaymentDate = DateTime.Now,
                                NextPaymentDate = DateTime.Now
                            };
                            iptID = taxpayer.IPTaxId;
                            

                            retSms = "Thank you for Subcribing to IPT. Your I pay tax Id is " + iptID + " with DAILY deduction of " + taxpayer.AmountToDeduct.ToString() + "NGN.";
                            
                         _context.Add(taxpayer);
                        }
                        else
                        {
                            iptID = existingTaxpayer.FirstOrDefault().IPTaxId;
                            retSms = "You have already registered for IPT. Your IPT ID is " + iptID;
                        }
                        


                        MessageOut messageOut = new MessageOut {
                            MSGType="SMS:TEXT",
                            msg = retSms,
                            PhoneNo = newOnboard[i].PhoneNo,
                            ServiceType="ON-BOARDING",
                            Sender="I PAY TAX",
                            IsProcessed = true,
                            Status = "Send",
                            Receiver = newOnboard[i].PhoneNo,
                            DateProcessed = DateTime.Now
                            
                        };

                         _context.Add(messageOut);

                        newOnboard[i].IsProcessed = true;

                         _context.SaveChanges();

                        transaction.Commit();

                       // return;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task ProcessSubscription()
        {
            try
            {
                // get all new onbaording 
                var newOnboard = await _context.MessageIns.Where(x => x.IsProcessed == false).ToListAsync();

                for (int i = 0; i < newOnboard.Count(); i++)
                {
                    using (var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.ReadUncommitted))
                    {

                        // get the tax payer to process
                        string retSms = "";
                        var taxpayer = await _taxPayerRepository.GetTaxPayerByPhoneNumberAsync(newOnboard[i].PhoneNo);
                        switch (newOnboard[i].msg.ToUpper())
                        {
                            case "DAILY":
                                taxpayer.SubscriptionType = "DAILY";
                                taxpayer.AmountToDeduct = 6;
                                break;
                            case "WEEKLY":
                                taxpayer.SubscriptionType = "WEEKLY";
                                taxpayer.AmountToDeduct = 42;
                                break;
                            case "MONTHLY":
                                taxpayer.SubscriptionType = "MONTHLY";
                                taxpayer.AmountToDeduct = 168;
                                break;
                            case "STOP":
                                taxpayer.IsActive = false;
                                break;
                            case "BALANCE":
                                taxpayer.IsActive = false;
                                break;
                            default:
                                break;
                        }

                        string subType = "";

                        if (newOnboard[i].msg.ToUpper() == "BALANCE")
                        {
                            subType = "CHECK BALANCE";
                            retSms = "IPT payment history. Sub. Type: " + taxpayer.SubscriptionType + 
                                        " Cur. Deduct. AMT: " + taxpayer.AmountToDeduct.ToString() + "NGN" +
                                        " Total AMT Deducted: " + taxpayer.TotalAmmountDeducted.ToString() + "NGN" +
                                        " Payable Balance: " + taxpayer.Balance.ToString() + "NGN" +
                                        " Last Payment Date: " + taxpayer.LastPaymentDate.ToString();
                        }
                        else
                        {
                            subType = "SUBSCRIPTION CHANGE";
                            taxpayer.UpdatedBy = "SMS";
                            taxpayer.LastDateUpdated = DateTime.Now;
                            if (taxpayer.IsActive)
                                retSms = "Your IPT subscripton has been changed to " + taxpayer.SubscriptionType + " with subsequence deduction of " + taxpayer.AmountToDeduct.ToString() + "NGN.";
                            else
                                retSms = "Your IPT subscripton has been DEACTIVATED and all deductions suspended.";
                        }


                        MessageOut messageOut = new MessageOut
                        {
                            MSGType = "SMS:TEXT",
                            msg = retSms,
                            PhoneNo = taxpayer.PhoneNo,
                            ServiceType = subType,
                            Sender = "I PAY TAX",
                            IsProcessed = true,
                            Status = "Send",
                            Receiver = taxpayer.PhoneNo,
                            DateProcessed = DateTime.Now
                        };
                        
                        _context.Add(messageOut);

                        newOnboard.FirstOrDefault().IsProcessed = true;

                        _context.SaveChanges();

                        transaction.Commit();

                        // return;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task ProcessDeductions()
        {
            try
            {
                // get all deductions 

                var deductions = await _context.TaxPayers.Where(x => x.IsActive == true && x.Balance > 0  && (DateTime.Compare(Convert.ToDateTime(x.NextPaymentDate), DateTime.Today) <= 0))
                                                                                                            .ToListAsync();

                //var newOnboard = await _context.MessageIns.Where(x => x.IsProcessed == false && x.msg.ToUpper() == "IPT").ToListAsync();
                for (int i = 0; i < deductions.Count(); i++)
                {
                    using (var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.ReadUncommitted))
                    {
                       


                        PaymentTransaction payment = new PaymentTransaction
                        {
                            PaymentDate = DateTime.Now,
                            IPTaxId = deductions[i].IPTaxId,
                            PaymentReferenceNo = _taxPayerRepository.GenerateTaxpayerIdAsync(10, false),
                            AmountPaid = deductions[i].AmountToDeduct,
                            PaymentType = deductions[i].SubscriptionType,
                            PaymentServiceProvider = "SMS",
                            TaxPayer= deductions[i],
                            TransactionDate = DateTime.Now,
                            TransactionReferenceNo = _taxPayerRepository.GuidToBigInteger(Guid.NewGuid()).ToString().Substring(0, 12),
                            Status="Success",
                            CreatedBy="Service",
                            DateCreated = DateTime.Now  
                        };

                        DateTime nextpaymentDate = Convert.ToDateTime(deductions[i].NextPaymentDate);

                        switch (deductions[i].SubscriptionType.ToUpper())
                        {
                            case "DAILY":
                                deductions[i].NextPaymentDate  = nextpaymentDate.AddDays(1) ;
                                deductions[i].TotalAmmountDeducted = deductions[i].TotalAmmountDeducted + 6;
                                break;
                            case "WEEKLY":
                                deductions[i].NextPaymentDate = nextpaymentDate.AddDays(7);
                                deductions[i].TotalAmmountDeducted = deductions[i].TotalAmmountDeducted + 42;
                                break;
                            case "MONTHLY":
                                deductions[i].NextPaymentDate = nextpaymentDate.AddMonths(1);
                                deductions[i].TotalAmmountDeducted = deductions[i].TotalAmmountDeducted + 168;
                                break;
                            default:
                                break;
                        }


                        deductions[i].Balance = deductions[i].Balance - deductions[i].AmountToDeduct;

                        

                        var retSms = "Your "+ deductions[i].SubscriptionType.ToUpper() + 
                                        " IPT payment deductions of "+ deductions[i].AmountToDeduct.ToString() + 
                                        "NGN was successfull. Total Tax payable balance is " + deductions[i].Balance.ToString() + "NGN";        

                        MessageOut messageOut = new MessageOut
                        {
                            MSGType = "SMS:TEXT",
                            msg = retSms,
                            PhoneNo = deductions[i].PhoneNo,
                            ServiceType = "PAYMENT DEDUCTIONS",
                            Sender = "I PAY TAX",
                            Status = "Send",
                            IsProcessed = true,
                            Receiver = deductions[i].PhoneNo,
                            DateProcessed = DateTime.Now
                        };

                        deductions[i].LastPaymentDate = DateTime.Now;

                        _context.Add(payment);
                        _context.Update(deductions[i]);
                        _context.Add(messageOut);


                        _context.SaveChanges();

                        transaction.Commit();

                        // return;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
