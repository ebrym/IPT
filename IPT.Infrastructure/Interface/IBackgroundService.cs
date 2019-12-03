using IPT.Data.Entity;
using IPT.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPT.Infrastructure.Interface
{
   public  interface IBackgroundService : IDependencyRegister
    {
      
        Task ProcessNewOnBaording();
        Task ProcessSubscription();
        Task ProcessDeductions();
    }
}
