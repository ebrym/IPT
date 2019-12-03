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
    public class TaxpayerController : BaseController
    {
        private IMessageInRepository _messageIn;
        private ITaxPayerRepository _taxPayerRepository;
        private readonly IMapper _mapper;
        public TaxpayerController(IMapper mapper, ITaxPayerRepository taxPayerRepository,
            IMessageInRepository messageIn)
        {
            _messageIn = messageIn;
            _taxPayerRepository = taxPayerRepository;
            _mapper = mapper;

        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
           return View();
        }

        
        public async Task<IActionResult> GetTaxpayers()
        {
            var model = await _taxPayerRepository.GetTaxPayersAsync();
            return Json(new { columns = model });
        }

       
    }
}
