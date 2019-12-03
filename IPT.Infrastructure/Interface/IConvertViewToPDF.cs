
using IPT.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPT.Infrastructure.Interface
{
    public interface IConvertViewToPDF : IDependencyRegister
    {
        Task<bool> CreatePDF(string emailMessage);
    }
}
