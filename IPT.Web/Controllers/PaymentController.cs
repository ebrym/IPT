using AutoMapper;
using IPT.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IPT.Web.Controllers
{
    [Authorize]
    public class PaymentController : BaseController
    {
        private IMessageInRepository _messageIn;
        private ITaxPayerRepository _taxPayerRepository;
        private IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        public PaymentController(IMapper mapper, ITaxPayerRepository taxPayerRepository,
            IPaymentRepository paymentRepository,
        IMessageInRepository messageIn)
        {
            _messageIn = messageIn;
            _taxPayerRepository = taxPayerRepository;
            _paymentRepository = paymentRepository;
            _mapper = mapper;

        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
           return View();
        }

        
        public async Task<IActionResult> GetPayments()
        {
            var model = await _paymentRepository.GetPaymentAsync();
            return Json(new { columns = model });
        }

       
    }
}
